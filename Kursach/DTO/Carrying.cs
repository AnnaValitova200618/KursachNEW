using Kursach.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.DTO
{
    [Table("carrying")]
    public class Carrying : BaseDTO
    {
        [Column("carrying")]
        public string carrying { get; set; }
    }
}
