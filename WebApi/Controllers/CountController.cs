using Domain.Dtos.CountDto;
using Domain.Dtos.UserDto;
using Infrastructure.Services.CountService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountController : ControllerBase
    {
        private readonly ICountService countService;

        public CountController(ICountService countService)
        {
            this.countService = countService;
        }
        [HttpGet("GetCount")]
        public async Task<IActionResult> Get()
        {
            var result = await countService.GetCount();
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPost("AddCount")]
        public async Task<IActionResult> Add(AddCountDto model)
        {
            var result = await countService.AddCount(model);
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpDelete("DeleteCount")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await countService.DeleteCount(id);
            return StatusCode((int)result.StatusCode, result);
        }
       
    }
}
