using System.ComponentModel.DataAnnotations;

namespace GymAppAPI.Dto
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Exercise Name has to be atleast 5 characters long")]
        public string Name { get; set; }
    }
}
