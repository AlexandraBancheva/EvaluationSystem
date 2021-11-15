﻿using EvaluationSystem.Application.Interfaces;
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
            answersServices.AddNewAnswer(questionId, model);
            return Ok();
        }

        [HttpDelete("{answerId}")]
        public IActionResult DeleteAnAnswerById(int answerId)
        {
            answersServices.DeleteAnAnswer(answerId);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateAnswer(int questionId, int answerId, [FromBody] UpdateAnswerDto model)
        {
            var res = answersServices.UpdateAnswer(questionId, answerId, model);
            return Ok(res);
        }
    }
}