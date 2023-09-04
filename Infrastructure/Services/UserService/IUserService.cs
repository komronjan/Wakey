using Domain.Dtos.UserDto;
using Domain.Wrapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.UserService
{
    public interface IUserService
    {
        Task<Response<List<GetUserDto>>> GetAllUser();
        Task<Response<GetUserDto>> AddUser( AddUserDto model );
        Task<Response<GetUserDto>> UpdateUser(AddUserDto model);
        Task<Response<GetUserDto>> GetById(int id);
        Task<Response<string>> DeleteUser(int id);

    }
}
