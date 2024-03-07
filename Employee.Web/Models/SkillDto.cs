using Employee.Domain.Employee;

namespace Employee.Web.Models
{
    public class SkillDto
    {
        public int SkillId { get; set; }
        public string? SkillName { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeModel? Employee { get; set; }
    }
}
