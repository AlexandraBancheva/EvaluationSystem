using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using System.Collections.Generic;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAnswerRepository : IRepository<AnswerTemplate>
    {
        ICollection<AnswerTemplate> GetAllByQuestionId(int questionId);

        FormModuleQuestionDto CheckFormIdModuleIdQuestionId(int formId, int moduleId, int questionId);
    }
}
