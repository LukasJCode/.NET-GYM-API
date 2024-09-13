using System.ComponentModel.DataAnnotations;

namespace GymAppAPI.Dto
{
    public class WorkoutDto
    {
        public int Id { get; set; }
        [MinLength(5, ErrorMessage = "Workout Name has to be atleast 5 characters long")]
        public string Name { get; set; }
        public DateTime DateOfWorkout { get; set; }
    }
}
