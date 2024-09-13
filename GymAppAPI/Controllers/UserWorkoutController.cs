using AutoMapper;
using GymAppAPI.Dto;
using GymAppAPI.Extensions;
using GymAppAPI.Interfaces;
using GymAppAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GymAppAPI.Controllers
{
    [Route("api/userworkout")]
    [ApiController]
    public class UserWorkoutController:ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IUserWorkoutRepository _userWorkoutRepository;
        private readonly IMapper _mapper;

        public UserWorkoutController(UserManager<AppUser> userManager,
        IWorkoutRepository workoutRepository,
        IUserWorkoutRepository userWorkoutRepository,
        IMapper mapper) 
        {
            _userManager = userManager;
            _workoutRepository = workoutRepository;
            _userWorkoutRepository = userWorkoutRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserWorkouts()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userWorkouts = _mapper.Map<List<WorkoutDto>>(await _userWorkoutRepository.GetUserWorkoutsAsync(appUser));
            return Ok(userWorkouts);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUserWorkout(int id)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var workout = await _workoutRepository.GetWorkoutByIdAsync(id);

            if (workout == null) return BadRequest("Workout not found");

            var userWorkout = new UserWorkout
            {
                WorkoutId = workout.Id,
                AppUserId = appUser.Id
            };

            await _userWorkoutRepository.CreateAsync(userWorkout);

            if(userWorkout == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUserWorkout(int id)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var userWorkouts = await _userWorkoutRepository.GetUserWorkoutsAsync(appUser);

            var filteredUserWorkouts = userWorkouts.Where(w => w.Id == id).ToList();

            if(filteredUserWorkouts.Count > 0)
            {
                await _userWorkoutRepository.DeleteAsync(appUser, id);
            }
            else
            {
                return BadRequest("Workout not found");
            }
            return Ok();
        }

    }
}
