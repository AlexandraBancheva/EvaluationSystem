using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class ModuleRepository : BaseRepository<ModuleTemplate>, IModuleRepository
    {
        //public ModuleRepository(IConfiguration configuration)
        //    :base(configuration)
        //{
        //}
        public ModuleRepository(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {
        }

        public void DeleteModule(int moduleId)
        {
           // using var dbConnection = Connection;
            var query = @"DELETE ModuleQuestion
                            WHERE IdModule = @ModuleId
                            DELETE ModuleTemplate
                            WHERE Id = @ModuleId";
            _connection.Execute(query, new { ModuleId = moduleId }, _transaction);
        }
    }
}