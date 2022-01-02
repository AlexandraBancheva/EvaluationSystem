using Dapper;
using EvaluationSystem.Application.Models.ModuleQuestions;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;
using System.Collections.Generic;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationModuleQuestionRepository : BaseRepository<AttestationModuleQuestion>, IAttestationModuleQuestionRepository
    {
        public AttestationModuleQuestionRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void AddNewQuestionToModule(int moduleId, int questionId, int position)
        {
            var query = @"INSERT INTO AttestationModuleQuestion
                        VALUES (@AttestationModuleId, @AttestationQuestionId, @AttestationPosition)";

            _connection.Execute(query, new { AttestationModuleId = moduleId, AttestationQuestionId = questionId, AttestationPosition = position }, _transaction);
        }

        public void DeleteQuestionFromModule(int moduleId, int questionId)
        {
            var query = @"DELETE FROM AttestationModuleQuestion
                        WHERE IdAttestationModule = @IdAttestationModule AND IdAttestationQuestion = @IdAttestationQuestion";

            _connection.Execute(query, new { IdAttestationModule = moduleId, IdAttestationQuestion = questionId }, _transaction);
        }

        public ICollection<ModuleQuestionGettingAllQuestionIds> GetAllQuestionIdsByModuleId(int moduleId)
        {
            var query = @"SELECT IdAttestationQuestion FROM AttestationModuleQuestion
                        WHERE IdAttestationModule = @IdAttestationModule";

            var results = _connection.Query<ModuleQuestionGettingAllQuestionIds>(query, new { IdAttestationModule = moduleId });

            return (ICollection<ModuleQuestionGettingAllQuestionIds>)results;
        }
    }
}
