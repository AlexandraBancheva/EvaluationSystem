using System.Collections.Generic;
using EvaluationSystem.Application.Models.Attestations;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAttestationsServices
    {
        void CreateAttestation(CreateAttestationDto model);

        void DeleteAttestation(int attestationId);

        IEnumerable<AttestationInfoDbDto> GetAllAttestations();
    }
}
