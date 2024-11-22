using DiskStore.Contracts;
using DiskStore.Data;
using DiskStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DiskStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly DiskStoreDbContext _db;

        public AuthController(DiskStoreDbContext db)
        {
            _db = db;
        }

        [HttpPost("reg")]
        public ActionResult Registration([FromForm] UserRegistration userData)
        {
            var name = userData.Name;
            var email = userData.Email;
            var password = userData.Password;

            if (ModelState.IsValid)
            {
                if (_db.Users.FirstOrDefault(user => user.Name == name) != null)
                    return Conflict("This name is exists");

                if (_db.Users.FirstOrDefault(user => user.Email == email) != null)
                    return Conflict("This email is registered");

                _db.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    PasswordHash = SecurePasswordHasher.Hash(password),
                    Created = DateTime.UtcNow,
                    PostedDisks = new List<Disk>(),
                });
                _db.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("log")]
        public ActionResult Login([FromForm] UserLogin userData) 
        {
            var name = userData.Name;
            var password = userData.Password;
            
            if (ModelState.IsValid)
            {
                if (_db.Users.FirstOrDefault(user => user.Name == name) is User user)
                {
                    if (SecurePasswordHasher.Hash(password) != user.PasswordHash)
                        return BadRequest("Password is not corrected");

                    var claims = new List<Claim> { new Claim(ClaimTypes.UserData, user.Id.ToString()), new Claim(ClaimTypes.Name, name) };
                    var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(2),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                    var responce = new
                    {
                        access_token = encodedJwt,
                        username = name,
                    };

                    return Ok(responce);
                }

                return Unauthorized();
            }

            return BadRequest();
        }
    }
}
