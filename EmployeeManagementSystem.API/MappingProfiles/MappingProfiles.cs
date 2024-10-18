using AutoMapper;
using EmployeeManagementSystem.API.DTOs;
using EmployeeManagementSystem.Core.Entities;


namespace EmployeeManagementSystem.API.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap(); ;
        }
    }
}
