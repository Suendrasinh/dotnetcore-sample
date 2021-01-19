using FluentValidation;
using MyGym.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGym.API.Validators
{
    public class SaveCustomerValidator : AbstractValidator<SaveCustomerRequest>
    {
        public SaveCustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().Length(0, 20);
            RuleFor(x => x.LastName).NotEmpty().Length(0, 20);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.DateOfBirth).InclusiveBetween(Convert.ToDateTime("01-Jan-1970"), Convert.ToDateTime("31-Dec-2015"));
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}
