using EvaluationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAnswerRepository
    {
        void AddNewAnswer(int questionId, Answer model);

        IEnumerable<Answer> GetAllAnswersByQuestionId(int questionId);

        void DeleteAnAnswer(int answerId);
    }
}
