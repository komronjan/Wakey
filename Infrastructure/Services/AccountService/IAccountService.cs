using Domain.Dtos.Account;
using Domain.Wrapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.AccountService
{
    public interface IAccountService
    {
        Task<Response<IdentityResult>> RegisterAsync(UserRegisterDto model);
        Task<Response<IdentityUser>> LoginAsync(UserLoginDto model);
        Task<Response<string>> JWTLogin(JWTLoginDto model);

    }
}
