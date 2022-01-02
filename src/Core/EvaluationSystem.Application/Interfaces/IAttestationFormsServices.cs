using System.Collections.Generic;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAttestationFormsServices
    {
        void CreateNewForm(CreateFormDto form);

        void DeleteFormFromAttestation(int formId);
    }
}
