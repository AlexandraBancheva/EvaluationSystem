using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class CustomQuestionsRepository : BaseRepository<QuestionTemplate>, ICustomQuestionsRepository
    {
        public CustomQuestionsRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void RemovedQuestion(int questionId)
        {
            var query = @"DELETE FROM ModuleQuestion
                        WHERE IdQuestion = @QuestionId";

            var queryParameter = new { QuestionId = questionId };
            Connection.Execute(query, queryParameter, transaction: Transaction);
        }

        public void DeleteCustomQuestion(int questionId)
        {
            var query = @"DELETE FROM AnswerTemplate
                            WHERE IdQuestion = @QuestionId
                            DELETE FROM ModuleQuestion
                            WHERE IdQuestion = @QuestionId
                            DELETE FROM QuestionTemplate
                            WHERE Id = @QuestionId";

            var queryParameter = new { QuestionId = questionId };
            Connection.Execute(query, queryParameter, transaction: Transaction);
        }

        public QuestionTemplateDto GetCustomById(int questionId)
        {
            var query = @"SELECT qt.Id, qt.[Name], qt.DateOfCreation, qt.[Type], qt.IsReusable, mq.Position AS QuestionPosition
                            FROM QuestionTemplate AS qt
                            JOIN ModuleQuestion AS mq ON mq.IdQuestion = qt.Id
                            WHERE qt.Id = @IdQuestion";


            var queryParameter = new { IdQuestion = questionId };
            var res = Connection.QueryFirstOrDefault<QuestionTemplateDto>(query, queryParameter, transaction: Transaction);

            return res;
        }
    }
}
