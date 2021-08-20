using CollectionsPattern;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPRowGroups:DeletableCollection<WTPRowGroup>
    {
        public Wtp Wtp
        {
            get;
            private set;
        }

        public WTPRowGroups(Wtp Wtp)
        {
            this.Wtp = Wtp;
        }

        #region Добавление строки
        /// <summary>
        /// Заполнение строки
        /// </summary>
        /// <param name="rowGroup">Строка</param>
        public void FillRow(IWTPROWGROUP rowGroup)
        {
            rowGroup.WTPROWGROUP_ID = null;
            rowGroup.WTP_ID = Wtp.DataRow.WTP_ID;
        }

        public void Add(WTPRowGroup rowGroup)
        {
            AddToList(rowGroup);
        }

        /// <summary>
        /// Добавление существующей дисциплины
        /// </summary>
        /// <param name="DataRow">Идентификатор дисциплины</param>
        /// <param name="FromBase"></param>
        /// <returns></returns>
        public WTPRowGroup Add(IWTPROWGROUP DataRow, bool FromBase)
        {
            WTPRowGroup newGroupRow = new WTPRowGroup(this, DataRow);
            Add(newGroupRow);
            return newGroupRow;
        }
        #endregion
    }
}
