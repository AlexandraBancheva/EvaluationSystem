using System.Collections.Generic;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IQuestionTemplatesServices
    {
        QuestionDetailDto CreateNewQuestion(CreateQuestionDto model);

        // 19.01.2022
        QuestionDetailDto CreateQuestionTemplateFromForm(int moduleId, int position, CreateQuestionDto model);

        QuestionDetailDto GetQuestionById(int questionId);

        QuestionDetailDto UpdateCurrentQuestion(int questionId, UpdateQuestionDto model);

        void DeleteQuestion(int questionId);

        IEnumerable<ListQuestionsAnswersDto> GetAllQuestionsWithTheirAnswers();

        IEnumerable<ListQuestionsAnswersDto> GetAllQuestionTemplatesWithTheirAnswers();

        IEnumerable<ListQuestionsAnswersDto> GetAllAnswersByQuestionId(int questionId);
    }
}
