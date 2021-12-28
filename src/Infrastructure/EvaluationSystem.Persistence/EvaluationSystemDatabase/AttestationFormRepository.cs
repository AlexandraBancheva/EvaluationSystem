using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationFormRepository : BaseRepository<AttestationForm>, IAttestationFormRepository
    {
        public AttestationFormRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void DeleteAttestationForm(int formId)
        {
            var query = @"DELETE FROM AttestationFormModule
                WHERE IdAttestationForm = @FormId
                DELETE FROM AttestationForm
                WHERE Id = @FormId";

            _connection.Execute(query, new {FormId = formId }, _transaction);
        }
    }
}
