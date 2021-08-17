using CollectionsPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.WorkTeacherPlan
{
    public class CalendarGraphvalueCollection : DeletableCollection<CalendarGraphvalue>
    {
        public Wtp Wtp
        {
            get;
            private set;
        }
        public CalendarGraphvalueCollection(Wtp Wtp)
        {
            this.Wtp = Wtp;
        }

        #region Добавление строки

        public void FillRow(ICALENDARGRAPHVALUES row)
        {
            row.CALENDARGRAPHVALUES_ID = null;
            row.WTP_ID = Wtp.DataRow.WTP_ID;
        }

        public void Add(CalendarGraphvalue Row)
        {
            AddToList(Row);
        }

        public CalendarGraphvalue Add(ICALENDARGRAPHVALUES DataRow)
        {
            CalendarGraphvalue newRow = new CalendarGraphvalue(this, DataRow);
            Add(newRow);
            return newRow;
        }
        #endregion 
    }
}
