using EvaluationSystem.Application.Models.ModuleQuestions;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IModuleQuestionsServices
    {
        void AddQuestionToModule(int moduleId, int questionId, int position);

        void DeleteQuestionFromModule(int moduleId, int questionId);

        IEnumerable<ListModulesQuestionsDto> GetAllModulesWithAllQuestions();

        IEnumerable<ListModulesQuestionsDto> GetModuleWithAllQuestions(int moduleId);
    }
}