using System.Collections.Generic;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using EvaluationSystem.Application.Models.Questions;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAttestationAnswersServices
    {
        ICollection<AnswerListDto> CreateAnswer(int questionId, AddListAnswers model);

        void DeleteAttestationAnswer(int answerId);
    }
}