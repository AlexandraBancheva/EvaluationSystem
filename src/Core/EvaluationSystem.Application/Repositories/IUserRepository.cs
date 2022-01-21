using System.Collections.Generic;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Application.Models.Users;

namespace EvaluationSystem.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void DeleteUserByEmail(string email);

        User GetUserByEmail(string email);

        ICollection<User> GetAllUsers();

        IEnumerable<UserToEvaluateDto> GetUsersToEvaluate(int idParticipant);
    }
}
