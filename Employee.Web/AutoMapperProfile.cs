using AutoMapper;
using Employee.Domain.Employee;
using Employee.Web.Models;

namespace Employee.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeModel,EmployeeDto>().ReverseMap();
        }
    }
}
