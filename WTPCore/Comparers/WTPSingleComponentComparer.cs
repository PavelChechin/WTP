using System.Collections.Generic;
using WTPCore.WorkTeacherPlan;
using System.Collections;

namespace WTPCore.Comparers
{
    public class WTPSingleComponentComparer:IComparer<WTPComponent>, IComparer
    {

        public static WTPSingleComponentComparer Instance
        {
            get;
            private set;
        }

        static WTPSingleComponentComparer()
        {
            Instance = new WTPSingleComponentComparer();
        }

        #region IComparer<WTPRow> Members

        public int Compare(WTPComponent x, WTPComponent y)
        {
            if (x == y) return 0;
            if (!x.DataRow.WTPCOMPONENT_SORTINDEX.HasValue ||
                !y.DataRow.WTPCOMPONENT_SORTINDEX.HasValue)
                return x.Name.CompareTo(y.Name);
            else
                return x.DataRow.WTPCOMPONENT_SORTINDEX.Value.CompareTo(y.DataRow.WTPCOMPONENT_SORTINDEX.Value);
        }
        #endregion

        #region IComparer Members
        public int Compare(object x, object y)
        {
            return Compare((WTPComponent)x, (WTPComponent)y);
        }

        #endregion
    }
}
