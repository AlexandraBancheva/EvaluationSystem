using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Graph;
using AutoMapper;
using Azure.Identity;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Application.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly string[] scopes = new[] { "https://graph.microsoft.com/.default" };
        private const string tenantId = "50ae1bf7-d359-4aff-91ac-b084dc52111e";
        private const string clientId = "dc32305c-c493-44e0-9654-0de398e76d50";
        private const string clientSecret = "1m57Q~ClngoPOBs-AQzcLuRnrQIXYyoX5-yLQ";


        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUser _currentUser;

        public UsersServices(IUserRepository userRepository, IMapper mapper, IUser currentUser)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<ICollection<UserDetailDto>> GetAll()
        {
            var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

            var userFromCloud = await UpdatingUsersInDatabase(graphClient);
            userFromCloud = userFromCloud.OrderBy(x => x.Id).ToList();
            return userFromCloud;
        }

        public IEnumerable<UserToEvaluateDto> GetUsersToEvaluate()
        {
            var participantId = _currentUser.Id;
            var res = _userRepository.GetUsersToEvaluate(participantId);
            return res;
        }

        private async Task<ICollection<UserDetailDto>> UpdatingUsersInDatabase(GraphServiceClient graphClient)
        {
            var users = await graphClient
                            .Users
                            .Request()
                            .Filter("(accountEnabled eq true)")
                            .GetAsync();

            var allUsers = new List<User>();

            while (true)
            {
                foreach (var user in users.CurrentPage)
                {
                    if (user.UserPrincipalName.EndsWith("@vsgbg.com") && !user.UserPrincipalName.EndsWith("#EXT#@vsgbg.com"))
                    {
                        allUsers.Add(user);
                    }
                }

                if (users.NextPageRequest == null)
                {
                    break;
                }
                users = await users.NextPageRequest.GetAsync();
            }

            var usersFromCloud = _mapper.Map<ICollection<UsersFromCloudDto>>(allUsers);
            var dbUsers = _userRepository.GetAll();

            if (usersFromCloud.Count > dbUsers.Count)
            {
                foreach (var currUser in usersFromCloud)
                {
                    var user = _userRepository.GetUserByEmail(currUser.Email);

                    if (user == null)
                    {
                        var userName = currUser.Name;
                        user = new Domain.Entities.User
                        {
                            Name = userName,
                            Email = currUser.Email,
                        };
                        _userRepository.Insert(user);
                    }
                }
            }
            else if (dbUsers.Count > usersFromCloud.Count)
            {
                foreach (var dbUser in dbUsers)
                {
                    var isExist = false;

                    foreach (var currentUser in usersFromCloud)
                    {
                        if (dbUser.Email == currentUser.Email)
                        {
                            isExist = true;
                            break;
                        }
                    }

                    if (isExist == false)
                    {
                        _userRepository.DeleteUserByEmail(dbUser.Email);
                    }
                }
            }
            return _mapper.Map<ICollection<UserDetailDto>>(dbUsers);
        }
    }
}
