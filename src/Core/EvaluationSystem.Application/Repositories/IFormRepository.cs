using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Repositories
{
    public interface IFormRepository : IRepository<FormTemplate>
    {
        void DeleteForm(int formId);

        ICollection<FormWithAllDto> GetAllForms();

        ICollection<FormWithAllDto> GetAllByFormId(int formId);

        ICollection<CheckFormNameDto> GetAllFormNames();
    }
}