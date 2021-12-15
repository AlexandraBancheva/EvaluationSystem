using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface ICustomQuestionsServices
    {
        int CreateNewQuestion(int moduleId, int position, CreateQuestionDto model);

        QuestionDetailDto GetCustomQuestionById(int questionId);

        void DeleteCustomQuestion(int questionId);
    }
}
