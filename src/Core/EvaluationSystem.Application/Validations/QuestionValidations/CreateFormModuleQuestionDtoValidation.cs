using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using FluentValidation;
using System.Linq;

namespace EvaluationSystem.Application.Validations.QuestionValidations
{
    public class CreateFormModuleQuestionDtoValidation : AbstractValidator<CreateFormModuleQuestionDto>
    {
        public CreateFormModuleQuestionDtoValidation()
        {
            RuleFor(v => v.QuestionName)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty!")
                .Length(3, 100).WithMessage("{PropertyName} must be between 3 and 100 characters!")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters!");

            RuleForEach(a => a.Answers).ChildRules(answers =>
                answers.RuleFor(b => b.AnswerText)
                .NotNull().WithMessage("{PropertyName} cannot be empty!")
                .Length(3, 200).WithMessage("{PropertyName} must be between 3 and 200 characters!")
                .Must(BeAValidName)
                );
        }

        public bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(char.IsLetterOrDigit);
        }
    }
}
