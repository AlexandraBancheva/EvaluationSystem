using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Forms;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/forms")]
    public class FormsController : ControllerBase
    {
        private readonly IFormsServices _formsServices;

        public FormsController(IFormsServices formsServices)
        {
            _formsServices = formsServices;
        }

        [HttpPost]
        public IActionResult CreateNewForm([FromBody] CreateFormDto form)
        {
            var result = _formsServices.CreateNewForm(form);
            return Ok(result);
        }

        [HttpDelete("{formId}")]
        public IActionResult DeleteCurrentForm(int formId)
        {
            _formsServices.DeleteFormById(formId);
            return NoContent();
        }

        [HttpGet("{formId}")]
        public IActionResult GetFormById(int formId)
        {
            var result = _formsServices.GetFormById(formId);
            return Ok(result);
        }

        [HttpPut("{formId}")]
        public IActionResult UpdateCurrentForm(int formId, [FromBody] UpdateFormDto form)
        {
            var result = _formsServices.UpdateCurrentForm(formId, form);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllFormsWithAllModules()
        {
            var results = _formsServices.GetAllForsWithAllModules();
            return Ok(results);
        }
    }
}