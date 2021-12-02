using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionTemplatesController : ControllerBase
    {
        private readonly IQuestionTemplatesServices questionsServices;

        public QuestionTemplatesController(IQuestionTemplatesServices questionsServices)
        {
            this.questionsServices = questionsServices;
        }

        [HttpGet]
        public IActionResult GetQuestionByIdWithAnswers()
        {
            var res = questionsServices.GetAllQuestionsWithTheirAnswers();
            return Ok(res);
        }

        [HttpGet("{questionId}")]
        public IActionResult GetQuestionById(int questionId)
        {
            // Example for cache!!!
            //if (!_memoryCache.TryGetValue("Question", out var question))
            //{
            //    question = questionsServices.GetQuestionById(id);
            //    var cacheOptions = new MemoryCacheEntryOptions()
            //    {
            //        AbsoluteExpiration = DateTime.Now.AddSeconds(30)
            //    };
            //    _memoryCache.Set("Question", question, cacheOptions);
            //}

            // return Ok(questionsServices.GetQuestionById(questionId));
            return Ok(questionsServices.GetAllAnswersByQuestionId(questionId));
        }

        [HttpPost]
        public IActionResult CreateNewQuestion([FromBody] CreateQuestionDto model)
        {
            var result = questionsServices.CreateNewQuestion(model);
            return Ok(result);
        }

        [HttpPut("{questionId}")]
        public IActionResult UpdateCurrentQuestion(int questionId, [FromBody] UpdateQuestionDto model)
        {
            return Ok(questionsServices.UpdateCurrentQuestion(questionId, model));
        }

        [HttpDelete("{questionId}")]
        public IActionResult DeleteQuestion(int questionId)
        {
            questionsServices.DeleteQuestion(questionId);
            return NoContent();
        }
    }
}