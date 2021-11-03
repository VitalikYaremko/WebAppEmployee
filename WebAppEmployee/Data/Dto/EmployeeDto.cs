using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppEmployee.Data.Dto
{
    public class EmployeeDto
    {
        public int RegistrationNumber { get; set; }
        public bool IsExternalEmployee { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string PositionName { get; set; }
        public decimal BaseSalary { get; set; }
    }
}