using Microsoft.AspNetCore.Mvc;
using EvaluationSystem.Application.Interfaces;

namespace EvaluationSystem.API.Controllers
{
    [ApiController]
    [Route("api/modules/{moduleId}/questions")]
    public class ModuleQuestionsController : BaseAdminController
    {
        private readonly IModuleQuestionsServices _moduleQuestionsServices;

        public ModuleQuestionsController(IModuleQuestionsServices moduleQuestionsServices)
        {
            _moduleQuestionsServices = moduleQuestionsServices;
        }

        [HttpPost("{questionId}")]
        public IActionResult AddNewQuestionToModule(int moduleId, int questionId, int position)
        {
            _moduleQuestionsServices.AddQuestionToModule(moduleId, questionId, position);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteQuestionFromModule(int moduleId, int questionId)
        {
            _moduleQuestionsServices.DeleteQuestionFromModule(moduleId, questionId);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAllQuestionsByModuleId(int moduleId)
        {
           var res = _moduleQuestionsServices.GetModuleWithAllQuestions(moduleId);
            return Ok(res);
        }
    }
}
