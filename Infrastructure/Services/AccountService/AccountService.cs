using Domain.Dtos.Account;
using Domain.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountService(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }
        public async Task<Response<string>> JWTLogin(JWTLoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
            var checkPassword = await userManager.CheckPasswordAsync(user, model.Password);
                if (checkPassword != false) 
                {
                    var token = await GenerateJwtToken(user);
                return new Response<string>(token);
                }
            }
            return new Response<string>(HttpStatusCode.BadRequest, "Email or Password is incorrect");
        }
        //Method to generate The Token
        private async Task<string> GenerateJwtToken(IdentityUser user)
        {
            // key
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // claims
            var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

            //add roles
            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            // token from all Informations
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token); //
        }

        public async Task<Response<IdentityUser>> LoginAsync(UserLoginDto model)
        {
            var existing = await userManager.FindByEmailAsync(model.Email);
            if (existing == null)
            {
                return new Response<IdentityUser>(HttpStatusCode.BadRequest, "Email or Password is incorrect");
            }
            var checkPassword = await userManager.CheckPasswordAsync(existing, model.Password);
            if (!checkPassword)
            {
               return new Response<IdentityUser>(HttpStatusCode.BadRequest, "Email or Password is incorrect");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, existing.UserName),
                new Claim(ClaimTypes.Email, existing.Email)
            }; 
            await signInManager.SignInWithClaimsAsync(existing, model.RememberMe, claims);
            return new Response<IdentityUser>(existing);
        }

        public async Task<Response<IdentityResult>> RegisterAsync(UserRegisterDto model)
        {
            try
            {
                var user = new IdentityUser()
                {
                    UserName=model.Username,
                    Email = model.Email,
                };
                var result = await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    return new Response<IdentityResult>(result);
                }
                    return new Response<IdentityResult>(HttpStatusCode.BadRequest, result.Errors.Select(e => e.Description).ToList());
            }
            catch (Exception ex)
            {
                return new Response<IdentityResult> (HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}   
