using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAnswersServices
    {
        AnswerDetailDto AddNewAnswer(int questionId, AddNewAnswerDto model);

        void DeleteAnAnswer(int answerId);

        AnswerDetailDto UpdateAnswer(int questionId, int answerId, UpdateAnswerDto model);
    }
}
