using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationQuestionRepository : IRepository<AttestationQuestion>
    {
        void DeleteQuestion(int questionId);

        ICollection<AttestationQuestion> GetAllQuestionsWithAnswers();

        ICollection<AttestationQuestion> GetAllById(int questionId);
    }
}