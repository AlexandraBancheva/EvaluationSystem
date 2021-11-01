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
                .Length(3, 100).WithMessage("Length of {PropertyName} must be between 3 and 100 characters!")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters!");

            // Validation for enums??
        }

        public bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(char.IsLetter);
        }
    }
}
