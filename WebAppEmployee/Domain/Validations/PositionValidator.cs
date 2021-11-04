using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppEmployee.Domain.Models;

namespace WebAppEmployee.Domain.Validations
{
    public class PositionValidator : AbstractValidator<Position>
    {
        public PositionValidator()
        {
            RuleFor(position => position.Name).NotEmpty();
            RuleFor(position => position.BaseSalary).GreaterThan(0);
        }
    }
}