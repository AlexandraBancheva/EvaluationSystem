using FluentValidation;
using EvaluationSystem.Application.Models.Modules;

namespace EvaluationSystem.Application.Validations
{
    public class CreateModuleValidation : AbstractValidator<CreateModuleDto>
    {
        public CreateModuleValidation()
        {
            RuleFor(m => m.ModuleName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Length(3, 100).WithMessage("{PropertyName} must be between 3 and 100 characters!");
        }
    }
}
