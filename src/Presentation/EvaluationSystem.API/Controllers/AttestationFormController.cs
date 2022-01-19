using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.AttestationQuestions;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/attestationForms")]
    public class AttestationFormsController : BaseController
    {
        private readonly IAttestationFormsServices _attestationFormsServices;

        public AttestationFormsController(IAttestationFormsServices attestationFormsServices)
        {
            _attestationFormsServices = attestationFormsServices;
        }

        [HttpGet("{attestationId}")]
        public IActionResult GetById(int attestationId)
        {
            var results = _attestationFormsServices.GetFormById(attestationId);
            return Ok(results);
        }

        [HttpPut("{attestationId}")]
        public IActionResult UpdateUserAnswer(int attestationId, [FromBody] AttestationQuestionUpdateDto model)
        {
            _attestationFormsServices.UpdateUserAnswer(attestationId, model);
            return Ok();
        }
    }
}
