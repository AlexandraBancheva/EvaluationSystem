using System.Collections.Generic;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IFormRepository : IRepository<FormTemplate>
    {
        void DeleteForm(int id);

        IEnumerable<FormTemplateDto> FormsWithModules();
    }
}
