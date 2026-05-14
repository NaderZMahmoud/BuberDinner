using FluentValidation;

namespace BuberDinner.Application.Authentication.Commands.Register
{
        public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
        {
            public RegisterCommandValidator()
            {
                RuleFor(x => x.FirstName)
                    .NotEmpty()
                    .MinimumLength(2);

                RuleFor(x => x.LastName)
                    .NotEmpty()
                    .MinimumLength(2);

                RuleFor(x => x.Email)
                    .NotEmpty()
                    .EmailAddress();

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .MinimumLength(6);
            }
        }
}