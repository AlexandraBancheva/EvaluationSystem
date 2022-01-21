using System.Collections.Generic;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IQuestionTemplatesServices
    {
        void DeleteQuestion(int questionId);

        QuestionDetailDto CreateNewQuestion(CreateQuestionDto model);

        QuestionDetailDto CreateQuestionTemplateFromForm(int moduleId, int position, CreateQuestionDto model);

        QuestionDetailDto GetQuestionById(int questionId);

        QuestionDetailDto UpdateCurrentQuestion(int questionId, UpdateQuestionDto model);

        IEnumerable<ListQuestionsAnswersDto> GetAllQuestionsWithTheirAnswers();

        IEnumerable<ListQuestionsAnswersDto> GetAllQuestionTemplatesWithTheirAnswers();

        IEnumerable<ListQuestionsAnswersDto> GetAllAnswersByQuestionId(int questionId);
    }
}
