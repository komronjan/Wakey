using Domain.Dtos.Account;
using Domain.Wrapper;
using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpPost("Login")]
        public async Task<Response<string>> Login ([FromBody]JWTLoginDto login)
        {
           return  await accountService.JWTLogin(login);
        }
        [HttpPost("Register")]
        public async Task<Response<IdentityResult>> Register([FromBody] UserRegisterDto model)
        {
            return await accountService.RegisterAsync(model);
        }
    }
}
