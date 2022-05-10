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
    class SportsmanVM : BaseVM
    {
        private readonly CurrentPageControl currentPageControl;

        public CommandVM SaveSportsman { get; set; } // сохранение спортсмена

    
        public List<Coach> Сoachs { get; set; } // для выбора тренера
       

        private Coach sportsmanCoach;
        public Coach SportsmanCoach
        {
            get => sportsmanCoach;
            set
            {
                sportsmanCoach = value;
                Signal();
            }
        }

        public List<string> Sportscategorys { get; set; }
        public string SelectedSportscategory { get; set; }

        public Sportsman EditSportsman { get; set; }

        private Sportsman selectedSportsman;
        public Sportsman SelectedSportsman
        {
            get => selectedSportsman;
            set
            {
                selectedSportsman = value;
                if (selectedSportsman == null)
                    return;
                EditSportsman = new Sportsman
                {
                    Bithday = selectedSportsman.Bithday,
                    FirstName = selectedSportsman.FirstName,
                    LastName = selectedSportsman.LastName,
                    Patronymic = selectedSportsman.Patronymic,
                    ID = selectedSportsman.ID,
                    Phone = selectedSportsman.Phone,
                    Sportscategory = selectedSportsman.Sportscategory,
                    IdCoach = SportsmanCoach.ID
                };
                Signal(nameof(EditSportsman));
            }

        }


        private List<Sportsman> sportsmans;
        public List<Sportsman> Sportsmans //вывод списка спортсменов
        {
            get => sportsmans;
            set
            {
                sportsmans = value;
                Signal();
            }
        }
      
        public SportsmanVM(CurrentPageControl currentPageControl)
        {

            var db = SqlModel.GetInstance();
            EditSportsman = new Sportsman { Bithday = DateTime.Now }; //создание спортсмена
            Sportsmans = SqlModel.GetInstance().SportsmanList();//вывод списка спортсменов
            Сoachs = db.SelectAllCoach(); //выбор тренера
            Sportscategorys = new List<string>(new string[] //выбор разряда
            {
                "б/р",
                "3 ю.",
                "2 ю.",
                "1 ю.",
                "3 в.",
                "2 в.",
                "1 в.",
                "КМС",
                "МС" });
            
            SaveSportsman = new CommandVM(() =>
            {
                if (EditSportsman.FirstName == null || EditSportsman.LastName == null || EditSportsman.Patronymic == null )
                {
                    MessageBox.Show("Не указаны все данные");
                    return;
                }
                if (EditSportsman.ID == 0)
                    db.Insert(EditSportsman);
                else
                    db.Update(EditSportsman);
                
            });
            this.currentPageControl = currentPageControl;
        }
      
    }
}
