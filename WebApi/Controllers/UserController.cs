using Domain.Dtos.UserDto;
using Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromForm]AddUserDto model)
        {
            var result = await service.AddUser(model);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await service.GetAllUser();
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById( int id)
        {
            var result = await service.GetById(id);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromForm] int id)
        {
            var result = await service.DeleteUser(id);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromForm] AddUserDto model)
        {
            var result = await service.UpdateUser(model);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
