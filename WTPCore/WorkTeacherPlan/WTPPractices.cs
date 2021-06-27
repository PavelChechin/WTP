using CollectionsPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPPractices : DeletableCollection<WTPPractice>
    {
        public Wtp Wtp
        {
            get;
            private set;
        }


        public WTPPractices(Wtp Wtp)
        {
            this.Wtp = Wtp;
        }


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

        public void Add(WTPPractice Row)
        {
            AddToList(Row);
        }
        /// <summary>
        /// Добавление существующего значения
        /// </summary>   
        public WTPPractice Add(IWTPPRACTICE DataRow, bool FromBase)
        {
            WTPPractice newRow = new WTPPractice(this, DataRow);
            Add(newRow);
            return newRow;
        }
    }
}