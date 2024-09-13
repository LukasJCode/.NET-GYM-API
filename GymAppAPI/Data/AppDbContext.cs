using GymAppAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymAppAPI.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        { 
            
        }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<UserWorkout> UserWorkouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //many to many connections
            modelBuilder.Entity<WorkoutExercise>().HasKey(we => new
            {
                we.ExerciseId,
                we.WorkoutId
            });

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(w => w.Workout)
                .WithMany(we => we.WorkoutExercises)
                .HasForeignKey(w => w.WorkoutId);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(e => e.Exercise)
                .WithMany(we => we.WorkoutExercises)
                .HasForeignKey(e => e.ExerciseId);

            modelBuilder.Entity<UserWorkout>().HasKey(uw => new
            {
                uw.AppUserId,
                uw.WorkoutId
            });

            modelBuilder.Entity<UserWorkout>()
                .HasOne(u => u.AppUser)
                .WithMany(uw => uw.UserWorkouts)
                .HasForeignKey(uw => uw.AppUserId);

            modelBuilder.Entity<UserWorkout>()
                .HasOne(u => u.Workout)
                .WithMany(uw => uw.UserWorkouts)
                .HasForeignKey(uw => uw.WorkoutId);

            base.OnModelCreating(modelBuilder);


            
            //identity roles
            List<IdentityRole> roles = new List<IdentityRole> 
            { 
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }



    }
}
