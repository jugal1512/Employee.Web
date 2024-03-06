using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Employee
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Designation { get; set; }
        public string? DOB { get; set; }
        public string? JoiningDate { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
    }
}
