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
    }
}