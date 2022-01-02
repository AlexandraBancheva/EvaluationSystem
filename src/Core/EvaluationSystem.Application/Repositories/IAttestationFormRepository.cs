using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationFormRepository : IRepository<AttestationForm>
    {
        void DeleteAttestationForm(int formId);

        ICollection<FormWithAllDto> GetAllByFormId(int formId);

        ICollection<CheckFormNameDto> GetAllFormNames();
    }
}
