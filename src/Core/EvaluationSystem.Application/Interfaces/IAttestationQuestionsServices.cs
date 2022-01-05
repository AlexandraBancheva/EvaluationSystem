using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAttestationQuestionsServices
    {
        int CreateNewQuestion(int moduleId, int position, CreateQuestionDto model); //  

        void DeleteAttestationQuestion(int questionId);

        CustomQuestionDetailDto GetCustomQuestionById(int questionId);
    }
}