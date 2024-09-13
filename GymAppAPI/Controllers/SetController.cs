using AutoMapper;
using GymAppAPI.Dto;
using GymAppAPI.Interfaces;
using GymAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymAppAPI.Controllers
{
    [Route("api/set")]
    [ApiController]
    public class SetController : ControllerBase
    {
        private readonly ISetRepository _setRepository;
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;

        public SetController(ISetRepository setRepository, IWorkoutRepository workoutRepository, IExerciseRepository exerciseRepository,
            IMapper mapper)
        {
            _setRepository = setRepository;
            _workoutRepository = workoutRepository;
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sets = _mapper.Map<List<SetDto>>(await _setRepository.GetAllSetsAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sets);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get(int id)
        {
            if (!await _setRepository.SetExistsAsync(id))
                return NotFound();

            var set = _mapper.Map<SetDto>(await _setRepository.GetSetByIdAsync(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(set);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post(SetDto setDto, int workoutId, int exerciseId)
        {
            if (setDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var setMap = _mapper.Map<Set>(setDto);
            setMap.Workout = await _workoutRepository.GetWorkoutByIdAsync(workoutId);
            setMap.Exercise = await _exerciseRepository.GetExerciseByIdAsync(exerciseId);

            if(setMap.Exercise == null && setMap.Workout == null)
            {
                ModelState.AddModelError("", "Selected workout or exercise does not exist");
                return StatusCode(400, ModelState);
            }

            await _setRepository.AddSetAsync(setMap);
            return CreatedAtAction(nameof(Get), new {id = setMap.Id}, _mapper.Map<SetDto>(setMap));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(int id, SetDto updatedSetDto)
        {
            if(!await _setRepository.SetExistsAsync(id))
                return NotFound();

            if(id != updatedSetDto.Id)
                return BadRequest(ModelState);

            if (updatedSetDto == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var updatedSetMap = _mapper.Map<Set>(updatedSetDto);
            await _setRepository.UpdateSetAsync(updatedSetMap);
            return Ok(_mapper.Map<SetDto>(updatedSetMap));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _setRepository.SetExistsAsync(id))
            {
                return NotFound();
            }

            await _setRepository.DeleteSetAsync(id);
            return NoContent();
        }
    }
}
