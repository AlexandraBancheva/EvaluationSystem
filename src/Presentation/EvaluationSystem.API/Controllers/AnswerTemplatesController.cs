using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route(("api/questionTemplates/{questionId}/answers"))]
    public class AnswerTemplatesController : BaseAdminController
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
        public IActionResult DeleteAnswer(int questionId ,int answerId)
        {
            _answersServices.DeleteAnswerTemplate(questionId, answerId);
            return NoContent();
        }

        [HttpPut("{answerId}")]
        public IActionResult UpdateAnswerTemplate(int questionId, int answerId, [FromBody] UpdateAnswerDto model)
        {
           var res = _answersServices.UpdateAnswer(questionId, answerId, model);
            return Ok(res);
        }
    }
}
