using AutoMapper;
using GymAppAPI.Dto;
using GymAppAPI.Models;

namespace GymAppAPI.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() 
        { 
            CreateMap<Workout, WorkoutDto>().ReverseMap();
            CreateMap<Exercise, ExerciseDto>().ReverseMap();
            CreateMap<Set, SetDto>().ReverseMap();
        }
    }
}
