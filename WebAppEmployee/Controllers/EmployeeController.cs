using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAppEmployee.Data.Dto;
using WebAppEmployee.Data.Interfaces.Service;
using WebAppEmployee.Domain.Models;

namespace WebAppEmployee.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet, Route("~/api/employee")]
        public async Task<IHttpActionResult> GetAll()
        {
            var employee = await _employeeService.GetAll();

            return Ok(employee);
        }

        [HttpGet, Route("~/api/employee/{registrationNumber}")]
        public async Task<IHttpActionResult> Get(int registrationNumber)
        {
            var employee = await _employeeService.GetByRegistrationNumber(registrationNumber);

            return Ok(employee);
        }

        [HttpPost, Route("~/api/employee")]
        public async Task<IHttpActionResult> CreateEmployee([FromBody] Employee employee)
        {
            var model = await _employeeService.Create(employee);

            return Ok(model);
        }

        [HttpPut, Route("~/api/employee")]
        public async Task<IHttpActionResult> UpdateEmployee([FromBody] EmployeeDto dto)
        {
            // need to use mappings
            var employee = new Employee()
            {
                RegistrationNumber = dto.RegistrationNumber,
                Birthday = dto.Birthday,
                FullName = dto.FullName,
                Gender = dto.Gender,
                IsExternalEmployee = dto.IsExternalEmployee,
                Position = new Position()
                {
                    Name = dto.PositionName,
                    BaseSalary = dto.BaseSalary
                }
            };

            var model = await _employeeService.Update(employee);

            return Ok(model);
        }

        [HttpDelete, Route("~/api/employee")]
        public async Task<IHttpActionResult> Delete(int registrationNumber)
        {
            await _employeeService.Delete(registrationNumber);
            return Ok();
        }
    }
}