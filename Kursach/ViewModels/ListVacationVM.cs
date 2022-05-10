using Kursach.DTO;
using Kursach.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach.ViewModels
{
    class ListVacationVM : BaseVM
    {
        public List<Coach> Coaches { get; set; }
        

        private List<Vacation> vacation;
        public List<Vacation> Vacation
        {
            get => vacation;
            set
            {
                vacation = value;
                Signal();
            }
        }


        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ListVacationVM()
        {
            var db = SqlModel.GetInstance();
            Vacation = SqlModel.GetInstance().ListVacation();
        }


    }
}
