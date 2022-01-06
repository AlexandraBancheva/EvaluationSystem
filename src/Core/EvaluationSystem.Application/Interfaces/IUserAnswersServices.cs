using EvaluationSystem.Application.Models.UserAnswers;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IUserAnswersServices
    {
        void Create(CreateUserAnswerDto model);
    }
}
