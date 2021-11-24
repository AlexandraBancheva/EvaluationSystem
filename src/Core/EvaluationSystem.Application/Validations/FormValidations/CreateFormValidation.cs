using System.Linq;
using FluentValidation;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Validations.FormValidations
{
    public class CreateFormValidation : AbstractValidator<CreateFormDto>
    {
        public CreateFormValidation()
        {
            RuleFor(f => f.FormName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Length(3, 200).WithMessage("{PropertyName} must be between 3 and 200 characters!")
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
