using AutoMapper;
using HealthAPI.Dtos;
using HealthAPI.Repositories.Interfaces;
using HealthAPI.Utils;
using System.Threading.Tasks;

namespace HealthAPI.Repositories.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public UserService(IRepositoryManager repository,
            ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUser(string userId, bool trackChanges)
        {
            var user = await _repository.User.GetUser(userId, trackChanges);

            if (user is null)
                throw new UserNotFoundException(userId);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> GetUserByEmail(string email, bool trackChanges)
        {
            var user = await _repository.User.GetUserByEmail(email, trackChanges);

            if (user is null)
                throw new UserNotFoundException(email);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
