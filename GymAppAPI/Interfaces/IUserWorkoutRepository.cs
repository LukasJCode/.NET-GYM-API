using GymAppAPI.Models;

namespace GymAppAPI.Interfaces
{
    public interface IUserWorkoutRepository
    {
        Task<ICollection<Workout>> GetUserWorkoutsAsync(AppUser user);
        Task<UserWorkout> CreateAsync(UserWorkout userWorkout);
        Task<UserWorkout> DeleteAsync(AppUser appUser, int id);
    }
}
