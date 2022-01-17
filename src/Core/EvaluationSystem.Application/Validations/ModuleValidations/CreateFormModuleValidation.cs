using System.Linq;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using FluentValidation;

namespace EvaluationSystem.Application.Validations.ModuleValidations
{
    public class CreateFormModuleValidation : AbstractValidator<CreateFormModuleDto>
    {
        public CreateFormModuleValidation()
        {
            RuleFor(m => m.ModuleName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Length(3, 100).WithMessage("{PropertyName} must be between 3 and 100 characters!")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters!");

            RuleForEach(q => q.Question).ChildRules(questions =>
                questions.RuleFor(a => a.QuestionName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Length(3, 200).WithMessage("{PropertyName} must be between 3 and 200 characters!")
                .Must(BeAValidName));
        }

        public bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(char.IsLetterOrDigit);
        }
    }
}
