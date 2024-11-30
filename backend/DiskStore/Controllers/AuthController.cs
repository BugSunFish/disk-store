using DiskStore.Contracts;
using DiskStore.Data;
using DiskStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml.Linq;

namespace DiskStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DiskStoreDbContext _db;
        public AuthController(DiskStoreDbContext db)
        {
            _db = db;
        }

        [HttpPost("/reg")]
        public async Task<IResult> Registrate([FromBody] UserRegistrate data)
        {
            if (!ModelState.IsValid)
                return TypedResults.BadRequest(ModelState);

            if (_db.Users.FirstOrDefault(user => user.Name == data.Name) != null)
                return TypedResults.Conflict("This name already exists");

            _db.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Name = data.Name,
                Email = data.Email,
                PasswordHash = SecurePasswordHasher.Hash(data.Password),
                Created = DateTime.UtcNow,
            });
            await _db.SaveChangesAsync();

            return TypedResults.Ok("Registration was successful");
        }

        [HttpPost("/log")]
        public async Task<IResult> Login([FromBody] UserLogin data)
        {
            if (!ModelState.IsValid)
                return TypedResults.BadRequest(ModelState);

            if (_db.Users.FirstOrDefault(user => user.Name == data.Name) is User user)
            {
                if (SecurePasswordHasher.Hash(data.Password) != user.PasswordHash)
                    return TypedResults.BadRequest("Password is not correct");

                var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
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
                    username = user.Name,
                };

                return TypedResults.Ok(responce);
            }

            return TypedResults.Unauthorized();
        }
    }
}
