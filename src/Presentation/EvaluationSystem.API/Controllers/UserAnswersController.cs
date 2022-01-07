using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.UserAnswers;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/UserAnswers")]
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
    }
}
