﻿using System.Collections.Generic;
using EvaluationSystem.Application.Models.Questions;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAnswersServices
    {
        void DeleteAnAnswer(int answerId);

        void UpdateAnswer(int questionId, int answerId, UpdateAnswerDto model);

        ICollection<AnswerListDto> CreateAnswer(int questionId, AddListAnswers model);

        ICollection<AnswerListDto> CreateAnswerTemplates(int formId, int moduleId, int questionId, AddListAnswers model);

        void UpdateAnswerTemplate(int formId, int moduleId, int questionId, int answerId, UpdateAnswerDto model);
    }
}
