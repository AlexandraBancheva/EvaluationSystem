using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.ModuleQuestions;

namespace EvaluationSystem.Application.Repositories
{
    public interface IAttestationModuleQuestionRepository : IRepository<AttestationModuleQuestion>
    {
        void AddNewQuestionToModule(int moduleId, int questionId, int position);

        void DeleteQuestionFromModule(int moduleId, int questionId);

        ICollection<ModuleQuestionGettingAllQuestionIds> GetAllQuestionIdsByModuleId(int moduleId);
    }
}
