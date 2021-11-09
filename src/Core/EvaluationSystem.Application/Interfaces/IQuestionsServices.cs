using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IQuestionsServices
    {
        QuestionDetailDto CreateNewQuestion(CreateQuestionDto model);

        QuestionDetailDto GetQuestionById(int questionId);


        QuestionDetailDto UpdateCurrentQuestion(int questionId, UpdateQuestionDto model);

        void DeleteQuestion(int questionId);

        IEnumerable<ListQuestionsAnswersDto> GetAllQuestionsWithTheirAnswers();
    }
}
