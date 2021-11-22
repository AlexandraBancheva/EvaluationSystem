namespace EvaluationSystem.Application.Interfaces
{
    public interface IModuleQuestionsServices
    {
        void AddQuestionToModule(int moduleId, int questionId, int position);

        void DeleteQuestionFromModule(int moduleId, int questionId);

        // GetAllModulesWithAllQuestions();
    }
}
