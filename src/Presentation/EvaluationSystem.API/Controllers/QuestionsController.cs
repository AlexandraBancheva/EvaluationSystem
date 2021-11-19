using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Questions.QuestionsDtos;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

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
        public IActionResult GetQuestionByIdWithAnswers()
        {
            var res = questionsServices.GetAllQuestionsWithTheirAnswers();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetQuestionById(int id)
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

            return Ok(questionsServices.GetQuestionById(id));
        }

        [HttpPost]
        public IActionResult CreateNewQuestion([FromBody] CreateQuestionDto model)
        {
            var result = questionsServices.CreateNewQuestion(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCurrentQuestion(int id, [FromBody] UpdateQuestionDto model)
        {
            return Ok(questionsServices.UpdateCurrentQuestion(id, model));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            questionsServices.DeleteQuestion(id);
            return NoContent();
        }
    }
}
