using System.Collections.Generic;
using WTPCore.WorkTeacherPlan;
using System.Collections;

namespace WTPCore.Comparers
{
    public class WTPSingleRowComparer:IComparer<WTPRow>, IComparer
    {

        public static WTPSingleRowComparer Instance
        {
            get;
            private set;
        }

        static WTPSingleRowComparer()
        {
            Instance = new WTPSingleRowComparer();
        }

        #region IComparer<WTPRow> Members

        public int Compare(WTPRow x, WTPRow y)
        {

            if (x == y) return 0;
            if (x.Variation != null && x.Variation == y.Variation)
            {
                if (!x.DataRow.WTPROW_SORTINDEX.HasValue ||
                !y.DataRow.WTPROW_SORTINDEX.HasValue)
                    return x.DataRow.WTPROW_INDEX.CompareTo(y.DataRow.WTPROW_INDEX);
                else
                    return x.DataRow.WTPROW_SORTINDEX.Value.CompareTo(y.DataRow.WTPROW_SORTINDEX.Value);
            }
            else
            {
                int? sortIndexX = x.Variation == null ? x.DataRow.WTPROW_SORTINDEX : x.Variation.VariationSortIndex;
                int? sortIndexY = y.Variation == null ? y.DataRow.WTPROW_SORTINDEX : y.Variation.VariationSortIndex;

                if (!sortIndexX.HasValue ||
                    !sortIndexY.HasValue)
                {
                    string discNameX = x.Variation == null ? x.DataRow.STUDDISCIPLINE_NAME : x.Variation.VariationName;
                    string discNameY = y.Variation == null ? y.DataRow.STUDDISCIPLINE_NAME : y.Variation.VariationName;

                    string indexX = x.Variation == null ? x.DataRow.WTPROW_INDEX : x.Variation.VariationIndex;
                    string indexY = y.Variation == null ? y.DataRow.WTPROW_INDEX : y.Variation.VariationIndex;

                    if (!string.IsNullOrWhiteSpace(indexX) && !string.IsNullOrWhiteSpace(indexY))
                        return WTPIndexComparer.Instance.Compare(indexX, indexY);
                    else if (string.IsNullOrWhiteSpace(indexX) && string.IsNullOrWhiteSpace(indexY))
                        return discNameX.CompareTo(discNameY);
                    else if (string.IsNullOrWhiteSpace(indexX))
                        return +1; //x пустой, поэтому он больше (ниже)  -1 - x-меньше,   +1 - x-больше
                    else  
                        return -1; //y больше (ниже)
                }
                else
                    return sortIndexX.Value.CompareTo(sortIndexY.Value);
            }
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
