using System;
using System.Collections.Generic;
using Kursach.Tools;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.DTO
{
    [Table("type")]
    public class Types : BaseDTO
    {
        [Column("type")]
        public string type { get; set; }
        
    }
}
