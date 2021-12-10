using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAnswersServices
    {
        void AddNewAnswer(int questionId, AddListAnswers model);

        void DeleteAnAnswer(int answerId);

        void UpdateAnswer(int questionId, int answerId, UpdateAnswerDto model);
    }
}
