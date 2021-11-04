using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppEmployee.Domain.Models;

namespace WebAppEmployee.Domain.Validations
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(employee => employee.Birthday).LessThan(new DateTime(2012, 1, 1));
            RuleFor(employee => employee.FullName).NotEmpty();
            RuleFor(employee => employee.Gender).NotEmpty();
            RuleFor(employee => employee.Position).SetValidator(new PositionValidator());
        }
    }
}