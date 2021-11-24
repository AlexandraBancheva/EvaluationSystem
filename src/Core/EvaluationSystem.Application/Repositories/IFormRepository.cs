using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IFormRepository : IRepository<FormTemplate>
    {
        void DeleteForm(int id);

        IEnumerable<FormTemplate> FormsWithModules();
    }
}
