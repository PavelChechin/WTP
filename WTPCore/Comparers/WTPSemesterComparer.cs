using System.Collections.Generic;
using System.Collections;
using WTPCore.WorkTeacherPlan;

namespace WTPCore.Comparers
{
    public class WTPSemesterComparer : IComparer<WTPSemester>, IComparer
    {
        public static WTPSemesterComparer Instance
        {
            get;
            private set;
        }

        static WTPSemesterComparer()
        {
            Instance = new WTPSemesterComparer();
        }

        #region IComparer<WTPSemester> Members

        public int Compare(WTPSemester x, WTPSemester y)
        {
            if (x == y) return 0;
            if (x.WtpRow != y.WtpRow)
                return WTPRowComparer.Instance.Compare(x.WtpRow, y.WtpRow);
            else
            {
                return x.DataRow.WTPSEMESTER_NUM.CompareTo(
                    y.DataRow.WTPSEMESTER_NUM);
            }
        }

        #endregion

        #region IComparer Members
        public int Compare(object x, object y)
        {
            return Compare((WTPSemester)x, (WTPSemester)y);
        }

        #endregion
    }
}
