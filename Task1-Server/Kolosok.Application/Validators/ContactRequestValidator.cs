using FluentValidation;
using Kolosok.Application.Contracts.Contacts;

namespace Kolosok.Application.Validators;

public class ContactValidator : AbstractValidator<ContactRequest>
{
    public ContactValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(c => c.MiddleName)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(c => c.Address)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(c => c.Phone)
            .NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(c => c.DateOfBirth)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Must(BeAValidDate).WithMessage("{PropertyName} must be a valid date.");
    }

    private static bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}
