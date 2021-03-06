using System.Collections.Generic;
using EvaluationSystem.Application.Models.UserAnswers;
using EvaluationSystem.Application.Models.AttestationForms;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IUserAnswersServices
    {
        void Create(CreateUserAnswerDto model);

        void DeleteUserAnswer(int attestationId);

        ICollection<AttestationFormDetailDto> GetAttestationAnswerByUser(int attestationId, string userEmail);
    }
}