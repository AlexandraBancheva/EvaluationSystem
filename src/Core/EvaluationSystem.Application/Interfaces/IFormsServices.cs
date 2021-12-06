using System.Collections.Generic;
using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IFormsServices
    {
        IEnumerable<FormDetailDto> CreateNewForm(CreateFormDto form);

        void DeleteFormById(int formId);

        IEnumerable<FormDetailDto> GetFormById(int formId);

        UpdatedFormDto UpdateCurrentForm(int formId, UpdateFormDto form);
   
        IEnumerable<FormDetailDto> GetAllForsWithAllModules();
    }
}