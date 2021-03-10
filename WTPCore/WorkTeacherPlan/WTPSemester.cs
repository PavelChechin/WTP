using WTPCore.Data.Interfaces.Base;
using CollectionsPattern;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPSemester: DeletableRow<WTPSemesters, IWTPSEMESTER, WTPSemester>
    {
        public WTPRow WtpRow
        {
            get
            {
                return Collection.WtpRow;
            }
        }
        public override bool New
        {
            get { return !DataRow.WTPSEMESTER_ID.HasValue; }
        }
        public WTPSemester(WTPSemesters Semesters, IWTPSEMESTER DataRow)
            : base(DataRow, Semesters)
        {

        }
    }
}
