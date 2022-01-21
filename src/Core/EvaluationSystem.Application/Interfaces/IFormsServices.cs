using System.Collections.Generic;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IFormsServices
    {
        void DeleteFormById(int formId);

        UpdatedFormDto UpdateCurrentForm(int formId, UpdateFormDto form);

        ICollection<FormDetailDto> CreateNewForm(CreateFormDto form);

        ICollection<FormDetailDto> GetFormById(int formId);
   
        ICollection<FormDetailDto> GetAllForsWithAllModules();
    }
}