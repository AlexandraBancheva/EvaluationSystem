using Dapper;
using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;
using System.Collections.Generic;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationFormModuleRepository : QuestionDatabase.BaseRepository<AttestationFormModule>, IAttestationFormModuleRepository
    {
        public AttestationFormModuleRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void AddModuleInForm(int formId, int moduleId, int position)
        {
            var query = @"INSERT INTO AttestationFormModule
                        VALUES (@IdAttestationForm, @IdAttestationModule, @Position)";

            _connection.Execute(query, new { IdAttestationForm = formId, IdAttestationModule = moduleId, Position = position }, _transaction);
        }

        public void DeleteModuleFromForm(int formId, int moduleId)
        {
            var query = @"DELETE FROM AttestationFormModule
                        WHERE IdAttestationForm = @IdAttestationForm AND IdAttestationModule = @IdAttestationModule";

            _connection.Execute(query, new { IdAttestationForm = formId, IdAttestationModule = moduleId }, _transaction);
        }

        public ICollection<FormModuleGettingOnlyModulesDto> GetAllModulesByFormId(int formId)
        {
            var query = @"SELECT IdAttestationModule FROM AttestationFormModule
                        WHERE IdAttestationForm = @IdAttestationForm";

            var formWithModules = _connection.Query<FormModuleGettingOnlyModulesDto>(query, new { IdAttestationForm = formId });

            return (ICollection<FormModuleGettingOnlyModulesDto>)formWithModules;
        }
    }
}
