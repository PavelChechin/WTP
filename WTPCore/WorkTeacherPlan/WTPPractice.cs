using System;
using WTPCore.Data.Interfaces.Base;
using CollectionsPattern;
using System.Collections;
using System.ComponentModel;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPPractice : BaseSimpleRow<IWTPPRACTICE>, INotifyPropertyChanged
    {
        
        public bool New
        {
            get { return !DataRow.WTPPRACTICE_ID.HasValue; }
        }

        public WTPPractice(IWTPPRACTICE PracticeDataRow)
            : base(PracticeDataRow)
        {

        }


    }
}
