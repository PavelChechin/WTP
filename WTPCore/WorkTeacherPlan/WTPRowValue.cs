using WTPCore.Data.Interfaces.Base;
using CollectionsPattern;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPRowValue : DeletableRow<WTPRowValues, IWTPROWVALUES, WTPRowValue>
    {
        public WTPParam Param
        {
            get;
            private set;
        }

        public string ParamName
        {
            get;
            private set;
        }

        public override bool New
        {
            get { return !DataRow.WTPROWVALUES_ID.HasValue; }
        }
        public WTPRowValue(WTPRowValues Values, IWTPROWVALUES DataRow)
            :base(DataRow, Values)
        {
            Param = Values.WtpRow.Wtp.Params.GetByID(DataRow.WTPPARAM_ID.GetValueOrDefault());

            ParamName = Param == null ? string.Empty : Param.DataRow.WTPPARAM_NAME;
        }

        public bool IsDefaultValue()
        {
            return Param.IsDefaultValue(DataRow.WTPROWVALUES_VALUE);
        }
    }
}
