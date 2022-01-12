using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IAnswerRepository : IRepository<AnswerTemplate>
    {
        ICollection<AnswerTemplate> GetAllByQuestionId(int questionId);

        CheckQuestionIdAnswerIdDto CheckQuestionIdAnswerId(int questionId, int answerId);

        CheckFormModuleQuestionDto CheckFormIdModuleIdQuestionId(int formId, int moduleId, int questionId);

        CheckFormModuleQuestionAnswerDto CheckFormIdModuleIdQuestionIdAnswerId(int formId, int moduleId, int questionId, int answerId);
    }
}
