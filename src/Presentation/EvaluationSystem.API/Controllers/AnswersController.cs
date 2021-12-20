using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route(("api/forms/{formId}/modules/{moduleId}/questions/{questionId}/answers"))]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswersServices answersServices;

        public AnswersController(IAnswersServices answersServices)
        {
            this.answersServices = answersServices;
        }

        [HttpPost]
        public IActionResult AddNewAnswer(int questionId, [FromBody] AddListAnswers model)
        {
            // answersServices.AddNewAnswer(questionId, model);
            var res = answersServices.CreateAnswer(questionId, model);
            return Ok(res);
        }

        [HttpDelete()]
        public IActionResult DeleteAnAnswerById(int answerId)
        {
            answersServices.DeleteAnAnswer(answerId);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateAnswer(int questionId, int answerId, [FromBody] UpdateAnswerDto model)
        {
            answersServices.UpdateAnswer(questionId, answerId, model);
            return Ok();
        }
    }
}
