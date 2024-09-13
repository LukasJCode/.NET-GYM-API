namespace GymAppAPI.Models
{
    public class WorkoutExercise
    {
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
        public Workout Workout { get; set; }
        public Exercise Exercise { get; set; }
    }
}
