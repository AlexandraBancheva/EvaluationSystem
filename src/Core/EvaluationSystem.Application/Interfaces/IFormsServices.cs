using System.Collections.Generic;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IFormsServices
    {
        ICollection<FormDetailDto> CreateNewForm(CreateFormDto form);

        void DeleteFormById(int formId);

        ICollection<FormDetailDto> GetFormById(int formId);

        UpdatedFormDto UpdateCurrentForm(int formId, UpdateFormDto form);
   
        IEnumerable<FormDetailDto> GetAllForsWithAllModules();
    }
}