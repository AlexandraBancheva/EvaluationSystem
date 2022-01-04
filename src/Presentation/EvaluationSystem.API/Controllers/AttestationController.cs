using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Attestations;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/attestations")]
    public class AttestationsController : ControllerBase
    {
        private readonly IAttestationsServices _attestationsServices;
        public AttestationsController(IAttestationsServices attestationsServices)
        {
            _attestationsServices = attestationsServices;
        }

        [HttpPost]
        public IActionResult CreateAttestation([FromBody] CreateAttestationDto model)
        {
            _attestationsServices.CreateAttestation(model);
            return Ok();
        }

        [HttpDelete("{attestationId}")]
        public IActionResult DeleteAttestation(int attestationId)
        {
            _attestationsServices.DeleteAttestation(attestationId);

            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAllAttestations()
        {
            var results = _attestationsServices.GetAllAttestations();
            return Ok(results);
        }
    }
}
