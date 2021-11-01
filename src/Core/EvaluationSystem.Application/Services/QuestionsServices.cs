using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using System.Collections.Generic;

namespace EvaluationSystem.Application.Services
{
    public class QuestionsServices : IQuestionsServices
    {
        public QuestionDetailDto CreateNewQuestion(CreateQuestionDto model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ListQuestionsDto> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
