using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route(("api/questions/{questionId}/answers"))]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswersServices answersServices;

        public AnswersController(IAnswersServices answersServices)
        {
            this.answersServices = answersServices;
        }

        [HttpPost]
        public IActionResult AddNewAnswer(int questionId, [FromBody] AddNewAnswerDto model)
        {
            var result = answersServices.AddNewAnswer(questionId, model);
            return Ok(result);
        }

        [HttpGet("questionId")]
        public IActionResult GetAllAnswersByQuestionId(int questionId)
        {
            var result = answersServices.GetAnswersByQuestionId(questionId);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult DeleteAnAnswerById(int questionId, int id)
        {
            answersServices.DeleteAnAnswer(questionId, id);
            return NoContent();
        }
    }
}
