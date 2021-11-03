using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppEmployee.Domain.Models;

namespace WebAppEmployee.Data.Interfaces.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetByRegistrationNumber(int registrationNumber);
        Task<Employee> Create(Employee employee);
        Task<Employee> Update(Employee employee);
        Task Delete(int registrationNumber);

    }
}
