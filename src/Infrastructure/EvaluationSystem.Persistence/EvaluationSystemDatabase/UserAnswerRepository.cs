using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Persistence.QuestionDatabase;

namespace EvaluationSystem.Persistence.EvaluationSystemDatabase
{
    public class UserAnswerRepository : BaseRepository<UserAnswer>, IUserAnswerRepository
    {
        public UserAnswerRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public void ChangeStatusToDone(int attestationId, int idUserParticipant)
        {
            var query = @"UPDATE AttestationParticipant
                        SET [Status] = 'Done'
                        WHERE IdAttestation = @AttestationId AND IdUserParticipant = @IdUserParticipant";

            Connection.Execute(query, new { AttestationId = attestationId, IdUserParticipant = idUserParticipant }, Transaction);
        }

        public ICollection<UserAnswer> GetAllAnswersByUser(int attestationId, int userId)
        {
            var query = @"SELECT * FROM [UserAnswer]
                        WHERE IdAttestation = @IdAttestation AND IdUserParticipant = @IdUserParticipant";

            var results = Connection.Query<UserAnswer>(query, new { IdAttestation = attestationId, IdUserParticipant = userId}, Transaction);

            return (ICollection<UserAnswer>)results;
        }
    }
}
