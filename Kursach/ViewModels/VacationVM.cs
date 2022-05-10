using Kursach.DTO;
using Kursach.Model;
using Kursach.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kursach.ViewModels
{
    class VacationVM : BaseVM
    {
        public List<Coach> Coachs { get; set; }
        public Coach SelectedCoach { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public CommandVM SaveVacation { get; set; }

        public VacationVM()
        {
            var db = SqlModel.GetInstance();
            Coachs = db.SelectAllCoach();
            Start = DateTime.Now;
            End = DateTime.Now;
            SaveVacation = new CommandVM(() =>
            {
                if (Start.Year == 1 || End.Year == 1 || SelectedCoach == null)
                {
                    MessageBox.Show("Не указаны все данные");
                    return;
                }
                db.Insert(new Vacation
                {
                    IdCouch = SelectedCoach.ID, 
                    Start = Start,
                    End = End
                });
            });
        }
    }
}
