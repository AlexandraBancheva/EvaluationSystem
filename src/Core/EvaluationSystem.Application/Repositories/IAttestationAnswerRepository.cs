using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationAnswerRepository : IRepository<AttestationAnswer>
    {
        ICollection<AttestationAnswer> GetAllByQuestionId(int questionId);
    }
}
