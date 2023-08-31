using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Account
{
    public class JWTLoginDto
    {
        public string  UserName { get; set; }
        public string Password  { get; set; }
    }
}
