using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using FluentValidation;
using System.Linq;

namespace EvaluationSystem.Application.Validations.ModuleValidations
{
    public class UpdateModuleValidation : AbstractValidator<UpdateModuleDto>
    {
        public UpdateModuleValidation()
        {
            RuleFor(m => m.ModuleName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Must(BeAValidName);
        }

        public bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(char.IsLetter);
        }
    }
}
