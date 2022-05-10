using Kursach.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.DTO
{
    [Table ("attendance")]
   public class Attendance : BaseDTO
    {
        [Column ("date")]
        public DateTime Date { get; set; }

        [Column ("id_sportsman")]
        public int SportsmanId { get; set; }

    }
}
