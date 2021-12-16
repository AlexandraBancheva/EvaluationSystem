﻿using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IQuestionRepository : IRepository<QuestionTemplate>
    {
        ICollection<QuestionTemplate> GetAllQuestionsWithAnswers();

        void DeleteTemplateQuestion(int questionId);

        ICollection<QuestionTemplate> GetAllById(int questionId);

        ICollection<CheckQuestionNamesDto> GetAllQuestionNames();
    }
}