using System.ComponentModel;
using WTPCore.Data.Interfaces.Base;
using CollectionsPattern;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPSemesters : CascadeCollection<WTPSemester>
    {
        public WTPRow WtpRow
        {
            get;
            private set;
        }
        public WTPSemesters(WTPRow WtpRow)
        {
            this.WtpRow = WtpRow;
            this.WtpRow.DataRow.PropertyChanged += WtpRow_PropertyChanged;
        }

        void WtpRow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName == "WTPROW_ID")
            {
                foreach (WTPSemester row in this)
                {
                    row.DataRow.WTPROW_ID = WtpRow.DataRow.WTPROW_ID;
                }
            }
        }

        public override ICascadeRow GetDeletedDataRow(WTPSemester Value)
        {
            return Value.DataRow;
        }

        #region Добавление семестра

        /// <summary>
        /// Заполнение семестра
        /// </summary>       
        public void FillRow(IWTPSEMESTER row)
        {
            row.WTPSEMESTER_ID = null;
            row.WTPROW_ID = WtpRow.DataRow.WTPROW_ID;
        }

        public void Add(WTPSemester Row)
        {
            AddToList(Row);
        }
        /// <summary>
        /// Добавление существующей дисциплины
        /// </summary>
        /// <param name="DataRow">Идентификатор дисциплины</param>
        /// <param name="FromBase"></param>
        /// <returns></returns>
        public WTPSemester Add(IWTPSEMESTER DataRow, bool FromBase)
        {
            WTPSemester newRow = new WTPSemester(this, DataRow);
            Add(newRow);
            return newRow;
        }
        #endregion 
        public override void Dispose()
        {
            WtpRow.DataRow.PropertyChanged -= WtpRow_PropertyChanged;
            base.Dispose();
        }
    }
}
