using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Interfaces;

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
        [Route("[action]", Name = "ListOfUsersForEvaluation")]
        public IActionResult ListOfUsersForEvaluation()
        {
            var res = _usersServices.GetUsersToEvaluate();
            return Ok(res);
        }
    }
}
