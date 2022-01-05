using System.Collections.Generic;
using EvaluationSystem.Application.Models.Attestations;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAttestationsServices
    {
        AttestationDetailDto CreateAttestation(CreateAttestationDto model);

        void DeleteAttestation(int attestationId);

        ICollection<AttestationInfoDbDto> GetAllAttestations();
    }
}
