using DiskStore.Contracts;
using DiskStore.Contracts.Disks;
using DiskStore.Contracts.Users;
using DiskStore.Data;
using DiskStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DiskStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisksController : ControllerBase
    {
        private readonly DiskStoreDbContext _db;
        public DisksController(DiskStoreDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<DiskFullDto>> Get()
        {

            return await _db.Disks
                .Include(u => u.User)
                .Select(d => new DiskFullDto
                {
                    Id = d.Id,
                    Title = d.Title,
                    Description = d.Description,
                    Author = d.Author,
                    Created = d.Created,
                    Updated = d.Updated,
                    User =  new UserDto
                    {
                        Id = d.User.Id,
                        Name = d.User.Name,
                        Email = d.User.Email,
                        Created = d.User.Created,
                        Updated = d.User.Updated,
                    }
                })
                .ToListAsync();
        }

        [HttpPost]
        [Authorize]
        public async Task<IResult> Post([FromBody] DiskCreate data)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (_db.Users.FirstOrDefault(user => user.Id == userId) is User user)
            {
                var disk = new Disk
                {
                    Id = Guid.NewGuid(),
                    Title = data.Title,
                    Description = data.Description,
                    Author = data.Author,
                    Created = DateTime.UtcNow,
                    User = user,
                };
                user.Disks.Add(disk);
                _db.Disks.Add(disk);
                _db.SaveChanges();

                return TypedResults.Ok("Disk was created");
            }

            return TypedResults.BadRequest("User is not found");
        }
    }
}
