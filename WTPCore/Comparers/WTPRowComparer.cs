using System.Collections.Generic;
using WTPCore.WorkTeacherPlan;
using System.Collections;

namespace WTPCore.Comparers
{
    public class WTPRowComparer:IComparer<WTPRow>, IComparer
    {

        public static WTPRowComparer Instance
        {
            get;
            private set;
        }

        static WTPRowComparer()
        {
            Instance = new WTPRowComparer();
        }

        #region IComparer<WTPRow> Members

        public int Compare(WTPRow x, WTPRow y)
        {
            if (x == y) return 0;
            if (x.Component != y.Component)
                return WTPComponentComparer.Instance.Compare(x.Component, y.Component);
            else
                return WTPSingleRowComparer.Instance.Compare(x, y);
        }

        #endregion
        #region IComparer Members

        public int Compare(object x, object y)
        {
            return Compare((WTPRow)x, (WTPRow)y);
        }

        #endregion
    }
}
