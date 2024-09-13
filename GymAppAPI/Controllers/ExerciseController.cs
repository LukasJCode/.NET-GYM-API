using AutoMapper;
using GymAppAPI.Dto;
using GymAppAPI.Interfaces;
using GymAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymAppAPI.Controllers
{
    [Route("api/exercise")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IMapper _mapper;

        public ExerciseController(IExerciseRepository exerciseRepository, IWorkoutRepository workoutRepository,
            IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _workoutRepository = workoutRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var exercises = _mapper.Map<List<ExerciseDto>>(await _exerciseRepository.GetAllExercisesAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(exercises);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get(int id)
        {
            if (!await _exerciseRepository.ExerciseExistsAsync(id))
                return NotFound();

            var exercise = _mapper.Map<ExerciseDto>(await _exerciseRepository.GetExerciseByIdAsync(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(exercise);
        }

        [HttpGet("{exerciseId:int}/{workoutId:int}/sets")]
        public async Task<IActionResult> GetExercisesForWorkout(int exerciseId, int workoutId)
        {
            if (!await _exerciseRepository.ExerciseExistsAsync(exerciseId) && !await _workoutRepository.WorkoutExistsAsync(workoutId))
            {
                ModelState.AddModelError("", "Selected workout or exercise does not exist");
                return StatusCode(400, ModelState);
            }

            var setsForExercise = _mapper.Map<List<SetDto>>(await _exerciseRepository.GetSetsForExercise(exerciseId, workoutId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(setsForExercise);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post(ExerciseDto exerciseDto)
        {
            if (exerciseDto == null)
                return BadRequest(ModelState);

            var exercises = await _exerciseRepository.GetAllExercisesAsync();
            var exerciseWithGivenNameExists = exercises.Where(e => e.Name.Trim().ToUpper() == exerciseDto.Name.Trim().ToUpper()).FirstOrDefault();

            if (exerciseWithGivenNameExists != null)
            {
                ModelState.AddModelError("", "Exercise already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exerciseMap = _mapper.Map<Exercise>(exerciseDto);
            await _exerciseRepository.AddExerciseAsync(exerciseMap);
            return CreatedAtAction(nameof(Get), new { id = exerciseMap.Id}, _mapper.Map<ExerciseDto>(exerciseMap));
        }
        [HttpPost("{exerciseId:int}/{workoutId:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddExerciseToWorkout(int exerciseId, int workoutId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(await _exerciseRepository.ExerciseInWorkoutExistsAsync(exerciseId, workoutId))
            {
                ModelState.AddModelError("", "Selected exercise already exists in the workout");
                return StatusCode(400, ModelState);
            }

            if (!await _workoutRepository.WorkoutExistsAsync(workoutId) && !await _exerciseRepository.ExerciseExistsAsync(exerciseId))
            {
                ModelState.AddModelError("", "Selected workout or exercise does not exist");
                return StatusCode(400, ModelState);
            }
            await _exerciseRepository.AddExerciseToWorkoutAsync(exerciseId, workoutId);
            return Ok("Exercise added to the workout");
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(int id, ExerciseDto updatedExerciseDto)
        {
            if (!await _exerciseRepository.ExerciseExistsAsync(id))
                return NotFound();

            if (id != updatedExerciseDto.Id)
                return BadRequest(ModelState);

            if (updatedExerciseDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var updatedExerciseMap = _mapper.Map<Exercise>(updatedExerciseDto);
            await _exerciseRepository.UpdateExerciseAsync(updatedExerciseMap);
            return Ok(_mapper.Map<ExerciseDto>(updatedExerciseMap));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _exerciseRepository.ExerciseExistsAsync(id))
            {
                return NotFound();
            }

            await _exerciseRepository.DeleteExerciseAsync(id);
            return NoContent();
        }
    }
}
