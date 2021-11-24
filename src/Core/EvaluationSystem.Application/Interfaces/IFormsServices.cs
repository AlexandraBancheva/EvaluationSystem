using System.Collections.Generic;
using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IFormsServices
    {
        FormDetailDto CreateNewForm(CreateFormDto form);

        void DeleteFormById(int formId);

        FormDetailDto GetFormById(int formId);

        FormDetailDto UpdateCurrentForm(int formId, UpdateFormDto form);
   
        IEnumerable<ListFormsModulesDto> GetAllForsWithAllModules();
    }
}
