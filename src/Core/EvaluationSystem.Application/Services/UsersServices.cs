using System.Collections.Generic;
using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Application.Repositories;

namespace EvaluationSystem.Application.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUser _currentUser;

        public UsersServices(IUserRepository userRepository, IMapper mapper, IUser currentUser)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public IEnumerable<UserDetailDto> GetAll()
        {
            var results = _userRepository.GetAllUsers();

            return _mapper.Map<IEnumerable<UserDetailDto>>(results);
        }

        public IEnumerable<UserToEvaluateDto> GetUsersToEvaluate()
        {
            var res = _userRepository.GetUsersToEvaluate(_currentUser.Email);
            return res;
        }
    }
}
