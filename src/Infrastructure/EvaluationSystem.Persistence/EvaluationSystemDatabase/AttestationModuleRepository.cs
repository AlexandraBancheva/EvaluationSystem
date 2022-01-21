﻿using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
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

            var queryParameter = new { ModuleId = moduleId };
            Connection.Execute(query, queryParameter, transaction: Transaction);
        }
    }
}