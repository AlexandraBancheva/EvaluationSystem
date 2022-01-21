using System.Collections.Generic;
using EvaluationSystem.Application.Models.Attestations;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAttestationsServices
    {
        void DeleteAttestation(int attestationId);

        AttestationDetailDto CreateAttestation(CreateAttestationDto model);

        ICollection<AttestationInfoDbDto> GetAllAttestations();
    }
}