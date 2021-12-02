using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;

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
    }
}
