using AutoMapper;
using EmployeeMgtSystemAPI.DTO;
using EmployeeMgtSystemAPI.Models;

namespace EmployeeMgtSystemAPI.Helper
{
    public class Mapping:Profile
    {
        public Mapping() 
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
