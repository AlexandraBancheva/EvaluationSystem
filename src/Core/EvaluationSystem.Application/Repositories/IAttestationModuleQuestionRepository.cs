using EvaluationSystem.Application.Models.ModuleQuestions;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationModuleQuestionRepository : IRepository<AttestationModuleQuestion>
    {
        void AddNewQuestionToModule(int moduleId, int questionId, int position);

        ICollection<ModuleQuestionGettingAllQuestionIds> GetAllQuestionIdsByModuleId(int moduleId);

        void DeleteQuestionFromModule(int moduleId, int questionId);
    }
}
