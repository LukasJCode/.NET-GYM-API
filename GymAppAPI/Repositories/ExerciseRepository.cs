using GymAppAPI.Data;
using GymAppAPI.Interfaces;
using GymAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GymAppAPI.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly AppDbContext _db;
        public ExerciseRepository(AppDbContext db)
        {
            this._db = db;
        }
        public async Task AddExerciseAsync(Exercise exercise)
        {
            await _db.Exercises.AddAsync(exercise);
            await _db.SaveChangesAsync();
        }

        public async Task AddExerciseToWorkoutAsync(int exerciseId, int workoutId)
        {
            var workout = await _db.Workouts.Where(w => w.Id == workoutId).FirstOrDefaultAsync();
            var exercise = await _db.Exercises.Where(e => e.Id == exerciseId).FirstOrDefaultAsync();

            var workoutExercise = new WorkoutExercise()
            {
                Exercise = exercise,
                Workout = workout
            };

            await _db.AddAsync(workoutExercise);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteExerciseAsync(int exerciseId)
        {
            var exercise = await _db.Exercises.FirstOrDefaultAsync(e  => e.Id == exerciseId);
            _db.Exercises.Remove(exercise);

            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExerciseExistsAsync(int exerciseId)
        {
            if(await _db.Exercises.AnyAsync(e => e.Id == exerciseId))
                return true;
            return false;
        }

        public async Task<bool> ExerciseInWorkoutExistsAsync(int exerciseId, int workoutId)
        {
            if (await _db.WorkoutExercises.AnyAsync(we => we.ExerciseId == exerciseId && we.WorkoutId == workoutId))
                return true;
            return false;
        }

        public async Task<ICollection<Exercise>> GetAllExercisesAsync()
        {
            return await _db.Exercises.OrderBy(e => e.Id).ToListAsync();
        }

        public async Task<Exercise> GetExerciseByIdAsync(int exerciseId)
        {
            return await _db.Exercises.FirstOrDefaultAsync(e => e.Id == exerciseId);
        }

        public async Task<ICollection<Set>> GetSetsForExercise(int exerciseId, int workoutId)
        {
            return await _db.Sets.Where(s => s.Exercise.Id == exerciseId && s.Workout.Id == workoutId).ToListAsync();
        }

        public async Task UpdateExerciseAsync(Exercise updatedExercise)
        {
            _db.Exercises.Update(updatedExercise);
            await _db.SaveChangesAsync();
        }
    }
}
