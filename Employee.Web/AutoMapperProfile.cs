using AutoMapper;
using Employee.Domain.Employee;
using Employee.Web.Models;

namespace Employee.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeModel, EmployeeDto>()
                .ForMember(dest => dest.SkillName,opt => opt.MapFrom(src => string.Join(",",src.Skills.Select(x => x.SkillName).ToList())))
                .ForMember(dest => dest.Skills,opt => opt.MapFrom(src => src.Skills.Select(x => x.SkillName).ToList()));
            CreateMap<EmployeeDto, EmployeeModel>();
            CreateMap<Skill, SkillDto>();
            CreateMap<SkillDto, Skill>();
        }
    }
}
