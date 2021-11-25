using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;

namespace EvaluationSystem.Application.Repositories
{
    public interface IModuleQuestionRepository : IRepository<ModuleQuestion>
    {
        void AddNewQuestionToModule(int moduleId, int questionId, int position);

        void DeleteQuestionFromModule(int moduleId, int questionId);

        ICollection<ModuleTemplateDto> GetModuleWithAllQuestions();
    }
}