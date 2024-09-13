using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace GymAppAPI.Models
{
    public class Workout
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime DateOfWorkout { get; set; }
        public ICollection<WorkoutExercise> ?WorkoutExercises { get; set; }
        public ICollection<UserWorkout> UserWorkouts { get; set; }


    }
}
