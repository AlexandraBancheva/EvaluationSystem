using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface ICustomQuestionsServices
    {
        void DeleteCustomQuestion(int questionId);

        CustomQuestionDetailDto CreateNewQuestion(int moduleId, int position, CreateQuestionDto model);

        CustomQuestionDetailDto GetCustomQuestionById(int questionId);
    }
}
