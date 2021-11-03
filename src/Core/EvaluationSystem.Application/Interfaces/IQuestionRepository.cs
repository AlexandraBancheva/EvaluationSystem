using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IQuestionRepository
    {
        void CreateNewQuestion(Question model);

        IEnumerable<Question> AllQuestions();

        Question UpdateCurrentQuestion(Question model);

        Question GetQuestionById(int questionId);

        void DeleteQuestion(int questionId);
    }
}
