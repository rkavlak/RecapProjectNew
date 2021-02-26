using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator:AbstractValidator<Car>
    {
        public CarValidator()
        {

            RuleFor(c => c.BrandId).NotEmpty();
            RuleFor(c => c.DailyPrice).NotEmpty();
            RuleFor(c => c.DailyPrice).GreaterThan(450).When(c => c.CarId == 1).WithMessage("The Car daily price should not be lower then 450");
            RuleFor(c => c.DailyPrice).GreaterThan(500).When(c => c.CarId == 2).WithMessage("The Car daily price should not be lower then 500");
            RuleFor(c => c.DailyPrice).GreaterThan(250).When(c => c.CarId == 3).WithMessage("The Car daily price should not be lower then 250");
            RuleFor(c => c.DailyPrice).GreaterThan(320).When(c => c.CarId == 4).WithMessage("The Car daily price should not be lower then 320");
            RuleFor(c => c.Description).MinimumLength(2);
        }
    }
}
