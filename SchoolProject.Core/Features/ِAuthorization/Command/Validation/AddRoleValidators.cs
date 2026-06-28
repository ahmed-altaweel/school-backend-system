using FluentValidation;
using SchoolProject.Core.Features._ِAuthorization.Command.Models;

namespace SchoolProject.Core.Features._ِAuthorization.Command.Validation
{
    public class AddRoleValidators : AbstractValidator<AddRoleCommand>
    {
        public AddRoleValidators()
        {
            ApplyValidatoinRules();
        }

        public void ApplyValidatoinRules()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("The role name can't be empty")
                .NotNull().WithMessage("the role name can't be null");
        }
    }
}
