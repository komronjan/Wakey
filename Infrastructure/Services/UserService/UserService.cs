using Domain.Dtos.UserDto;
using Domain.Entities;
using Domain.Wrapper;
using System.Net;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Infrastructure.Services.FileService;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext context;
        private readonly IFileService fileService;

        public UserService(DataContext context, IFileService fileService)
        {
            this.context = context;
            this.fileService = fileService;
        }
        public async Task<Response<GetUserDto>> AddUser(AddUserDto model)
        {
            try
            {
                var fileName = fileService.CreateFile("images", model.PofileIMage);
                var user = new User()
                {
                    Id = model.Id,
                    City = model.City,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProfileImage = fileName
                };
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                var _user = new GetUserDto()
                {
                    Id = user.Id,
                    City = model.City,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    FileName = fileName
                };
                return new Response<GetUserDto>(_user);
            }
            catch (Exception ex)
            {
                return new Response<GetUserDto>(HttpStatusCode.InternalServerError, new List<string?> { ex.Message });
            }
        }

        public async Task<Response<string>> DeleteUser(int id)
        {
            try
            {
                var find = await context.Users.FindAsync(id);
                context.Users.Remove(find);
                await context.SaveChangesAsync();
                return new Response<string>("Okey");
            }
            catch (Exception ex)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, new List<string?> { ex.Message });
            }
        }

        public async Task<Response<List<GetUserDto>>> GetAllUser()
        {
            try
            {
                var result = await context.Users.Select(x => new GetUserDto
                {
                    Id = x.Id,
                    City = x.City,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    FileName = x.ProfileImage
                }).OrderBy(x => x.Id).ToListAsync();
                return new Response<List<GetUserDto>>(result);
            }
            catch (Exception ex)
            {
                return new Response<List<GetUserDto>>(HttpStatusCode.InternalServerError, new List<string?> { ex.Message });
            }
        }

        public async Task<Response<GetUserDto>> GetById(int id)
        {
            try
            {
                var find = await context.Users.FindAsync(id);
                var user = new GetUserDto()
                {
                    Id = find.Id,
                    FirstName = find.FirstName,
                    LastName = find.LastName,
                    FileName = find.ProfileImage,
                    City = find.City
                };
                return new Response<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                return new Response<GetUserDto>(HttpStatusCode.InternalServerError, new List<string?> { ex.Message });
            }
        }

        public async Task<Response<GetUserDto>> UpdateUser(AddUserDto model)
        {
            try
            {
                var find = await context.Users.FindAsync(model.Id);
                find.Id = model.Id;
                find.FirstName = model.FirstName;
                find.LastName = model.LastName;
                find.City = model.City;
                string fileName = null;
                if (model.PofileIMage != null && find.ProfileImage != null)
                {
                    fileService.DeleteFile("images", find.ProfileImage);
                    fileName = fileService.CreateFile("images", model.PofileIMage);
                    find.ProfileImage = fileName;
                }
                else if (model.PofileIMage != null && find.ProfileImage == null)
                {
                    fileName = fileService.CreateFile("images", model.PofileIMage);
                    find.ProfileImage = fileName;
                }
                await context.SaveChangesAsync();
                var user = new GetUserDto()
                {
                    Id = find.Id,
                    FirstName = find.FirstName,
                    LastName = find.LastName,
                    FileName = find.ProfileImage,
                    City = find.City
                };
                return new Response<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                return new Response<GetUserDto>(HttpStatusCode.InternalServerError, new List<string?> { ex.Message });
            }
        }
    }
}
