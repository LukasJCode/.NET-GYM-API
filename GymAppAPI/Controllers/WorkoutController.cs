using AutoMapper;
using GymAppAPI.Dto;
using GymAppAPI.Helper;
using GymAppAPI.Interfaces;
using GymAppAPI.Models;
using GymAppAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymAppAPI.Controllers
{
    [Route("api/workout")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IMapper _mapper;

        public WorkoutController(IWorkoutRepository workoutRepository,
            IMapper mapper)
        {
            _workoutRepository = workoutRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var workouts = _mapper.Map<List<WorkoutDto>>(await _workoutRepository.GetAllWorkoutsAsync(query));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(workouts);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get(int id)
        {
            if (!await _workoutRepository.WorkoutExistsAsync(id))
                return NotFound();

            var workout = _mapper.Map<WorkoutDto>(await _workoutRepository.GetWorkoutByIdAsync(id));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(workout);
        }

        // ???
        [HttpGet("{workoutId}/exercises")]
        public async Task<IActionResult> GetExercisesForWorkout(int workoutId)
        {
            if (!await _workoutRepository.WorkoutExistsAsync(workoutId))
                return NotFound();

            var exercisesForWorkout = _mapper.Map<List<ExerciseDto>>(await _workoutRepository.GetExercisesForWorkoutAsync(workoutId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(exercisesForWorkout);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post(WorkoutDto workoutDto)
        {
            if(workoutDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var workoutMap = _mapper.Map<Workout>(workoutDto);
            await _workoutRepository.AddWorkoutAsync(workoutMap);
            return CreatedAtAction(nameof(Get), new { id = workoutMap.Id}, _mapper.Map<WorkoutDto>(workoutMap));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(int id, WorkoutDto updatedWorkoutDto)
        {
            if(!await _workoutRepository.WorkoutExistsAsync(id))
                return NotFound();
            
            if(id != updatedWorkoutDto.Id)
                return BadRequest(ModelState);

            if(updatedWorkoutDto == null)
                return BadRequest(ModelState);

            if(!ModelState.IsValid)
                return BadRequest();

            var updatedWorkoutMap = _mapper.Map<Workout>(updatedWorkoutDto);
            await _workoutRepository.UpdateWorkoutAsync(updatedWorkoutMap);
            return Ok(_mapper.Map<WorkoutDto>(updatedWorkoutDto));
        }
        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            if(!await _workoutRepository.WorkoutExistsAsync(id))
            {
                return NotFound();
            }

            await _workoutRepository.DeleteWorkoutAsync(id);
            return NoContent();
        }
    }
}
