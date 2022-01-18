using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Validations.AnswerValidations;
using FluentValidation;
using System.Linq;

namespace EvaluationSystem.Application.Validations.QuestionValidations
{
    public class QuestionValidator : AbstractValidator<CreateFormModuleQuestionDto>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.QuestionName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Length(1, 100).WithMessage("Length of {PropertyName} must be between 1 and 100 characters!")
                .Must(BeAValidName);

            RuleForEach(q => q.Answers)
                .SetValidator(new AnswerValidator());
        }

        public bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(char.IsLetterOrDigit);
        }
    }
}
