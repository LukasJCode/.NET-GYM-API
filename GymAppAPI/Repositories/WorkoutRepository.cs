using AutoMapper;
using GymAppAPI.Data;
using GymAppAPI.Helper;
using GymAppAPI.Interfaces;
using GymAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GymAppAPI.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly AppDbContext _db;
        public WorkoutRepository(AppDbContext db)
        {
            this._db = db;
        }

        public async Task AddWorkoutAsync(Workout workout)
        {
            await _db.Workouts.AddAsync(workout);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteWorkoutAsync(int workoutId)
        {
            var workout = await _db.Workouts.Where(w => w.Id == workoutId).FirstOrDefaultAsync();
            _db.Workouts.Remove(workout);

            await _db.SaveChangesAsync();
        }

        // ??
        public async Task<ICollection<Workout>> GetAllWorkoutsAsync(QueryObject query)
        {
            var workouts = _db.Workouts.AsQueryable();
            //Filtering
            if(!string.IsNullOrWhiteSpace(query.WorkoutName))
            {
                workouts = workouts.Where(w => w.Name.Contains(query.WorkoutName));
            }

            //Sorting
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    workouts = query.IsDecsending ? workouts.OrderByDescending(w => w.Name) : workouts.OrderBy(w => w.Name);
                }
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    workouts = query.IsDecsending ? workouts.OrderByDescending(w => w.DateOfWorkout) : workouts.OrderBy(w => w.DateOfWorkout);
                }
            }
            //Pagination
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await workouts.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        // ??
        public async Task<ICollection<Exercise>> GetExercisesForWorkoutAsync(int workoutId)
        {
            return await _db.WorkoutExercises.Where(we => we.WorkoutId == workoutId).Select(e => e.Exercise).ToListAsync();
        }

        // ??
        public async Task<Workout> GetWorkoutByIdAsync(int workoutId)
        {
            return await _db.Workouts.FirstOrDefaultAsync(w => w.Id == workoutId);
        }

        public async Task UpdateWorkoutAsync(Workout updatedWorkout)
        {
            _db.Workouts.Update(updatedWorkout);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> WorkoutExistsAsync(int workoutId)
        {
            if(await _db.Workouts.AnyAsync(w => w.Id == workoutId))
                return true;
            return false;
        }
    }
}
