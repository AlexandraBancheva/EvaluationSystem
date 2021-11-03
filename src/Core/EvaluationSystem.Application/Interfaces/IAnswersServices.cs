using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAnswersServices
    {
        AnswerDetailDto AddNewAnswer(int questionId, AddNewAnswerDto model);

        IEnumerable<ListAnswersByQuestionId> GetAnswersByQuestionId(int questionId);

        void DeleteAnAnswer(int questionId, int answerId);
    }
}
