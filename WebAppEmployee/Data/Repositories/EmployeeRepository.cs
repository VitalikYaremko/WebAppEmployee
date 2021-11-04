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
                var employee = await context.Employees.Include(x => x.Position).FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber);
                return employee;
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
                var entity = await context.Employees.Include(x => x.Position).FirstOrDefaultAsync(x => x.RegistrationNumber == employee.RegistrationNumber);
                if (entity != null)
                {
                    // need to use Mapper
                    entity.Birthday = employee.Birthday;
                    entity.IsExternalEmployee = employee.IsExternalEmployee;
                    entity.FullName = employee.FullName;
                    entity.Gender = employee.Gender;
                    entity.ModifiedOn = DateTime.Now;
                    if (entity.Position == null)
                    {
                        entity.Position = new Position()
                        {
                            Name = employee.Position.Name,
                            BaseSalary = employee.Position.BaseSalary
                        };
                    }
                    else
                    {
                        entity.Position.Name = employee.Position.Name;
                        entity.Position.BaseSalary = employee.Position.BaseSalary;
                    }
                    // not good - need to use Id of Position for employee and change this Id, and if need to update some
                    //info in Position - update just Position
                    //var position = await context.Positions.FirstOrDefaultAsync(x => x.Id == employee.Position.Id);
                    //if (position != null)
                    //{
                    //    position.Name = employee.Position.Name;
                    //    position.BaseSalary = employee.Position.BaseSalary;
                    //}
                    //else
                    //{
                    //    context.Positions.Add(employee.Position);
                    //}

                    await context.SaveChangesAsync();
                }

                return entity;
            }
        }

        public async Task Delete(int registrationNumber)
        {
            using (var context = new WebAppContext())
            {
                var entity = await context.Employees.FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber);
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
                    var a = await context.Employees.Include(x => x.Position).Where(x => x.IsActive == true).ToListAsync();
                    return a;
                }

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<Employee>> BulkCreate(List<Employee> employees)
        {
            try
            {
                using (var context = new WebAppContext())
                {
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    foreach (var item in employees)
                    {
                        item.ModifiedOn = item.CreatedOn = DateTime.Now;
                        item.IsActive = true;
                        context.Employees.Add(item);
                    }

                    await context.SaveChangesAsync();
                    return employees;
                }
            }
            catch (Exception e)
            {

                throw;
            }
            
        }
    }
}