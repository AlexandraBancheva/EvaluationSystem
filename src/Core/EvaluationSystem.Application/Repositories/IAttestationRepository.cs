using EvaluationSystem.Application.Models.Attestations;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationRepository : IRepository<Attestation>
    {
        void AddUserParticipantsToAttestation(int attestationId, int participantId, string position);

        void DeleteAttestation(int attestationId);

        ICollection<AttestationInfoDbDto> GetAllAttestation();
    }
}
