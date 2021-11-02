using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
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

        [HttpGet, Route("~/api/employee/{id}")]
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
        public async Task<IHttpActionResult> UpdateEmployee(int id, [FromBody] string value)
        {
            return Ok();
        }

        [HttpDelete, Route("~/api/employee")]
        public async Task<IHttpActionResult> Delete(int registrationNumber, bool isExternalEmployee)
        {
            await _employeeService.Delete(registrationNumber, isExternalEmployee);
            return Ok();
        }
    }
}