namespace GymAppAPI.Models
{
    public class Set
    {
        public int Id { get; set; }
        public float Weight { get; set; }
        public int Reps { get; set; }
        public Workout Workout { get; set; }
        public Exercise Exercise { get; set; }
    }
}
