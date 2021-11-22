using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class ModuleRepository : BaseRepository<ModuleTemplate>, IModuleRepository
    {
        public ModuleRepository(IConfiguration configuration)
            :base(configuration)
        {
        }

        public void DeleteModule(int moduleId)
        {
            using var dbConnection = Connection;
            var query = @"DELETE ModuleQuestion
                            WHERE IdModule = @ModuleId
                            DELETE ModuleTemplate
                            WHERE Id = @ModuleId";
            dbConnection.Execute(query, new { ModuleId = moduleId });
        }
    }
}