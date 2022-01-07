using System.Collections.Generic;
using EvaluationSystem.Application.Models.AttestationForms;
using EvaluationSystem.Application.Models.UserAnswers;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IUserAnswersServices
    {
        void Create(CreateUserAnswerDto model);

        ICollection<AttestationFormDetailDto> GetAttestationAnswerByUser(int attestationId, string userEmail);
    }
}
