using Employee.Domain.Employee;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.EF.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<EmployeeModel> AddEmployee(EmployeeModel employeeModel)
        {
            await _employeeDbContext.Employees.AddAsync(employeeModel);
            await _employeeDbContext.SaveChangesAsync();
            return employeeModel;
        }

        public async Task<EmployeeModel> DeleteEmployee(int? id)
        {
            var deleteEmployee = await GetEmployeeById(id);
            _employeeDbContext.Employees.Remove(deleteEmployee);
            await _employeeDbContext.SaveChangesAsync();
            return deleteEmployee;
        }

        public async Task<EmployeeModel> GetEmployeeById(int? id)
        {
            return await _employeeDbContext.Employees.AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            return await _employeeDbContext.Employees.AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeModel> UpdateEmployee(int? id, EmployeeModel employeeModel)
        {
            var updateEmployee = await GetEmployeeById(id);
            employeeModel.Id = updateEmployee.Id;
            _employeeDbContext.Employees.Update(employeeModel);
            await _employeeDbContext.SaveChangesAsync();
            return employeeModel;
        }
    }
}
