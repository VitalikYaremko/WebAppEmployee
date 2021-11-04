using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using WebAppEmployee.Data.Dto;
using WebAppEmployee.Data.Interfaces.Service;
using WebAppEmployee.Domain.Models;
using WebAppEmployee.Domain.Validations;

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

        [HttpPost, Route("~/api/employee/import")]
        public async Task<IHttpActionResult> ImportEmployees()
        {
            try
            {
                var sb = new StringBuilder();
                List<Employee> successEmployees = new List<Employee>();

                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];

                    using (StreamReader r = new StreamReader(postedFile.InputStream))
                    {
                        string json = r.ReadToEnd();
                        List<Employee> items = JsonConvert.DeserializeObject<List<Employee>>(json);

                        //validate items FluentValidation
                        var validator = new EmployeeValidator();
                        for (var i = 0 ; i < items.Count; i++)
                        {
                            var result = validator.Validate(items[i]);
                            var allMessages = result.ToString("~");
                            if (!string.IsNullOrEmpty(allMessages))
                            {
                                sb.AppendLine($"{i + 1} | Name: {items[i].FullName} | {allMessages}");
                            }
                            else
                            {
                                successEmployees.Add(items[i]);
                                sb.AppendLine($"{i + 1} | Name: {items[i].FullName} | Success !");
                            }
                        }
                    }
                }

                await _employeeService.BulkCreate(successEmployees);

                var base65 = Base64Encode(sb.ToString());

                // it would be better if return ResponseMessageResult to get more info on UI side
                return Ok(base65);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}