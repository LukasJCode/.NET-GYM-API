using System.ComponentModel.DataAnnotations;

namespace GymAppAPI.Models
{
    public class UserWorkout
    {
        public string AppUserId { get; set; }
        public int WorkoutId { get; set; }
        public AppUser AppUser { get; set; }
        public Workout Workout { get; set; }
    }
}
