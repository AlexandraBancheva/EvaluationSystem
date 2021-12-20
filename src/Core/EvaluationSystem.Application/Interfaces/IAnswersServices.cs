using System.Collections.Generic;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Questions;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAnswersServices
    {
        void DeleteAnAnswer(int answerId);

        void UpdateAnswer(int questionId, int answerId, UpdateAnswerDto model);

        ICollection<AnswerListDto> CreateAnswer(int questionId, AddListAnswers model);
    }
}
