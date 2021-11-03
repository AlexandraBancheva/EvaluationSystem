﻿using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using FluentValidation;

namespace EvaluationSystem.Application.Validations.AnswerValidations
{
    public class CreateAnswerValidaton : AbstractValidator<AddNewAnswerDto>
    {
        public CreateAnswerValidaton()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("{PropertyName} must not be empty!")
                .Length(3, 100).WithMessage("Length of {PropertyName} must be between 3 and 100 characters!");
        }
    }
}
