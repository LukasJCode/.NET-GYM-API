using GymAppAPI.Data;
using GymAppAPI.Interfaces;
using GymAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GymAppAPI.Repositories
{
    public class UserWorkoutRepository : IUserWorkoutRepository
    {
        private readonly AppDbContext _db;

        public UserWorkoutRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserWorkout> CreateAsync(UserWorkout userWorkout)
        {
            await _db.UserWorkouts.AddAsync(userWorkout);
            await _db.SaveChangesAsync();
            return userWorkout;
        }

        public async Task<UserWorkout> DeleteAsync(AppUser appUser, int id)
        {
            var userWorkout = await _db.UserWorkouts.FirstOrDefaultAsync(x => x.WorkoutId == id && x.AppUserId == appUser.Id);
            if (userWorkout == null) return null;
            
            _db.UserWorkouts.Remove(userWorkout);
            await _db.SaveChangesAsync();
            return userWorkout;
        }

        public async Task<ICollection<Workout>> GetUserWorkoutsAsync(AppUser user)
        {
            return await _db.UserWorkouts.Where(u => u.AppUserId == user.Id)
            .Select(workout => new Workout
            {
                Id = workout.WorkoutId,
                Name = workout.Workout.Name,
                DateOfWorkout = workout.Workout.DateOfWorkout

            }).ToListAsync();
        }
    }
}
