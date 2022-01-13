using EvaluationSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private readonly IUsersServices _usersServices;
        public UsersController(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var res = await _usersServices.GetAll();
            return Ok(res);
        }

        [HttpGet]
        [Route("[action]", Name = "GetUsersToEvaluate")]
        public IActionResult GetUsersToEvaluate()
        {
            var res = _usersServices.GetUsersToEvaluate();
            return Ok(res);
        }
    }
}
