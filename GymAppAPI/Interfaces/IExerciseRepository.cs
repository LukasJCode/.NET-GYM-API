using GymAppAPI.Models;

namespace GymAppAPI.Interfaces
{
    public interface IExerciseRepository
    {
        Task<ICollection<Exercise>> GetAllExercisesAsync();
        Task<Exercise> GetExerciseByIdAsync(int exerciseId);
        Task AddExerciseAsync(Exercise exercise);
        Task AddExerciseToWorkoutAsync(int exerciseId, int workoutId);
        Task<ICollection<Set>> GetSetsForExercise(int exerciseId, int workoutId);
        Task UpdateExerciseAsync(Exercise updatedExercise);
        Task DeleteExerciseAsync(int exerciseId);
        Task<bool> ExerciseExistsAsync(int exerciseId);
        Task<bool> ExerciseInWorkoutExistsAsync(int exerciseId, int workoutId);
    }
}
