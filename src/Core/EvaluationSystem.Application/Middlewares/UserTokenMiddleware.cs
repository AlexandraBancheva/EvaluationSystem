using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Application.Middlewares
{
    public class UserTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public UserTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository, IUser currentUser)
        {
            var userEmail = context.User.Claims.FirstOrDefault(u => u.Type == "preferred_username")?.Value;
            var user = userRepository.GetAllUsers().FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
            {
                var username = context.User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                var userId = userRepository.Insert(new Domain.Entities.User() { Email = userEmail, Name = username});
            }

            currentUser.Id = user.IdUser;
            currentUser.Name = user.Name;
            currentUser.Email = user.Email;

            await _next.Invoke(context);
        }
    }
}
