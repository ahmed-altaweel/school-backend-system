using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Contracts;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class AddStudentValidatorCommand : AbstractValidator<AddStudentCommand>
    {

        private readonly IStudentService _student;

        public AddStudentValidatorCommand(IStudentService student)
        {
            _student = student;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        public AddStudentValidatorCommand()
        {

        }



        public void ApplyValidationRules()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The user name can't be empty")
                .NotNull().WithMessage("The user name can't be null")
                .MaximumLength(20).WithMessage("The user name can't be grater than 20 characters")
                .MinimumLength(1).WithMessage("The user name can't be less than 1 characters");
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (key, CancellationToken) => !await _student.IsNameExist(key))
                .WithMessage("The user name must be unique");
        }

    }
}
