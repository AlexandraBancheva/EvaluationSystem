﻿using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Answers.AnswersDtos;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/forms/{formId}/modules/{moduleId}/questions/{questionId}/answers")]
    public class AnswersController : BaseController
    {
        private readonly IAnswersServices answersServices;

        public AnswersController(IAnswersServices answersServices)
        {
            this.answersServices = answersServices;
        }

        [HttpPost]
        public IActionResult AddNewAnswer(int formId, int moduleId, int questionId, [FromBody] AddListAnswers model)
        {
            var res = answersServices.CreateAnswerTemplates(formId, moduleId, questionId, model);
            return Ok(res);
        }

        [HttpDelete("{answerId}")]
        public IActionResult DeleteAnAnswerById(int answerId)
        {
            answersServices.DeleteAnAnswer(answerId);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateAnswer(int formId, int moduleId, int questionId, int answerId, [FromBody] UpdateAnswerDto model)
        {
            answersServices.UpdateAnswerTemplate(formId, moduleId, questionId, answerId, model);
            return Ok();
        }
    }
}
