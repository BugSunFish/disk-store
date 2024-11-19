using DiskStore.API;
using DiskStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskStore.DataAccess.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly DiskStoreDbContext _context;
        public UserRepository(DiskStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task CreateAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (await _context.Users.FindAsync(id) is User user)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
