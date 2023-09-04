using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.CountDto
{
    public class GetCountDto : CountBaseDto
    {
        public string UserName { get; set; }
    }
}
