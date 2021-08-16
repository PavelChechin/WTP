using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WTPCore.Data.Interfaces.Base;
using WTPCore.Data.Tables.Base;

namespace EducPlanSolution
{
    class CalendarGridRow : INotifyPropertyChanged
    {
        private int cours;
        private int semestr;
        private int weekNumber;
        private string monday;
        private string tuesday;
        private string wednesday;
        private string thursday;
        private string friday;
        private string saturday;

        public int Cours
        {
            get
            {
                return cours;
            }
            set
            {
                cours = value;
                OnPropertyChanged("Cours");
            }
        }

        public int Semestr
        {
            get
            {
                return semestr;
            }
            set
            {
                semestr = value;
                OnPropertyChanged("Semestr");
            }
        }

        public int WeekNumber
        {
            get
            {
                return weekNumber;
            }
            set
            {
                weekNumber = value;
                OnPropertyChanged("WeekNumber");
            }
        }

        public string Monday
        {
            get
            {
                return monday;
            }
            set
            {
                monday = value;
                OnPropertyChanged("Monday");
            }
        }

        public string Tuesday
        {
            get
            {
                return tuesday;
            }
            set
            {
                tuesday = value;
                OnPropertyChanged("Tuesday");
            }
        }

        public string Wednesday
        {
            get
            {
                return wednesday;
            }
            set
            {
                wednesday = value;
                OnPropertyChanged("Wednesday");
            }
        }

        public string Thursday
        {
            get
            {
                return thursday;
            }
            set
            {
                thursday = value;
                OnPropertyChanged("Thursday");
            }
        }

        public string Friday
        {
            get
            {
                return friday;
            }
            set
            {
                friday = value;
                OnPropertyChanged("Friday");
            }
        }

        public string Saturday
        {
            get
            {
                return saturday;
            }
            set
            {
                saturday = value;
                OnPropertyChanged("Saturday");
            }
        }

        public CalendarGridRow()
        {

        }

        public CalendarGridRow(int day, string shortName, int weekNumber, int semNumber)
        {

        }

        void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
