using FluentValidation;
using EvaluationSystem.Application.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Validations.QuestionValidations
{
    public class CreateQuestionValidation : AbstractValidator<CreateQuestionDto>
    {
        public CreateQuestionValidation()
        {
            RuleFor(q => q.QuestionName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Length(3, 100).WithMessage("Length of {PropertyName} must be between 3 and 100 characters!");

            RuleFor(e => e.Type)
                .IsInEnum();
        }
    }
}
