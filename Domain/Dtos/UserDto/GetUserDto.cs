using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.UserDto
{
    public class GetUserDto : UserBaseDto
    {
        public string FileName { get; set; }
    }
}
