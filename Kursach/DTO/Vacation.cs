using Kursach.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.DTO
{
    [Table ("vacation")]
   public class Vacation : BaseDTO
    {
        [Column("id_couch")]
        public int IdCouch { get; set; }
        [Column ("start")]
        public DateTime Start { get; set; }
        [Column("end")]
        public DateTime End { get; set; }

        public Coach Coach { get; set; }
    }
}
