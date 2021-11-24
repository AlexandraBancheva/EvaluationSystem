namespace EvaluationSystem.Application.Interfaces
{
    public interface IFormModulesServices
    {
        void AddModulesInForm(int formId, int moduleId, int position);

        void DeleteModuleFromForm(int formId, int moduleId);
    }
}
