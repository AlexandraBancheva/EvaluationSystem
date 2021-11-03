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

        [HttpGet]
        public IActionResult GetAllQuestions()
        {
            var result = this.questionsServices.GetAll();

            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult GetQuestionById(int id)
        {
            return Ok(questionsServices.GetQuestionById(id));
        }

        [HttpPost]
        public IActionResult CreateNewQuestion([FromBody] CreateQuestionDto model)
        {
            var result = questionsServices.CreateNewQuestion(model);
            return Ok(result);
        }

        [Route("api/questions/{id}")]
        [HttpPut]
        public IActionResult UpdateCurrentQuestion(int id, [FromBody] UpdateQuestionDto model)
        {
            return Ok(questionsServices.UpdateCurrentQuestion(id, model));
        }

        [Route("api/questions/{id}")]
        [HttpDelete]
        public IActionResult DeleteQuestion(int id)
        {
            questionsServices.DeleteQuestion(id);
            return NoContent();
        }
    }
}
