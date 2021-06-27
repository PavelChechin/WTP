using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WTPCore.Data.Interfaces.Base;
using WTPCore.WorkTeacherPlan;

namespace WTPCoreExample
{
    class WTPGridRow : INotifyPropertyChanged
    {
        private string parentCycle;
        private string childCycle;
        private string childCycleCode;
        private string name;
        private string code;
        private long? number;
        private long? semNum;
        private object lections;
        private object exams;
        private object practics;
        private object labWorks;
        private object midTerms;
        private object referats;
        private object calcGraphWorks;
        private object coursProjects;
        private object coursWorks;
        private object totalHours;
        private object ksr;
        private object independentWork;
        

        public string ParentCycle
        {
            get
            {
                return parentCycle;
            }
            set
            {
                parentCycle = value;
                OnPropertyChanged("ParentCycle");
            }
        }

        public string ChildCycle
        {
            get
            {
                return childCycle;
            }
            set
            {
                childCycle = value;
                OnPropertyChanged("ChildCycle");
            }
        }

        public string ChildCycleCode
        {
            get
            {
                return childCycleCode;
            }
            set
            {
                childCycleCode = value;
                OnPropertyChanged("ChildCycle");
            }
        }

        public string Discip_Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Discip_Name");
            }
        }

        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }

        public long? Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }

        public long? SemNum
        {
            get
            {
                return semNum;
            }
            set
            {
                semNum = value;
                OnPropertyChanged("SemNum");
            }
        }

        public object Lections
        {
            get
            {
                return lections;
            }
            set
            {
                lections = value;
                OnPropertyChanged("Lections");
            }
        }

        public object Exams
        {
            get
            {
                return exams;
            }
            set
            {
                exams = value;
                OnPropertyChanged("Exams");
            }
        }

        public object Practics
        {
            get
            {
                return practics;
            }
            set
            {
                practics = value;
                OnPropertyChanged("Practics");
            }
        }

        public object LabWorks
        {
            get
            {
                return labWorks;
            }
            set
            {
                labWorks = value;
                OnPropertyChanged("LabWorks");
            }
        }

        public object MidTerms
        {
            get
            {
                return midTerms;
            }
            set
            {
                midTerms = value;
                OnPropertyChanged("MidTerms");
            }
        }

        public object Referats
        {
            get
            {
                return referats;
            }
            set
            {
                referats = value;
                OnPropertyChanged("Referats");
            }
        }

        public object CalcGraphWorks
        {
            get
            {
                return calcGraphWorks;
            }
            set
            {
                calcGraphWorks = value;
                OnPropertyChanged("CalcGraphWorks");
            }
        }

        public object CoursProjects
        {
            get
            {
                return coursProjects;
            }
            set
            {
                coursProjects = value;
                OnPropertyChanged("CoursProjects");
            }
        }

        public object CoursWorks
        {
            get
            {
                return coursWorks;
            }
            set
            {
                coursWorks = value;
                OnPropertyChanged("CoursWorks");
            }
        }

        public object TotalHours
        {
            get
            {
                return totalHours;
            }
            set
            {
                totalHours = value;
                OnPropertyChanged("TotalHours");
            }
        }

        public object KSR
        {
            get
            {
                return ksr;
            }
            set
            {
                ksr = value;
                OnPropertyChanged("KSR");
            }
        }

        public object IndependentWork
        {
            get
            {
                return independentWork;
            }
            set
            {
                independentWork = value;
                OnPropertyChanged("IndependentWork");
            }
        }

        public WTPGridRow(string parentComponent, WTPComponent childComponent, WTPRow row, WTPRowValues values, long? sem_num)
        {
            ParentCycle = parentComponent;
            ChildCycle = childComponent.DataRow.WTPCOMPONENT_NAME;
            ChildCycleCode = row.Component.DataRow.WTPCOMPONENT_CODE;
            Discip_Name = row.DataRow.STUDDISCIPLINE_NAME;
            SemNum = sem_num;
            Code = row.Component.DataRow.WTPCOMPONENT_CODE +"."+ row.DataRow.WTPROW_NUMBER;
            Number = row.DataRow.WTPROW_SORTINDEX;

            foreach (WTPRowValue value in values)
            {
                switch (value.DataRow.WTPPARAM_ID)
                {
                    case 16:
                        Exams = value.DataRow.WTPROWVALUES_VALUE;
                        break;

                    case 17:
                        MidTerms = value.DataRow.WTPROWVALUES_VALUE;
                        break;

                    case 7:
                        Lections = value.DataRow.WTPROWVALUES_VALUE;
                        break;

                    case 10:
                        Practics = value.DataRow.WTPROWVALUES_VALUE;
                        break;

                    case 12:
                        LabWorks = value.DataRow.WTPROWVALUES_VALUE;
                        break;

                    case 14:
                        Referats = value.DataRow.WTPROWVALUES_VALUE;
                        break;

                    case 13:
                        CalcGraphWorks = value.DataRow.WTPROWVALUES_VALUE;
                        break;

                    case 18:
                        CoursProjects = value.DataRow.WTPROWVALUES_VALUE;
                        break;

                    case 19:
                        CoursWorks = value.DataRow.WTPROWVALUES_VALUE;
                        break;

                    case 34:
                        TotalHours = value.DataRow.WTPROWVALUES_VALUE;
                        break;

                    case 28:
                        IndependentWork = value.DataRow.WTPROWVALUES_VALUE;
                        break;

                    case 24:
                        KSR = value.DataRow.WTPROWVALUES_VALUE;
                        break;

                    default:
                        continue;
                }
            }
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
