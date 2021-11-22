using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Application.Services
{
    public class ModuleQuestionsServices : IModuleQuestionsServices
    {
        private readonly IModuleQuestionRepository _moduleQuestionRepository;

        public ModuleQuestionsServices(IModuleQuestionRepository moduleQuestionRepository)
        {
            _moduleQuestionRepository = moduleQuestionRepository;
        }

        public void AddQuestionToModule(int moduleId, int questionId, int position)
        {
            _moduleQuestionRepository.AddNewQuestionToModule(moduleId, questionId, position);
        }

        public void DeleteQuestionFromModule(int moduleId, int questionId)
        {
            _moduleQuestionRepository.DeleteQuestionFromModule(moduleId, questionId);
        }
    }
}
