using System.Threading.Tasks;
using System.Collections.Generic;
using EvaluationSystem.Application.Models.Users;

namespace EvaluationSystem.Application.Interfaces
{
    public interface IUsersServices
    {
        Task<ICollection<UserDetailDto>> GetAll();

        IEnumerable<UserToEvaluateDto> GetUsersToEvaluate();
    }
}
