using System.Collections.Generic;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Questions;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAttestationAnswersServices
    {
        void DeleteAttestationAnswer(int answerId);

        ICollection<AnswerListDto> CreateAnswer(int questionId, AddListAnswers model);
    }
}