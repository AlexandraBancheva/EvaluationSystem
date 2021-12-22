using System.Collections.Generic;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        IEnumerable<UserToEvaluateDto> GetUsersToEvaluate(string email);
    }
}
