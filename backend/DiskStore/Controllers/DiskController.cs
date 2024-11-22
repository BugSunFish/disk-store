using DiskStore.Contracts;
using DiskStore.Data;
using DiskStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace DiskStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DiskController : Controller
    {
        private readonly DiskStoreDbContext _db;

        public DiskController(DiskStoreDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Disk>> Get()
        {
            Console.WriteLine("CALLING OF GET DISKS");
            return Ok(_db.Disks.ToList());
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Disk> GetById(Guid id)
        {
            return Ok(_db.Disks.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public async Task<ActionResult<Disk>> Post([FromForm] DiskRequest disk)
        {
            if (User.FindFirst(ClaimTypes.UserData).Value is string userId)

                if (_db.Users.FirstOrDefault(x => x.Id.ToString() == userId) is User user)
                {
                    var newDisk = new Disk
                    {
                        Id = Guid.NewGuid(),
                        Title = disk.Title,
                        Description = disk.Description,
                        Author = disk.Author,
                        Created = DateTime.UtcNow,
                        PublisherId = user.Id,
                        Publisher = user,
                    };
                    _db.Disks.Add(newDisk);
                    _db.SaveChanges();
                    return Ok(newDisk);
                }
            
            return BadRequest();
        }
    }
}
