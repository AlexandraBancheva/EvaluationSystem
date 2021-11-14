using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IQuestionRepository : IRepository<QuestionTemplate>
    {
      //  int CreateNewQuestion(QuestionTemplate model);

      //  QuestionTemplate GetQuestionById(int questionId);

     //   void UpdateCurrentQuestion(int id, QuestionTemplate model);

        List<QuestionTemplate> GetAllQuestionsWithAnswers();

      //  QuestionTemplate GetQuestionById(int id);

      //  void DeleteQuestion(int questionId);

       

      //  void Create(QuestionTemplate currentEntity);
    }
}
