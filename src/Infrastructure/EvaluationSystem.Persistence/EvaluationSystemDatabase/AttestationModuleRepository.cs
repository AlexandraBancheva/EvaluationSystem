using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationModuleRepository : BaseRepository<AttestationModule>, IAttestationModuleRepository
    {
        public AttestationModuleRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void DeleteAttestatationModule(int moduleId)
        {
            var query = @"DELETE FROM AttestationFormModule
                        WHERE IdAttestationModule = @ModuleId
                        DELETE FROM AttestationModuleQuestion
                        WHERE IdAttestationModule = @ModuleId
                        DELETE FROM AttestationModule
                        WHERE Id = @ModuleId";

            _connection.Execute(query, new { ModuleId  = moduleId}, _transaction);
        }
    }
}