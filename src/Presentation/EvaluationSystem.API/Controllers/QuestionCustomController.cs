using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/modules/{moduleId}/customQuestions")]
    public class QuestionCustomController : ControllerBase
    {
        private readonly ICustomQuestionsServices _questionsCustomServices;

        public QuestionCustomController(ICustomQuestionsServices questionsCustomServices)
        {
            _questionsCustomServices = questionsCustomServices;
        }

        [HttpPost]
        public IActionResult CreateNewQuestion(int moduleId, int position, [FromBody] CreateQuestionDto model)
        {
            var result = _questionsCustomServices.CreateNewQuestion(moduleId, position, model);
            return Ok(result);
        }

        // Update query

        // Delete query
    }
}
