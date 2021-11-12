using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IQuestionRepository
    {
        void CreateNewQuestion(QuestionTemplate model);

        QuestionTemplate GetQuestionById(int questionId);

        void DeleteQuestion(int questionId);

        void UpdateCurrentQuestion(int id, QuestionTemplate model);

        List<QuestionTemplate> GetAllQuestionsWithAnswers();

        void Create(QuestionTemplate currentEntity);
    }
}
