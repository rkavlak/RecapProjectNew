using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator:AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r => r.RentDate).GreaterThanOrEqualTo(DateTime.Now).WithMessage("You can not rent for past days");
            RuleFor(r => r.CarId).NotEmpty().WithMessage("You can not rent without Car Id");
            RuleFor(r => r.ReturnDate).GreaterThanOrEqualTo(r => r.RentDate).WithMessage("Rent date must be earlier than return date");




        }
    }
}
