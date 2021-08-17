using CollectionsPattern;
using WTPCore.Data.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTPCore.WorkTeacherPlan
{
    public class CalendarGraphvalue : DeletableRow<CalendarGraphvalueCollection, ICALENDARGRAPHVALUES, CalendarGraphvalue>
    {
        public override bool New
        {
            get { return !DataRow.CALENDARGRAPHVALUES_ID.HasValue; }
        }
        public CalendarGraphvalue(CalendarGraphvalueCollection CalendarValues, ICALENDARGRAPHVALUES CalendarDataRow)
                   : base(CalendarDataRow, CalendarValues)
        {

        }
 
    }
}
