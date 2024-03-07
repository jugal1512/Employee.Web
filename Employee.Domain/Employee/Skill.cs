using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Employee
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string? SkillName { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeModel? Employee { get; set; }
    }
}
