using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route(("api/questionTemplates/{questionId}/answers"))]
    public class AnswerTemplatesController : BaseController
    {
        private readonly IAnswersServices _answersServices;

        public AnswerTemplatesController(IAnswersServices answersServices)
        {
            _answersServices = answersServices;
        }

        [HttpPost()]
        public IActionResult AddNewAnswer(int questionId, [FromBody] AddListAnswers model)
        {
            var result = _answersServices.CreateAnswer(questionId, model);
            return Ok(result);
        }

        [HttpDelete("{answerId}")]
        public IActionResult DeleteAnswer(int answerId)
        {
            _answersServices.DeleteAnAnswer(answerId);
            return NoContent();
        }

        [HttpPut()]
        public IActionResult UpdateAnswerTemplate(int questionId, int answerId, [FromBody] UpdateAnswerDto model)
        {
            _answersServices.UpdateAnswer(questionId, answerId, model);
            return Ok();
        }
    }
}
