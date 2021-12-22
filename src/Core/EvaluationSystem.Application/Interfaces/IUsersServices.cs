using System.Collections.Generic;
using EvaluationSystem.Application.Models.Users;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IUsersServices
    {
        IEnumerable<UserDetailDto> GetAll();

        IEnumerable<UserToEvaluateDto> GetUsersToEvaluate();
    }
}
