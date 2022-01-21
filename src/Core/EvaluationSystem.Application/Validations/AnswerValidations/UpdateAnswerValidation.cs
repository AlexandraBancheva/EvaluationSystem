﻿using FluentValidation;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Validations.AnswerValidations
{
    public class UpdateAnswerValidation : AbstractValidator<AnswerTemplate>
    {
        public UpdateAnswerValidation()
        {
            RuleFor(a => a.AnswerText)
                .NotEmpty().WithMessage("{PropertyName} must not be empty!")
                .Length(1, 100).WithMessage("Length of {PropertyName} must be between 1 and 100 characters!");
        }
    }
}
