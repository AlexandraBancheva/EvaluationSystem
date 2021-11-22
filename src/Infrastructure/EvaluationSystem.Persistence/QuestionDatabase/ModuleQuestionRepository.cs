using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

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

        public ICollection<ModuleTemplate> GetModuleWithAllQuestions()
        {
            using var dbConnection = Connection;
            var query = @"SELECT * FROM ModuleTemplate AS mt
                            JOIN ModuleQuestion AS mq ON mt.Id = mq.IdModule
                            JOIN QuestionTemplate AS qt ON mq.IdQuestion = qt.Id";

            var moduleDictionary = new Dictionary<int, ModuleTemplate>();
            var modules = dbConnection.Query<ModuleTemplate, QuestionTemplate, ModuleTemplate>(query, (module, question) =>
            {
                if (!moduleDictionary.TryGetValue(module.Id, out var currentModule))
                {
                    currentModule = module;
                    moduleDictionary.Add(currentModule.Id, currentModule);
                }

                currentModule.Questions.Add(question);
                return currentModule;
            }, splitOn: "Id")
                .Distinct()
                .ToList();

            return modules;
        }
    }
}
