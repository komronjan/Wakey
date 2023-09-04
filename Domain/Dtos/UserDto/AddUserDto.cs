using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.UserDto
{
    public class AddUserDto : UserBaseDto
    {
        public IFormFile? PofileIMage { get; set; }
    }
}
