using FluentValidation;
using EvaluationSystem.Application.Validations.AnswerValidations;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Validations.QuestionValidations
{
    public class QuestionValidator : AbstractValidator<CreateFormModuleQuestionDto>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.QuestionName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Length(1, 100).WithMessage("Length of {PropertyName} must be between 1 and 100 characters!");

            RuleForEach(q => q.Answers)
                .SetValidator(new AnswerValidator());
        }
    }
}
