using AutoMapper;
using HealthAPI.Dtos;
using HealthAPI.Models;

namespace HealthAPI.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<PatientForCreationDto, Patient>();
            CreateMap<PatientForUpdateDto, Patient>();
            CreateMap<UserForRegistrationDto, User>().ReverseMap();
        }
    }
}
