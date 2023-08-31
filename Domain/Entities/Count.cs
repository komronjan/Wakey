using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Count
    {
        [Key]
        public DateTime Date { get; set; }
        [NotNull]
        public long Steps { get; set; }
        [NotNull]
        public long Diamonds { get; set; }
        [NotNull]
        public long  ScoreColoriya { get; set; }
    }
}
