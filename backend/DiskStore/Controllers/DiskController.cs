using DiskStore.Contracts;
using DiskStore.Data;
using DiskStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiskStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiskController : Controller
    {
        public DiskStoreDbContext _db;

        public DiskController(DiskStoreDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Disk>> Get()
        {
            return Ok(_db.Disks.ToList());
        }

        [HttpPost]
        public ActionResult<IEnumerable<Disk>> Post([FromForm] Disk disk)
        {
            //if(_db.)

            return Ok();
        }
    }
}
