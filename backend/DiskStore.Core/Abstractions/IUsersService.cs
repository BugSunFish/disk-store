using DiskStore.Core.Contracts.Requests;
using DiskStore.Core.Contracts.Responses;

namespace DiskStore.Application.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<UserResponse> GetUserByIdAsync(Guid id);
        Task<Guid> CreateUserAsync(UserRequest newData);
        Task<Guid> UpdateUserAsync(Guid id, UserRequest newData);
        Task<Guid> DeleteUserAsync(Guid id);
    }
}