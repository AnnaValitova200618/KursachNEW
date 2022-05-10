using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kursach.DTO;
using System.Threading.Tasks;
using Kursach.Tools;
using Kursach.Model;
using System.Windows;
using Kursach.Pages;
using Kursach.MoreWindow;

namespace Kursach.ViewModels
{
    class TrainersVM : BaseVM
    {
        private readonly CurrentPageControl currentPageControl;

        public CommandVM OpenAttendance { get; private set; }
        public List<Types> typesArray { get; set; }
        public Types SelectedType { get; set; }

        public List<Carrying> carryingsArray { get; set; }
        public Carrying SelectedCarrying { get; set; }

        public List<Coach> Coachs { get; set; } 
        public Coach SelectedCoach { get; set; }

        public List<Attendance> attendances { get; set; }

        public DateTime Date { get; set; }
        public CommandVM SaveTrainers { get; set; }

        public TrainersVM(CurrentPageControl currentPageControl)
        {
            var db = SqlModel.GetInstance();
            Date = DateTime.Now;
            typesArray = db.SelectType();
            carryingsArray = db.SelectCarrying();
            Coachs = db.SelectAllCoach();

            SaveTrainers = new CommandVM(() =>
            {
                if (SelectedType == null || SelectedCarrying == null || SelectedCoach == null)
                {
                    MessageBox.Show("Не указаны все данные");
                    return;
                }
                db.Insert(new Trainers
                {
                    Date = Date,
                    TypeId = SelectedType.ID,
                    CarryingId = SelectedCarrying.ID,
                    CouchId = SelectedCoach.ID
                });
            });
            this.currentPageControl = currentPageControl;


            OpenAttendance = new CommandVM(() =>
            {new AttendanceWindow().Show(); });

        }
    }
}
