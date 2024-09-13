using System.ComponentModel.DataAnnotations;

namespace GymAppAPI.Dto
{
    public class SetDto
    {
        public int Id { get; set; }
        [Required]
        [Range(0, 1000)]
        public float Weight { get; set; }
        [Required]
        [Range(0, 1000)]
        public int Reps { get; set; }
    }
}
