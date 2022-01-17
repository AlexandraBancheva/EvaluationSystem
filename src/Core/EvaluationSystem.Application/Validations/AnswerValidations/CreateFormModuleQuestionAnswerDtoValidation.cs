using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using FluentValidation;

namespace EvaluationSystem.Application.Validations.AnswerValidations
{
    public class CreateFormModuleQuestionAnswerDtoValidation : AbstractValidator<CreateFormModuleQuestionAnswerDto>
    {
        public CreateFormModuleQuestionAnswerDtoValidation()
        {
            RuleFor(v => v.AnswerText)
                 .NotEmpty().WithMessage("{PropertyName} cannot be empty!");
        }
    }
}
