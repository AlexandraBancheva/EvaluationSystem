using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using FluentValidation;

namespace EvaluationSystem.Application.Validations.AnswerValidations
{
    public class UpdateAnswerDtoValidation : AbstractValidator<UpdateAnswerDto>
    {
        public UpdateAnswerDtoValidation()
        {
            RuleFor(a => a.AnswerText)
                .NotEmpty().WithMessage("{PropertyName} must not be empty!")
                .Length(1, 100).WithMessage("Length of {PropertyName} must be between 1 and 100 characters!");
        }
    }
}
