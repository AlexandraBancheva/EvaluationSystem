using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class ModuleRepository : BaseRepository<ModuleTemplate>, IModuleRepository
    {
        public ModuleRepository(IUnitOfWork unitOfWork)
           : base(unitOfWork)
        {
        }

        public void DeleteModule(int moduleId)
        {
            var query = @"DELETE ModuleQuestion
                            WHERE IdModule = @ModuleId
                            DELETE ModuleTemplate
                            WHERE Id = @ModuleId";
            _connection.Execute(query, new { ModuleId = moduleId }, _transaction);
        }
    }
}