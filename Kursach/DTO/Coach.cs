using Kursach.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Kursach.DTO
{
    [Table ("couch")]
    public class Coach : BaseDTO
    {
        [Column("firstname")]
        public string FirstName { get; set; }

        [Column("lastname")]
        public string LastName { get; set; }

        [Column("patronymic")]
        public string Patronymic { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("bithday")]
        public DateTime Birthday { get; set; }

        

    }
}
