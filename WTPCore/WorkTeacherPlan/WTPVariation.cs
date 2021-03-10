using System.Collections.Generic;
using System.Linq;
using CollectionsPattern;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPVariation : BaseRow<WTPVariations, IWTPVARIATION, WTPVariation>
    {
        public int RowCount
        {
            get
            {
                return Rows.Count;
            }
        }
        public List<WTPRow> Rows
        {
            get;
            private set;
        }
        public string VariationName
        {
            get;
            private set;
        }
        public string VariationIndex
        {
            get;
            private set;
        }

        public int? VariationSortIndex
        {
            get;
            private set;
        }

        private void CorrectDiscName(string DiscName)
        {
            if (string.IsNullOrEmpty(VariationName) || VariationName.CompareTo(DiscName) > 0)
                VariationName = DiscName;
        }
        private void CorrectDiscIndex(string DiscIndex)
        {
            if (string.IsNullOrEmpty(VariationIndex) || VariationIndex.CompareTo(DiscIndex) > 0)
                VariationIndex = DiscIndex;
        }
        private void CorrectSortIndex(int? SortIndex)
        {
            if (!VariationSortIndex.HasValue || VariationSortIndex.Value.CompareTo(SortIndex) > 0)
                VariationSortIndex = SortIndex;
        }
        public void AddRow(WTPRow Row)
        {
            if (!Rows.Contains(Row))
            {
                Rows.Add(Row);
                CorrectDiscName(Row.DataRow.STUDDISCIPLINE_NAME);
                CorrectDiscIndex(Row.DataRow.WTPROW_INDEX);
                CorrectSortIndex(Row.DataRow.WTPROW_SORTINDEX);
                Row.Variation = this;
            }
        }

        public void RemoveRow(WTPRow Row)
        {
            if (Rows.Contains(Row))
            {
                Rows.Remove(Row);

                VariationName = Rows.Where(dd => !dd.Deleted && !dd.Disposed)
                    .Min(d => d.DataRow.STUDDISCIPLINE_NAME);
                VariationIndex = Rows.Where(dd => !dd.Deleted && !dd.Disposed)
                    .Min(d => d.DataRow.WTPROW_INDEX);
                VariationSortIndex = Rows.Where(dd => !dd.Deleted && !dd.Disposed)
                    .Min(d => d.DataRow.WTPROW_SORTINDEX);
                Row.Variation = null;          
            }
        }
        public void ClearDisc()
        {
            while (Rows.Count > 0)
            {
                RemoveRow(Rows[0]);
            }
        }
        public override void Dispose()
        {            
            ClearDisc();
            base.Dispose();
        }
        public WTPVariation(WTPVariations Variations, IWTPVARIATION DataRow)
            : base(DataRow, Variations)
        {
            VariationName = string.Empty;
            VariationIndex = string.Empty;
            VariationSortIndex = null;
            Rows = new List<WTPRow>();
        }
    }
}
