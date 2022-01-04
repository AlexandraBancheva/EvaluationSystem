using System.Collections.Generic;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Domain.Entities;

namespace EvaluationSystem.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllUsers();

        IEnumerable<UserToEvaluateDto> GetUsersToEvaluate(string email);

        User GetUserByEmail(string email);
    }
}
