using CollectionsPattern;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPComponents : DeletableCollection<WTPComponent>
    {
         public Wtp Wtp
        {
            get;
            private set;
        }
         public WTPComponents(Wtp Wtp)
        {
            this.Wtp = Wtp;
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
        public void FillRow(IWTPCOMPONENT row)
         {
             row.WTPCOMPONENT_ID = null;
             row.WTP_ID = Wtp.DataRow.WTP_ID;
         }

         public void Add(WTPComponent Row)
         {
             AddToList(Row);
         }
         /// <summary>
         /// Добавление существующей дисциплины
         /// </summary>
         /// <param name="DataRow">Идентификатор дисциплины</param>
         /// <param name="FromBase"></param>
         /// <returns></returns>
         public WTPComponent Add(IWTPCOMPONENT DataRow, bool FromBase)
         {
             WTPComponent newRow = new WTPComponent(this, DataRow);
             Add(newRow);
             return newRow;
         }
         #endregion   

         
    }
}
