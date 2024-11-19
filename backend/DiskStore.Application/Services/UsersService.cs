using DiskStore.Core.Contracts;
using DiskStore.Core.Contracts.Requests;
using DiskStore.Core.Contracts.Responses;
using DiskStore.Core.Models;
using DiskStore.DataAccess.Repositories;

namespace DiskStore.Application.Services
{
    public class UsersService : IUsersService
    {
        public IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _usersRepository.GetAllAsync();

            return users.Select(u => new UserResponse
            {
                Username = u.Username,
                Email = u.Email
            });
        }

        public async Task<UserResponse> GetUserByIdAsync(Guid id)
        {
            var user = await _usersRepository.GetByIdAsync(id);

            return new UserResponse
            {
                Username = user.Username,
                Email = user.Email,
            };
        }

        public async Task<Guid> CreateUserAsync(UserRequest newData)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = newData.Username,
                Email = newData.Email,
                PasswordHash = Hasher.HashPassword(newData.Password),
            };

            await _usersRepository.CreateAsync(user);

            return user.Id;
        }

        public async Task<Guid> UpdateUserAsync(Guid id, UserRequest newData)
        {
            if (await _usersRepository.GetByIdAsync(id) is User user)
            {
                await _usersRepository.UpdateAsync(new User
                {
                    Id = Guid.NewGuid(),
                    Username = newData.Username,
                    Email = newData.Email,
                    PasswordHash = Hasher.HashPassword(newData.Password),
                });

            }

            return id;
        }

        public async Task<Guid> DeleteUserAsync(Guid id)
        {
            await _usersRepository.DeleteAsync(id);

            return id;
        }
    }
}
