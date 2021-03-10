using System.Collections.Generic;
using WTPCore.WorkTeacherPlan;
using System.Collections;

namespace WTPCore.Comparers
{
    public class WTPComponentComparer:IComparer<WTPComponent>, IComparer
    {

        public static WTPComponentComparer Instance
        {
            get;
            private set;
        }

        static WTPComponentComparer()
        {
            Instance = new WTPComponentComparer();
        }

        #region IComparer<WTPRow> Members
       
        public int Compare(WTPComponent x, WTPComponent y)
        {
            WTPComponent parentX = x.Parent;
            WTPComponent parentY = y.Parent;
            WTPComponent xi = parentX == null || x.Level < y.Level ? x : parentX;
            WTPComponent yi = parentY == null || y.Level < x.Level ? y : parentY;
            if (xi == x && yi == y)
            {
                return WTPSingleComponentComparer.Instance.Compare(xi, yi);
            }
            else
            {
                int res = Compare(xi, yi);
                if (res == 0)
                {
                    if (xi == x && yi != y)
                        res = -1;
                    else if (xi != x && yi == y)
                        res = 1;
                    else
                        res = WTPSingleComponentComparer.Instance.Compare(x, y);
                }
                return res;
            }
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
