using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using FluentValidation;

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
