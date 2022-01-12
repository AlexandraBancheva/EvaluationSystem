using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class AnswerRepository : BaseRepository<AnswerTemplate>, IAnswerRepository
    {
        public AnswerRepository(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {
        }

        public CheckFormModuleQuestionDto CheckFormIdModuleIdQuestionId(int formId, int moduleId, int questionId)
        {
            var query = @"SELECT fm.IdForm AS IdForm, fm.IdModule AS IdModule, mq.IdQuestion AS IdQuestion 
                            FROM FormModule AS fm
                            JOIN  ModuleTemplate AS mt ON fm.IdModule = mt.Id
                            JOIN ModuleQuestion AS mq ON mq.IdModule = mt.Id
                            WHERE fm.IdForm = @IdForm AND fm.IdModule = @IdModule AND mq.IdQuestion = @IdQuestion";

            var res = Connection.QueryFirstOrDefault<CheckFormModuleQuestionDto>(query, new { IdForm = formId, IdModule = moduleId, IdQuestion = questionId }, Transaction);

            return res;
        }

        public CheckFormModuleQuestionAnswerDto CheckFormIdModuleIdQuestionIdAnswerId(int formId, int moduleId, int questionId, int answerId)
        {
            var query = @"SELECT fm.IdForm AS IdForm, fm.IdModule AS IdModule, mq.IdQuestion AS IdQuestion, [at].Id AS IdAnswer 
                        FROM FormModule AS fm
                        JOIN  ModuleTemplate AS mt ON fm.IdModule = mt.Id
                        JOIN ModuleQuestion AS mq ON mq.IdModule = mt.Id
                        JOIN AnswerTemplate AS [at] ON [at].IdQuestion = mq.IdQuestion
                        WHERE fm.IdForm = @IdForm AND fm.IdModule = @IdModule AND mq.IdQuestion = @IdQuestion AND [at].Id = @IdAnswer";

            var res = Connection.QueryFirstOrDefault<CheckFormModuleQuestionAnswerDto>(query, new { IdForm = formId, IdModule = moduleId, IdQuestion = questionId, IdAnswer = answerId});

            return res;
        }

        public CheckQuestionIdAnswerIdDto CheckQuestionIdAnswerId(int questionId, int answerId)
        {
            var query = @"SELECT qt.Id AS IdQuestion, [at].Id AS IdAnswer 
                        FROM QuestionTemplate AS qt
                        JOIN AnswerTemplate AS [at] ON [at].IdQuestion = qt.Id
                        WHERE qt.IsReusable = 'true' AND [at].IdQuestion = @IdQuestion AND [at].Id = @IdAnswer";

            var res = Connection.QueryFirstOrDefault<CheckQuestionIdAnswerIdDto>(query, new { IdQuestion = questionId, IdAnswer = answerId }, Transaction);

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