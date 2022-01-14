using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationParticipantRepository : IRepository<AttestationParticipant>
    {
        ICollection<AttestationParticipant> GetAllAttestationParticipant(int userId);

        void DeleteAttestationIdFromAttestationParticipant(int userId, int attestationId);
    }
}
