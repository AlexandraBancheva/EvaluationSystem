using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Application.Models.FormModules;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationFormModuleRepository : BaseRepository<AttestationFormModule>, IAttestationFormModuleRepository
    {
        public AttestationFormModuleRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void AddModuleInForm(int formId, int moduleId, int position)
        {
            var query = @"INSERT INTO AttestationFormModule
                        VALUES (@IdAttestationForm, @IdAttestationModule, @Position)";

            Connection.Execute(query, new { IdAttestationForm = formId, IdAttestationModule = moduleId, Position = position }, Transaction);
        }

        public void DeleteModuleFromForm(int formId, int moduleId)
        {
            var query = @"DELETE FROM AttestationFormModule
                        WHERE IdAttestationForm = @IdAttestationForm AND IdAttestationModule = @IdAttestationModule";

            Connection.Execute(query, new { IdAttestationForm = formId, IdAttestationModule = moduleId }, Transaction);
        }

        public ICollection<FormModuleGettingOnlyModulesDto> GetAllModulesByFormId(int formId)
        {
            var query = @"SELECT IdAttestationModule FROM AttestationFormModule
                        WHERE IdAttestationForm = @IdAttestationForm";

            var formWithModules = Connection.Query<FormModuleGettingOnlyModulesDto>(query, new { IdAttestationForm = formId });

            return (ICollection<FormModuleGettingOnlyModulesDto>)formWithModules;
        }
    }
}
