using EvaluationSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/attestationForms")]
    public class AttestationFormsController : ControllerBase
    {
        private readonly IAttestationFormsServices _attestationFormsServices;

        public AttestationFormsController(IAttestationFormsServices attestationFormsServices)
        {
            _attestationFormsServices = attestationFormsServices;
        }

        [HttpGet("{formId}")]
        public IActionResult GetById(int formId)
        {
            var results = _attestationFormsServices.GetFormById(formId);
            return Ok(results);
        }
    }
}
