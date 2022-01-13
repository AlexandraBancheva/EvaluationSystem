using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IUserAnswerRepository : IRepository<UserAnswer>
    {
        void ChangeStatusToDone(int attestationId, int idUserParticipant);

        ICollection<UserAnswer> GetAllAnswersByUser(int attestationId, int userId);

        void DeleteUserAnswerByAttestationId(int attestationId);
    }
}