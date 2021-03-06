using FluentValidation;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Validations.FormValidations
{
    public class UpdateFormValidation : AbstractValidator<UpdateFormDto>
    {
        public UpdateFormValidation()
        {
            RuleFor(f => f.FormName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Length(3, 200).WithMessage("{PropertyName} must be between 3 and 200 characters!");
        }
    }
}
