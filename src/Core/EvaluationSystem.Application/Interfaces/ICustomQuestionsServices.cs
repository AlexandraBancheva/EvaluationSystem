using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface ICustomQuestionsServices
    {
        int CreateNewQuestion(CreateQuestionDto model);

        QuestionDetailDto GetQuestionById(int questionId);
    }
}
