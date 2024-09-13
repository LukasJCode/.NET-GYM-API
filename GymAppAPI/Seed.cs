using GymAppAPI.Data;
using GymAppAPI.Models;
using System.Diagnostics.Metrics;

namespace GymAppAPI
{
    public class Seed
    {
        private readonly AppDbContext dataContext;
        public Seed(AppDbContext context)
        {
            this.dataContext = context;
        }
        //public void SeedDataContext()
        //{
        //    if (!dataContext.Workouts.Any())
        //    {
        //        var workouts = new List<Workout>()
        //        {
        //            new Workout
        //            {
        //                Name = "Chest Workout",
        //                DateOfWorkout = DateTime.Now,
        //                Exercises = new List<Exercise>
        //                {
        //                    new Exercise
        //                    { Id = 1, Name = "Bench Press", Sets = new List<Set>
        //                        {
        //                            new Set { Reps = 8, Weight = 40},
        //                            new Set { Reps = 8, Weight = 50},
        //                            new Set { Reps = 8, Weight = 60}
        //                        }
        //                    },
        //                    new Exercise
        //                    { Id = 1, Name = "Incline Bench Press", Sets = new List<Set>
        //                        {
        //                            new Set { Reps = 8, Weight = 40},
        //                            new Set { Reps = 8, Weight = 50},
        //                            new Set { Reps = 8, Weight = 60}
        //                        }
        //                    }
        //                }
        //            },
        //            new Workout
        //            {
        //                Name = "Back Workout",
        //                DateOfWorkout = DateTime.Now,
        //                Exercises = new List<Exercise>
        //                {
        //                    new Exercise
        //                    { Id = 1, Name = "Lat Pulldown", Sets = new List<Set>
        //                        {
        //                            new Set { Reps = 8, Weight = 40},
        //                            new Set { Reps = 8, Weight = 50},
        //                            new Set { Reps = 8, Weight = 60}
        //                        }
        //                    },
        //                    new Exercise
        //                    { Id = 1, Name = "Pull Ups", Sets = new List<Set>
        //                        {
        //                            new Set { Reps = 8, Weight = 0},
        //                            new Set { Reps = 8, Weight = 0},
        //                            new Set { Reps = 8, Weight = 0}
        //                        }
        //                    }
        //                }
        //            }
        //        };
        //        dataContext.Workouts.AddRange(workouts);
        //        dataContext.SaveChanges();
        //    }
        //}
    }
}
