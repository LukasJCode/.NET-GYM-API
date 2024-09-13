using Microsoft.AspNetCore.Identity;

namespace GymAppAPI.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<UserWorkout> UserWorkouts { get; set; }
    }
}
