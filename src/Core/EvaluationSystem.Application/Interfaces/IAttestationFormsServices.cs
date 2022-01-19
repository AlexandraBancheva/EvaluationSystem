using EvaluationSystem.Application.Models.AttestationQuestions;
using EvaluationSystem.Application.Models.Forms;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAttestationFormsServices
    {
        int CreateNewForm(CreateFormDto form);

        void DeleteFormFromAttestation(int formId);

        void UpdateUserAnswer(int attestationId, AttestationQuestionUpdateDto model);

        ICollection<FormDetailDto> GetFormById(int formId);
    }
}
