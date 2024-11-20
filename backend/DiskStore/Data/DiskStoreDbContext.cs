using DiskStore.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskStore.Data
{
    public class DiskStoreDbContext : DbContext
    {
        public DiskStoreDbContext(DbContextOptions<DiskStoreDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Disk> Disks { get; set; }
    }
}
