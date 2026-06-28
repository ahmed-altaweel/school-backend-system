using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Contracts;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class EditStudentValidatorCommand : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentService _student;
        public EditStudentValidatorCommand(IStudentService student)
        {
            _student = student;
            ApplayValidationRules();
        }


        void ApplayValidationRules()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("the user name can't be empty")
                .NotNull().WithMessage("the user name can't be null")
                .MustAsync(async (model, key, CancellationToken) => await _student.CheckIsExist(key, model.Id))
                .WithMessage("The user name must be unique");
        }
    }
}
