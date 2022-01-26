using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Attestations;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationRepository : IRepository<Attestation>
    {
        void AddUserParticipantsToAttestation(int attestationId, int participantId, string position);

        void DeleteAttestation(int attestationId);

        ICollection<AttestationInfoDbDto> GetAllAttestation();

        ICollection<Attestation> GetAllAtestationByUserId(int userId);
    }
}
