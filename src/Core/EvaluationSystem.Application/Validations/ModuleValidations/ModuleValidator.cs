using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using EvaluationSystem.Application.Validations.QuestionValidations;
using FluentValidation;
using System.Linq;

namespace EvaluationSystem.Application.Validations.ModuleValidations
{
    public class ModuleValidator : AbstractValidator<CreateFormModuleDto>
    {
        public ModuleValidator()
        {
            RuleFor(x => x.ModuleName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Length(3, 100).WithMessage("{PropertyName} must be between 3 and 100 characters!")
                .Must(BeAValidName);

            RuleForEach(x => x.Question).SetValidator(new QuestionValidator());
        }

        public bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(char.IsLetterOrDigit);
        }
    }
}
