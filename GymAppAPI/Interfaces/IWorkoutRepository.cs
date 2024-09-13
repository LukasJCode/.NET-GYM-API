using GymAppAPI.Helper;
using GymAppAPI.Models;

namespace GymAppAPI.Interfaces
{
    public interface IWorkoutRepository
    {
        Task<ICollection<Workout>> GetAllWorkoutsAsync(QueryObject query);
        Task<Workout> GetWorkoutByIdAsync(int workoutId);
        Task AddWorkoutAsync(Workout workout);
        Task UpdateWorkoutAsync(Workout updatedWorkout);
        Task<ICollection<Exercise>> GetExercisesForWorkoutAsync(int workoutId);
        Task DeleteWorkoutAsync(int workoutId);
        Task<bool> WorkoutExistsAsync(int workoutId);

    }
}
