using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAppEmployee.Data.Interfaces.Repository;
using WebAppEmployee.Data.Interfaces.Service;
using WebAppEmployee.Domain.Models;

namespace WebAppEmployee.Domain.Services
{
    //private readonly IMapper _mapper;
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> GetByRegistrationNumber(int registrationNumber)
        {
            return await _employeeRepository.GetByRegistrationNumber(registrationNumber);
        }

        public async Task<Employee> Create(Employee employee)
        {
            return await _employeeRepository.Create(employee);
        }

        public Task<Employee> Update(Employee employee)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int registrationNumber, bool isExternalEmployee)
        {
            await _employeeRepository.Delete(registrationNumber, isExternalEmployee);
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _employeeRepository.GetAll();
        }
    }
}