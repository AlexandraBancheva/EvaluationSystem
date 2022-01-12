﻿using System.Collections.Generic;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        ICollection<User> GetAllUsers();

        IEnumerable<UserToEvaluateDto> GetUsersToEvaluate(int idParticipant);

        User GetUserByEmail(string email);

        void DeleteUserByEmail(string email);
    }
}
