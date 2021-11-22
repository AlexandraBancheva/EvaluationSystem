using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Modules;
using EvaluationSystem.Application.Models.Modules.ModulesDtos;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/modules")]
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
        public IActionResult CreateNewModule([FromBody] CreateModuleDto model)
        {
            var res = _modulesServices.CreateModule(model);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateModule(int id, [FromBody] UpdateModuleDto model)
        {
            var res = _modulesServices.UpdateCurrentModule(id, model);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetModuleById(int id)
        {
            var res = _modulesServices.GetModuleById(id);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteModule(int id)
        {
            _modulesServices.DeleteCurrentModule(id);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAllModulesWithAllQuestions()
        {
            var results = _moduleQuestionsServices.GetAllModulesWithAllQuestions();
            return Ok(results);
        }
    }
}
