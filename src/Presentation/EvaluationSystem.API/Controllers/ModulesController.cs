using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/forms/{formId}/modules")]
    public class ModulesController : ControllerBase
    {
        private readonly IModulesServices _modulesServices;
        private readonly IModuleQuestionsServices _moduleQuestionsServices;

        public ModulesController(IModulesServices moduleServices, IModuleQuestionsServices moduleQuestionsServices)
        {
            _modulesServices = moduleServices;
            _moduleQuestionsServices = moduleQuestionsServices;
        }

        [HttpPost]
        public IActionResult CreateNewModule(int formId, [FromBody] CreateModuleDto model)
        {
            var res = _modulesServices.CreateModule(formId, model);
            return Ok(res);
        }

        [HttpPut("{moduleId}")]
        public IActionResult UpdateModule(int formId, int moduleId, [FromBody] UpdateModuleDto model)
        {
            var res = _modulesServices.UpdateCurrentModule(formId, moduleId, model);
            return Ok(res);
        }

        [HttpGet("{moduleId}")]
        public IActionResult GetModuleById(int formId, int moduleId)
        {
            var res = _modulesServices.GetCurrentModuleById(formId, moduleId);
            return Ok(res);
        }

        [HttpDelete("{moduleId}")]
        public IActionResult DeleteModule(int moduleId)
        {
            _modulesServices.DeleteCurrentModule(moduleId);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAllModulesByFormId(int formId)
        {
            var res = _modulesServices.GetAllModulesByFormId(formId);
            return Ok(res);
        }
    }
}