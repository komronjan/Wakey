using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName  { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string  UserName { get; set; }
    }
}
