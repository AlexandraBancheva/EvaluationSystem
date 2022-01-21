using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationFormRepository : IRepository<AttestationForm>
    {
        void DeleteAttestationForm(int formId);

        ICollection<FormWithAllDto> GetAllByFormId(int formId);

        ICollection<CheckFormNameDto> GetAllFormNames();
    }
}
