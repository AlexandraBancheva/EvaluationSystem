using System.Collections.Generic;
using Dapper;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Persistence.QuestionDatabase
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public IEnumerable<User> GetAllUsers()
        {
            var query = @"SELECT * FROM [User]";

            var users = _connection.Query<User>(query, _transaction);

            return users;
        }

        public User GetUserByEmail(string email)
        {
            var query = @"SELECT * FROM [User]
                        WHERE Email = @Email";

            var result = _connection.QueryFirstOrDefault<User>(query, new { Email = email }, _transaction);

            return result;
        }

        public IEnumerable<UserToEvaluateDto> GetUsersToEvaluate(int idParticipant)
        {
            var query = @"SELECT [at].Id AS IdAttestation, [at].IdForm, u.[Email] 
                            FROM Attestation AS [at]
                            LEFT JOIN [User] AS u ON [at].IdUserToEvaluate = u.Id
                            LEFT JOIN AttestationForm AS af ON [at].IdForm = af.Id
                            LEFT JOIN AttestationParticipant AS ap ON ap.IdAttestation = [at].Id
                            WHERE ap.IdUserParticipant = @IdParticipant AND ap.[Status] = 'Open'";

            var users = _connection.Query<UserToEvaluateDto>(query, new { IdParticipant = idParticipant }, _transaction);

            return users;
        }
    }
}
