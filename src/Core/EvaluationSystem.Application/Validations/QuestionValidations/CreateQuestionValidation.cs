using EvaluationSystem.Application.Questions.QuestionsDtos;
using FluentValidation;
using System.Linq;

namespace EvaluationSystem.Application.Validations.QuestionValidations
{
    public class CreateQuestionValidation : AbstractValidator<CreateQuestionDto>
    {
        public CreateQuestionValidation()
        {
            RuleFor(q => q.QuestionName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Length(1, 100).WithMessage("Length of {PropertyName} must be between 1 and 100 characters!")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters!");

            // Validation for enums??
            //RuleFor(e => e.Type)
            //    .IsInEnum();
        }

        public bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(char.IsLetter);
        }
    }
}
