﻿using System.Collections.Generic;
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

        public void DeleteUserByEmail(string email)
        {
            var query = @" DELETE FROM [User]
                        WHERE Email = @Email";

            Connection.Execute(query, new { Email = email}, Transaction);
        }

        public ICollection<User> GetAllUsers()
        {
            var query = @"SELECT * FROM [User]";

            var users = Connection.Query<User>(query, Transaction);

            return (ICollection<User>)users;
        }

        public User GetUserByEmail(string email)
        {
            var query = @"SELECT * FROM [User]
                        WHERE Email = @Email";

            var result = Connection.QueryFirstOrDefault<User>(query, new { Email = email }, Transaction);

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

            var users = Connection.Query<UserToEvaluateDto>(query, new { IdParticipant = idParticipant }, Transaction);

            return users;
        }
    }
}
