using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationAnswerRepository : IRepository<AttestationAnswer>
    {
        ICollection<AttestationAnswer> GetAllByQuestionId(int questionId);
    }
}
