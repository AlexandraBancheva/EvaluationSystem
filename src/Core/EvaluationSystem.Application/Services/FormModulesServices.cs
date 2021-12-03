using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Application.Services
{
    public class FormModulesServices : IFormModulesServices
    {
        private readonly IFormModuleRepository _formQuestionRepository;

        public FormModulesServices(IFormModuleRepository formQuestionRepository)
        {
            _formQuestionRepository = formQuestionRepository;
        }

        public void AddModulesInForm(int formId, int moduleId, int position)
        {
            _formQuestionRepository.AddNewModuleInForm(formId, moduleId, position);
        }

        public void DeleteModuleFromForm(int formId, int moduleId)
        {
            _formQuestionRepository.DeleteModuleFromForm(formId, moduleId);
        }
    }
}