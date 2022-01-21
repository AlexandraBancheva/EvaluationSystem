using FluentValidation;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Validations.AnswerValidations
{
    public class CreateAnswerValidaton : AbstractValidator<AddNewAnswerDto>
    {
        public CreateAnswerValidaton()
        {
            RuleFor(a => a.AnswerText)
                .NotEmpty().WithMessage("{PropertyName} must not be empty!")
                .Length(1, 100).WithMessage("Length of {PropertyName} must be between 1 and 100 characters!");
        }
    }
}
