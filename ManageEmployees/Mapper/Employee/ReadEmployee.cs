using AutoMapper;

namespace ManageEmployees.Mapper.Employee
{
    public class ReadEmployee : Profile
    {
        public ReadEmployee()
        {
            CreateMap<Entities.Employee, Dtos.Employee.ReadEmployee>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            .ReverseMap();
        }
    }
}