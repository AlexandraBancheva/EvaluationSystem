using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Persistence.QuestionDatabase;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class AttestationAnswerRepository : BaseRepository<AttestationAnswer>, IAttestationAnswerRepository
    {
        public AttestationAnswerRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public ICollection<AttestationAnswer> GetAllByQuestionId(int questionId)
        {
            var query = @"SELECT * FROM AttestationAnswer
                        WHERE IdQuestion = @QuestionId";

            var queryParameter = new { QuestionId = questionId };
            var attestationAnswers = Connection.Query<AttestationAnswer>(query, queryParameter, transaction: Transaction);

            return (ICollection<AttestationAnswer>)attestationAnswers;
        }
    }
}
