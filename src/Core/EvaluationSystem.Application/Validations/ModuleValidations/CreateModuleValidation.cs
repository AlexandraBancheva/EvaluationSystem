using EvaluationSystem.Application.Models.Modules;
using FluentValidation;
using System.Linq;

namespace EvaluationSystem.Application.Validations
{
    public class CreateModuleValidation : AbstractValidator<CreateModuleDto>
    {
        public CreateModuleValidation()
        {
            RuleFor(m => m.ModuleName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Length(3, 100).WithMessage("{PropertyName} must be between 3 and 100 characters!")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters!");
        }

        public bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(char.IsLetter);
        }
    }
}
