using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAppEmployee.Domain.Models;

namespace WebAppEmployee.Data.Interfaces.Service
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetByRegistrationNumber(int registrationNumber);
        Task<Employee> Create(Employee employee);
        Task<Employee> Update(Employee employee);
        Task Delete(int registrationNumber, bool isExternalEmployee);
    }
}