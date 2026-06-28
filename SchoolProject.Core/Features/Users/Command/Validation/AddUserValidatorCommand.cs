using FluentValidation;
using SchoolProject.Core.Features.Users.Command.Models;

namespace SchoolProject.Core.Features.Users.Command.Validation
{
    public class AddUserValidatorCommand : AbstractValidator<AddUserCommand>
    {
        public AddUserValidatorCommand()
        {
            ApplyValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("The user name can't be empty")
                .NotNull().WithMessage("The user name can't be null")
                .MaximumLength(20).WithMessage("The user name can't be grater than 20 characters")
                .MinimumLength(1).WithMessage("The user name can't be less than 1 characters");
            RuleFor(x => x.Email).NotEmpty().WithMessage("The Email can't be empty")
              .NotNull().WithMessage("The Email can't be null")
              .MaximumLength(40).WithMessage("The user name can't be grater than 20 characters")
              .MinimumLength(1).WithMessage("The user name can't be less than 1 characters");
            RuleFor(x => x.Password).NotEmpty().WithMessage("The user name can't be empty")
              .NotNull().WithMessage("The user name can't be null")
              .MaximumLength(50).WithMessage("The user name can't be grater than 20 characters")
              .MinimumLength(1).WithMessage("The user name can't be less than 1 characters");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("The password and confirmPassword not matches");
        }

        public void ApplyCustomValidationRules()
        {

        }
    }
}
