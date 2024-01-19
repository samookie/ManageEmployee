using AutoMapper;
using ManageEmployees.Entities;


namespace ManageEmployees.Mapper.Employee
{
    /// <summary>
    /// Mapper pour la methode CreateEmployee
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class CreateEmployee : Profile
    {
        /// <summary>
        /// Initializes a new mapper of the <see cref="CreateEmployee"/> class.
        /// </summary>
        public CreateEmployee() 
        {
            CreateMap<Entities.Employee, Dtos.Employee.CreateEmployee>()
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.BirthDate))
            .ReverseMap();
        }
    }
}
