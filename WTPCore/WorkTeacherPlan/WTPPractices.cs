using CollectionsPattern;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPPractices : DeletableCollection<WTPPractice>
    {
        //public Wtp Wtp
        //{
        //    get;
        //    private set;
        //}

        public WTPRow WtpRow
        {
            get;
            private set;
        }

        public WTPPractices(WTPRow WtpRow)
        {
            this.WtpRow = WtpRow;
            this.WtpRow.DataRow.PropertyChanged += DataRow_PropertyChanged;
        }

        void DataRow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WTPROW_ID")
            {
                foreach (WTPPractice practice in this)
                {
                    practice.DataRow.WTPROW_ID = WtpRow.DataRow.WTPROW_ID;
                }
            }
        }
        //public WTPPractices(Wtp Wtp)
        //{
        //    this.Wtp = Wtp;
        //}



        /// <summary>
        /// Заполнение значения
        /// </summary> 
        public void FillRow(IWTPPRACTICE row)
        {
            row.WTPPRACTICE_ID = null;
            row.WTPROW_ID = WtpRow.DataRow.WTPROW_ID;
        }

        public void Add(WTPPractice Row)
        {
            AddToList(Row);
        }
        /// <summary>
        /// Добавление существующего значения
        /// </summary>   
        public WTPPractice Add(IWTPPRACTICE DataRow, bool FromBase)
        {
            WTPPractice newRow = new WTPPractice(this, DataRow);
            Add(newRow);
            return newRow;
        }
    }
}