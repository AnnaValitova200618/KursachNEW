using Kursach.DTO;
using Kursach.Model;
using Kursach.MoreWindow;
using Kursach.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kursach.ViewModels
{
    class CoachVM : BaseVM
    {
        private readonly CurrentPageControl currentPageControl;
        
        public CommandVM SaveCouch { get; set; }
        public CommandVM OpenVacation { get; set; }
        public CommandVM OpenListVacation { get; set; }

        public Coach EditCouch { get; set; }
        private Coach selectedCouch;
        public Coach SelectedCouch //выделение тренера и отображение его информации
        {
            get => selectedCouch;
            set
            {
                selectedCouch = value;
                if (selectedCouch == null)
                    return;
                EditCouch = new Coach { 
                    Birthday = selectedCouch.Birthday,
                    FirstName = selectedCouch.FirstName,
                    LastName = selectedCouch.LastName,
                    Patronymic = selectedCouch.Patronymic,
                    ID = selectedCouch.ID,
                    Phone = selectedCouch.Phone
                };
                Signal(nameof(EditCouch));
            }
        }


        private List<Coach> сoachs;
        public List<Coach> Сoachs
        {
            get => сoachs;
            set
            {
                сoachs = value;
                Signal();
            }
        }

        public CoachVM(CurrentPageControl currentPageControl)
        {
            var db = SqlModel.GetInstance();
            EditCouch = new Coach {  Birthday = DateTime.Now};

            Сoachs = SqlModel.GetInstance().CoachList();//вывод списка тренеров из БД

            OpenVacation = new CommandVM(() => { new VacationWindow().Show(); });
            OpenListVacation = new CommandVM(() => { new ListVacation().Show(); });
            SaveCouch = new CommandVM(() =>
            {
                if (EditCouch.FirstName == null || EditCouch.LastName == null || EditCouch.Patronymic == null || EditCouch.Phone == null)
                {
                    MessageBox.Show("Не указаны все данные");
                    return;
                }
                if (EditCouch.ID == 0)
                    db.Insert(EditCouch);
                else
                    db.Update(EditCouch);
                
            });
            this.currentPageControl = currentPageControl;
        }
    }
}
