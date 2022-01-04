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

            var result = _connection.Query<User>(query, new { Email = email }, _transaction);

            return (User)result;
        }

        public IEnumerable<UserToEvaluateDto> GetUsersToEvaluate(string email)
        {
            var query = @"SELECT a.Id AS IdAttestation, a.IdForm, u.[Email] 
                        FROM [User] AS u
                        JOIN [Attestation] AS a ON a.IdUserToEvaluate = u.IdUser
                        JOIN [AttestationParticipant] AS ap ON ap.IdAttestation = a.Id
                        JOIN [User] AS ue ON ap.IdUserParticipant = ue.IdUser
                        WHERE ue.Email = @Email AND ap.[Status] = 1";

            var users = _connection.Query<UserToEvaluateDto>(query, new { Email = email }, _transaction);

            return users;
        }
    }
}
