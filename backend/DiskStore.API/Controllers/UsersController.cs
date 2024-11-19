using DiskStore.Application.Services;
using DiskStore.Core.Contracts.Requests;
using DiskStore.Core.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DiskStore.API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UsersController : Controller
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetUsersAsync()
        {
            return Ok(await _usersService.GetAllUsersAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<UserResponse>>> GetUserAsync(Guid id)
        {
            return Ok(await _usersService.GetUserByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<List<UserResponse>>> PostUserAsync([FromBody] UserRequest user)
        {
            return Ok(await _usersService.CreateUserAsync(user));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<List<UserResponse>>> PutUserAsync(Guid id, [FromBody] UserRequest user)
        {
            return Ok(await _usersService.UpdateUserAsync(id, user));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<List<UserResponse>>> PutUserAsync(Guid id)
        {
            return Ok(await _usersService.DeleteUserAsync(id));
        }
    }
}
