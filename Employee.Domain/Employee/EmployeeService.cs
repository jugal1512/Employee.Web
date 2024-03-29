﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Domain.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeModel> AddEmployee(EmployeeModel employeeModel)
        {
            return await _employeeRepository.AddEmployee(employeeModel);    
        }

        public async Task<EmployeeModel> DeleteEmployee(int? id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }

        public async Task<EmployeeModel> GetEmployeeById(int? id)
        {
            return await _employeeRepository.GetEmployeeById(id);
        }

        public async Task<List<EmployeeModel>> GetEmployees(string sortOrder)
        {
            return await _employeeRepository.GetEmployees(sortOrder);
        }

        public Task<List<EmployeeModel>> SearchEmployee(string? searchString)
        {
            return _employeeRepository.SearchEmployee(searchString);
        }

        public async Task<EmployeeModel> UpdateEmployee(int? id, EmployeeModel employeeModel)
        {
            return await _employeeRepository.UpdateEmployee(id,employeeModel);
        }
    }
}
