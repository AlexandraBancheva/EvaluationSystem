using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class ModuleQuestionRepository : BaseRepository<ModuleQuestion>, IModuleQuestionRepository
    {
        public ModuleQuestionRepository(IConfiguration configuration) 
            : base(configuration)
        {

        }

        public void AddNewQuestionToModule(int moduleId, int questionId, int position)
        {
            using var dbConnection = Connection;
            var query = @"INSERT INTO ModuleQuestion
                            VALUES (@IdModule, @IdQuestion, @Position)";
            dbConnection.Execute(query, new { IdModule = moduleId, IdQuestion = questionId, Position = position});
        }

        public void DeleteQuestionFromModule(int moduleId, int questionId)
        {
            using var dbConnection = Connection;
            var query = @"DELETE ModuleQuestion
                            WHERE IdModule = @ModuleId AND IdQuestion = @QuestionId ";

            dbConnection.Execute(query, new { ModuleId = moduleId, QuestionId = questionId});
        }

        public ICollection<ModuleTemplate> GetModuleWithAllQuestions(int moduleId, int questionId)
        {
            throw new System.NotImplementedException();
        }
    }
}
