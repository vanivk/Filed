using Filed.Payment.API.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filed.Payment.API.CommonUtils
{
	public class PaymentValidator : AbstractValidator<PaymentDto>
	{
        public PaymentValidator()
        {
            RuleFor(m => m.CreditCardNumber).Must(m => Utilities.IsCardNumberValid(m)).WithMessage("Invalid credit card number");
            RuleFor(m => m.CardHolder).NotEmpty();
            RuleFor(m => m.ExpirationDate).Must(m => Utilities.DateFormatValidator(m)).WithMessage("Date format must be mm/dd/yyyy and it cannot be in the past");
            RuleFor(m => m.SecurityCode).MinimumLength(3).MaximumLength(3).WithMessage("Must be of length 3");
            RuleFor(m => m.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
        }

        public ICollection<string> GetErrorList(IList<FluentValidation.Results.ValidationFailure> errorList)
        {
            List<string> errors = new List<string>();
            foreach (var error in errorList)
            {
                errors.Add(error.ErrorMessage);
            }

            return errors;
        }
    }
}
