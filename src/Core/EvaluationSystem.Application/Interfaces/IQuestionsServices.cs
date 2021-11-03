using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IQuestionsServices
    {

        IEnumerable<ListQuestionsDto> GetAll();

        QuestionDetailDto GetQuestionById(int questionId);

        QuestionDetailDto CreateNewQuestion(CreateQuestionDto model);

        QuestionDetailDto UpdateCurrentQuestion(int questionId, UpdateQuestionDto model);

        void DeleteQuestion(int questionId);
    }
}
