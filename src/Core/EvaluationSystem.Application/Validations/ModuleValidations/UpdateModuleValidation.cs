using FluentValidation;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Validations.ModuleValidations
{
    public class UpdateModuleValidation : AbstractValidator<UpdateModuleDto>
    {
        public UpdateModuleValidation()
        {
            RuleFor(m => m.ModuleName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!");
        }
    }
}
