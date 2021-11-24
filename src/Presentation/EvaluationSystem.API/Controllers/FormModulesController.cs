using EvaluationSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/formModules")]
    public class FormModulesController : ControllerBase
    {
        private readonly IFormModulesServices _formModulesServices;

        public FormModulesController(IFormModulesServices formModulesServices)
        {
            _formModulesServices = formModulesServices;
        }

        [HttpPost()]
        public IActionResult AddModulesInForm(int formId, int moduleId, int position)
        {
            _formModulesServices.AddModulesInForm(formId, moduleId, position);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteModuleFromForm(int formId, int moduleId)
        {
            _formModulesServices.DeleteModuleFromForm(formId, moduleId);
            return Ok();
        }

      
    }
}
