using System;
using System.Linq;
using CollectionsPattern;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPVariations : BaseCollection<WTPVariation>
    {
        Wtp Wtp;
        public WTPVariations(Wtp Wtp)
        {
            this.Wtp = Wtp;
            //Refresh();
        }
        public WTPVariation New()
        {
            return Add(-1);
        }
        public WTPVariation Add(Int64 VariationID)
        {
            IWTPVARIATION newVariation = Wtp.Factory.CreateRow<IWTPVARIATION>();
            if (VariationID > 0)
            {
                newVariation.WTPVARIATION_ID = VariationID;
                newVariation.AcceptChanges();
            }
            WTPVariation rowVariation = new WTPVariation(this, newVariation);
            AddToList(rowVariation);
            return rowVariation;
        }

        public int Refresh()
        {
            var wtpRows = Wtp.Rows
                .Where(pd=>pd.DataRow.WTPROW_VARIATIONID > 0);
            foreach (var wtpRow in wtpRows)
            {
                var findedVariations = lst.Where
                    (ds => ds.DataRow.WTPVARIATION_ID == wtpRow.DataRow.WTPROW_VARIATIONID);
                WTPVariation variation;
                if (findedVariations.Count() == 0)
                {
                    variation = Add(wtpRow.DataRow.WTPROW_VARIATIONID);
                }
                else
                {
                    variation = findedVariations.First();
                }
                variation.AddRow(wtpRow);
            }
            CorrectVariations();
            return lst.Count;
        }

        public void CorrectVariations()
        {
            var removeVariation = this.Where(str => str.Rows.Where(
                dd => !dd.Deleted && !dd.Disposed).Count() < 2).ToArray();
            foreach (WTPVariation variation in removeVariation)
            {
                Remove(variation);
            }
        }
    }
}
