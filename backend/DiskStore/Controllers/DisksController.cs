using DiskStore.Contracts;
using DiskStore.Data;
using DiskStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IEnumerable<Disk>> Get()
        {
            return _db.Disks.ToList();
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
                    PublisherId = userId,
                    Publisher = user,
                };
                user.PostedDisks.Add(disk);
                _db.Disks.Add(disk);

                return TypedResults.Ok("Disk was created");
            }

            return TypedResults.BadRequest("User is not found");
            
        }
    }
}
