using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.Forms;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class AnswerRepository : BaseRepository<AnswerTemplate>, IAnswerRepository
    {
        public AnswerRepository(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {
        }

        public FormModuleQuestionDto CheckFormIdModuleIdQuestionId(int formId, int moduleId, int questionId)
        {
            var query = @"SELECT fm.IdForm AS IdForm, fm.IdModule AS IdModule, mq.IdQuestion AS IdQuestion 
                            FROM FormModule AS fm
                            JOIN  ModuleTemplate AS mt ON fm.IdModule = mt.Id
                            JOIN ModuleQuestion AS mq ON mq.IdModule = mt.Id
                            WHERE fm.IdForm = @IdForm AND fm.IdModule = @IdModule AND mq.IdQuestion = @IdQuestion";

            var res = Connection.QueryFirstOrDefault<FormModuleQuestionDto>(query, new { IdForm = formId, IdModule = moduleId, IdQuestion = questionId }, Transaction);

            return res;
        }

        public ICollection<AnswerTemplate> GetAllByQuestionId(int questionId)
        {
            var query = @"SELECT * FROM AnswerTemplate
                        WHERE IdQuestion = @QuestionId";

            var answers = Connection.Query<AnswerTemplate>(query, new { QuestionId = questionId}, Transaction);

            return (ICollection<AnswerTemplate>)answers;
        }
    }
}