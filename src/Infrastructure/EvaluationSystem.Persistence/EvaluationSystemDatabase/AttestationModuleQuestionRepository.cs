using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Application.Models.ModuleQuestions;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;

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

            Connection.Execute(query, new { AttestationModuleId = moduleId, AttestationQuestionId = questionId, AttestationPosition = position }, Transaction);
        }

        public void DeleteQuestionFromModule(int moduleId, int questionId)
        {
            var query = @"DELETE FROM AttestationModuleQuestion
                        WHERE IdAttestationModule = @IdAttestationModule AND IdAttestationQuestion = @IdAttestationQuestion";

            Connection.Execute(query, new { IdAttestationModule = moduleId, IdAttestationQuestion = questionId }, Transaction);
        }

        public ICollection<ModuleQuestionGettingAllQuestionIds> GetAllQuestionIdsByModuleId(int moduleId)
        {
            var query = @"SELECT IdAttestationQuestion FROM AttestationModuleQuestion
                        WHERE IdAttestationModule = @IdAttestationModule";

            var results = Connection.Query<ModuleQuestionGettingAllQuestionIds>(query, new { IdAttestationModule = moduleId });

            return (ICollection<ModuleQuestionGettingAllQuestionIds>)results;
        }
    }
}
