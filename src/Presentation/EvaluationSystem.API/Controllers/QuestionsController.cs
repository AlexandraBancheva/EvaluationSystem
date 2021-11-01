using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsServices questionsServices;

        public QuestionsController(IQuestionsServices questionsServices)
        {
            this.questionsServices = questionsServices;
        }

        [HttpPost]
        public QuestionDetailDto CreateNewQuestion([FromBody] CreateQuestionDto model)
        {
            return null;
        }

        [HttpGet]
        public IEnumerable<ListQuestionsDto> GetAllQuestions()
        {
            return this.questionsServices.GetAll();
        }
    }
}
