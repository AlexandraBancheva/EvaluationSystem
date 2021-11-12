using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IQuestionRepository
    {
        void CreateNewQuestion(Question model);

        Question GetQuestionById(int questionId);

        void DeleteQuestion(int questionId);

        void UpdateCurrentQuestion(int id, Question model);

        List<Question> GetAllQuestionsWithAnswers();

        void Create(Question currentEntity);
    }
}
