using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationParticipantRepository : IRepository<AttestationParticipant>
    {
        void DeleteAttestationIdFromAttestationParticipant(int userId, int attestationId);

        ICollection<AttestationParticipant> GetAllAttestationParticipant(int userId);
    }
}
