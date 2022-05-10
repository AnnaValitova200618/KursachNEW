using Kursach.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.DTO
{
    [Table ("trainers")]
    public class Trainers : BaseDTO
    {
        [Column ("date")]
        public DateTime Date { get; set; }

        [Column("id_coach")]
        public int CouchId { get; set; }

        [Column("id_attendance")]
        public int AttendanceId { get; set; }

        [Column("id_type")]
        public int TypeId { get; set; }

        [Column("id_carrying")]
        public int CarryingId { get; set; }
    }
}
