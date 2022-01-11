using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.UserAnswers;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/UserAnswers/")]
    public class UserAnswersController : BaseController
    {
        private readonly IUserAnswersServices _userAnswersServices;

        public UserAnswersController(IUserAnswersServices userAnswersServices)
        {
            _userAnswersServices = userAnswersServices;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserAnswerDto model)
        {
            _userAnswersServices.Create(model);
            return Ok();
        }

        [HttpGet()]
        [Route("{attestationId}/{userEmail}")]
        public IActionResult GetAttestationAnswerByUser(int attestationId, string userEmail)
        {
            var res = _userAnswersServices.GetAttestationAnswerByUser(attestationId, userEmail);
            return Ok(res);
        }
    }
}
