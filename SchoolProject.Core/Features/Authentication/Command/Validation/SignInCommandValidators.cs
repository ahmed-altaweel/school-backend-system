using FluentValidation;
using SchoolProject.Core.Features.Authentication.Command.Models;

namespace SchoolProject.Core.Features.Authentication.Command.Validation
{
    public class SignInCommandValidators : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidators()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("The user name can't be empty")
                .NotNull().WithMessage("The user name can't be null")
                .MaximumLength(20).WithMessage("The user name can't be grater than 20 characters")
                .MinimumLength(1).WithMessage("The user name can't be less than 1 characters");
            RuleFor(x => x.Password).NotEmpty().WithMessage("The user name can't be empty")
              .NotNull().WithMessage("The user name can't be null")
              .MaximumLength(50).WithMessage("The user name can't be grater than 20 characters")
              .MinimumLength(1).WithMessage("The user name can't be less than 1 characters");
            // RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("The password and confirmPassword not matches");
        }

        public void ApplyCustomValidationRules()
        {

        }
    }
}
