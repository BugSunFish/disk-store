using DiskStore.Contracts;
using DiskStore.Data;
using DiskStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiskStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        public DiskStoreDbContext _db;

        public UserController(DiskStoreDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_db.Users.ToList());
        }

        [HttpGet("{id:guid}")]
        public ActionResult<IEnumerable<User>> GetById(Guid id)
        {
            return Ok(_db.Users.Find(id));
        }

        [HttpPost]
        public ActionResult<IEnumerable<User>> Post([FromBody] UserDTO userDTO)
        {
            _db.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Name = userDTO.Name,
                Email = userDTO.Email,
                PasswordHash = SecurePasswordHasher.Hash(userDTO.Password)
            }); 
            _db.SaveChanges();
            return Ok();
        }

        [HttpPut("{id:guid}")]
        public ActionResult<IEnumerable<User>> Put(Guid id, [FromBody] UserDTO userDTO)
        {
            if (_db.Users.Find(id) is User user) {

                user.Name = userDTO.Name;
                user.Email = userDTO.Email;
                user.PasswordHash = SecurePasswordHasher.Hash(userDTO.Password);

                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult<int> Delete(Guid id) 
        {
            if (_db.Users.Find(id) is User user)
            {
                _db.Users.Remove(user);

                return Ok(id);
            }
            return BadRequest();
        }
    }
}
