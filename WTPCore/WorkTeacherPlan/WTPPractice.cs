using System;
using WTPCore.Data.Interfaces.Base;
using CollectionsPattern;
using System.Collections;
using System.ComponentModel;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPPractice : BaseSimpleRow<IWTPPRACTICE>, INotifyPropertyChanged
    {
        
        public bool New
        {
            get { return !DataRow.WTPPRACTICE_ID.HasValue; }
        }

        public WTPPractice(WTPPractices Practices, IWTPPRACTICE PracticeDataRow)
            : base(PracticeDataRow)
        {

        }


    }
}
