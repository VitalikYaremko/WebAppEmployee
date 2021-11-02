using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAppEmployee.Data.Context;
using WebAppEmployee.Data.Interfaces.Repository;
using WebAppEmployee.Domain.Models;

namespace WebAppEmployee.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public async Task<Employee> GetByRegistrationNumber(int registrationNumber)
        {
            using (var context = new WebAppContext())
            {
                return await context.Employees.FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber);
            }
        }

        public async Task<Employee> Create(Employee employee)
        {
            using (var context = new WebAppContext())
            {
                employee.ModifiedOn = employee.CreatedOn = DateTime.Now;
                employee.IsActive = true;

                context.Employees.Add(employee);
                await context.SaveChangesAsync();
                return await GetByRegistrationNumber(employee.RegistrationNumber);
            }
        }

        public async Task<Employee> Update(Employee employee)
        {
            using (var context = new WebAppContext())
            {

                var entity = await context.Employees.FirstOrDefaultAsync(x => x.RegistrationNumber == employee.RegistrationNumber
                                                                            && x.IsExternalEmployee == employee.IsExternalEmployee);
                if (entity != null)
                {
                    // need to use Mapper I didnt have time for create mappings ...
                    entity.Birthday = employee.Birthday;
                    entity.FullName = employee.FullName;
                    entity.Gender = employee.Gender;
                    entity.Position = employee.Position;
                    entity.ModifiedOn = DateTime.Now;

                    await context.SaveChangesAsync();
                }

                return entity;
            }
        }

        public async Task Delete(int registrationNumber, bool isExternalEmployee)
        {
            using (var context = new WebAppContext())
            {
                var entity = await context.Employees.FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber
                                                                            && x.IsExternalEmployee == isExternalEmployee);
                if (entity != null)
                {
                    entity.IsActive = false;
                    await context.SaveChangesAsync();
                }
                else
                {
                    // need to throw an exception
                }
            }
        }

        public async Task<List<Employee>> GetAll()
        {
            try
            {
                using (var context = new WebAppContext())
                {
                    // need to use pagination 
                    var a = await context.Employees.Where(x => x.IsActive == true).ToListAsync();
                    return a;
                }

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}