using DiskStore.Contracts;
using DiskStore.Contracts.Disks;
using DiskStore.Contracts.Users;
using DiskStore.Data;
using DiskStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiskStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DiskStoreDbContext _db;
        public UsersController(DiskStoreDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<UserFullDto>> Get()
        {
            return await _db.Users
                .Include(u => u.Disks)
                .Select(u => new UserFullDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Created = u.Created,
                    Updated = u.Updated,
                    Disks = u.Disks.Select(d => new DiskDto
                    {
                        Id = d.Id,
                        Title = d.Title,
                        Description = d.Description,
                        Author = d.Author,
                        Created = d.Created,
                        Updated = d.Updated
                    }).ToList()
                })
                .ToListAsync();
        }

    }
}
