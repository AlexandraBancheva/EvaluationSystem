using EvaluationSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _usersServices;
        public UsersController(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }

        [HttpGet()]
        public IActionResult GetAllUsers()
        {
            var res = _usersServices.GetAll();
            return Ok(res);
        }

        [HttpGet()]
        [Route("[action]", Name = "GetUsersToEvaluate")]
        public IActionResult GetUsersToEvaluate()
        {
            var res = _usersServices.GetUsersToEvaluate();
            return Ok(res);
        }
    }
}
