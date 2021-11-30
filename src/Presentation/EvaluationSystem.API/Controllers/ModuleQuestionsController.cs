//using EvaluationSystem.Application.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace EvaluationSystem.API.Controllers
//{
//    [ApiController]
//    [Route("api/modules/{moduleId}/questions")]
//    public class ModuleQuestionsController : ControllerBase
//    {
//        private readonly IModuleQuestionsServices _moduleQuestionsServices;

//        public ModuleQuestionsController(IModuleQuestionsServices moduleQuestionsServices)
//        {
//            _moduleQuestionsServices = moduleQuestionsServices;
//        }

//        [HttpPost]
//        public IActionResult AddNewQuestionToModule(int moduleId, int questionId, int position)
//        {
//            _moduleQuestionsServices.AddQuestionToModule(moduleId, questionId, position);
//            return Ok();
//        }

//        [HttpDelete]
//        public IActionResult DeleteQuestionFromModule(int moduleId, int questionId)
//        {
//            _moduleQuestionsServices.DeleteQuestionFromModule(moduleId, questionId);
//            return NoContent();
//        }
//    }
//}
