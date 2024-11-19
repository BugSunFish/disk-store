using DiskStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskStore.API
{
    public class DiskStoreDbContext : DbContext
    {
        public DiskStoreDbContext(DbContextOptions<DiskStoreDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
