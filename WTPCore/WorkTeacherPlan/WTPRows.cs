using CollectionsPattern;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPRows:DeletableCollection<WTPRow>
    {
        public Wtp Wtp
        {
            get;
            private set;
        }
        public WTPRowGroup RowGroup
        {
            get;
            private set;
        }
        public WTPRows(Wtp Wtp)
        {
            this.Wtp = Wtp;
        }
        public WTPRows(WTPRowGroup RowGroup)
        {
            this.RowGroup = RowGroup;
        }

        #region Добавление строки

        /// <summary>
        /// Заполнение строки
        /// </summary>
        /// <param name="row">Строка</param>
        /// <param name="ROWCHAIR_ID">Идентификатор дисциплины в нагрузке на кафедру</param>
        /// <param name="Name">Название дисциплины</param>
        /// <param name="SpecNumb">Шифр специальности</param>
        /// <param name="Chanded">Изменение</param>
        /// <param name="Notes">Примечания</param>
        public void FillRow(IWTPROW row)
        {
            row.WTPROW_ID = null;
            row.WTP_ID = Wtp.DataRow.WTP_ID;
        }

        public void Add(WTPRow Row)
        {
            AddToList(Row);            
        }
        /// <summary>
        /// Добавление существующей дисциплины
        /// </summary>
        /// <param name="DataRow">Идентификатор дисциплины</param>
        /// <param name="FromBase"></param>
        /// <returns></returns>
        public WTPRow Add(IWTPROW DataRow, bool FromBase)
        {
            WTPRow newRow = new WTPRow(this, DataRow);
            Add(newRow);
            return newRow;
        }
        #endregion   
    }
}
