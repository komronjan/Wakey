using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.CountDto
{
    public class CountBaseDto
    { 
        public int UserId { get; set; }
      
        public long Steps { get; set; }
   
        public long Diamonds { get; set; }
       
        public long ScoreColoriya { get; set; }
    }
}
