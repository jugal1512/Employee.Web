using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Employee
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeModel>> GetEmployees(string sortOrder);
        public Task<EmployeeModel> GetEmployeeById(int? id);
        public Task<EmployeeModel> AddEmployee(EmployeeModel employeeModel);
        public Task<EmployeeModel> DeleteEmployee(int? id);
        public Task<EmployeeModel> UpdateEmployee(int? id,EmployeeModel employeeModel);
        public Task<List<EmployeeModel>> SearchEmployee(string? searchString);
    }
}
