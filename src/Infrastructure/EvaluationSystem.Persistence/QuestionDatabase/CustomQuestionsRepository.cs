using Dapper;
using EvaluationSystem.Application.Models.Questions.QuestionsDtos;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using System.Collections.Generic;

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

            _connection.Execute(query, new { QuestionId = questionId });
        }

        public void DeleteCustomQuestion(int questionId)
        {
            var query = @"DELETE FROM AnswerTemplate
                            WHERE IdQuestion = @QuestionId
                            DELETE FROM ModuleQuestion
                            WHERE IdQuestion = @QuestionId
                            DELETE FROM QuestionTemplate
                            WHERE Id = @QuestionId";
            _connection.Execute(query, new { QuestionId = questionId }, _transaction);
        }

        public QuestionTemplateDto GetCustomById(int questionId)
        {
            var query = @"SELECT qt.Id, qt.[Name], qt.DateOfCreation, qt.[Type], qt.IsReusable, mq.Position AS QuestionPosition
                            FROM QuestionTemplate AS qt
                            JOIN ModuleQuestion AS mq ON mq.IdQuestion = qt.Id
                            WHERE qt.Id = @IdQuestion";

            var res = _connection.QueryFirstOrDefault<QuestionTemplateDto>(query, new { IdQuestion = questionId }, _transaction);

            return res;
        }
    }
}
