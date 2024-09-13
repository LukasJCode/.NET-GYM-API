﻿namespace GymAppAPI.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<WorkoutExercise> ?WorkoutExercises { get; set; }
        public ICollection<Set> Sets { get; set; }
    }
}
