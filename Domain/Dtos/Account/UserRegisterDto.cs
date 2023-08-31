using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Account
{
    public class UserRegisterDto
    {

        [Required(ErrorMessage = "Please fill up email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please fill up password")]
        public string Password { get; set; }
    }
}
