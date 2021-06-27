using System;
using System.ComponentModel;
using WTPCore.Data.Interfaces.Base;
using CollectionsPattern;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPRowValues : CascadeCollection<WTPRowValue>
    {
        public WTPRow WtpRow
        {
            get;
            private set;
        }
        public WTPRowValues(WTPRow WtpRow)
        {
            this.WtpRow = WtpRow;
            this.WtpRow.DataRow.PropertyChanged += DataRow_PropertyChanged;
        }

        void DataRow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WTPROW_ID")
            {
                foreach (WTPRowValue row in this)
                {
                    row.DataRow.WTPROW_ID = WtpRow.DataRow.WTPROW_ID;
                }
            }
        }

        public override ICascadeRow GetDeletedDataRow(WTPRowValue Value)
        {
            return Value.DataRow;
        }

        #region Добавление значения

        /// <summary>
        /// Заполнение значения
        /// </summary>       
        public void FillRow(IWTPROWVALUES row)
        {
            row.WTPROWVALUES_ID = null;
            row.WTPROW_ID = WtpRow.DataRow.WTPROW_ID;
        }

        public void Add(WTPRowValue Row)
        {
            AddToList(Row);
        }
        /// <summary>
        /// Добавление существующего значения
        /// </summary>   
        public WTPRowValue Add(IWTPROWVALUES DataRow, bool FromBase)
        {
            WTPRowValue newRow = new WTPRowValue(this, DataRow);
            Add(newRow);
            return newRow;
        }
        #region Добавление существующего значения
        /// <summary>
        /// Добавление существующего значения
        /// </summary>
        /// <param name="DataRow">Идентификатор строки</param>
        /// <returns></returns>
        public WTPRowValue Add(Int64 WTPPARAM_ID, short? SemNum)
        {
            IWTPROWVALUES newRow = WtpRow.Wtp.Factory.CreateRow<IWTPROWVALUES>();
            newRow.WTPPARAM_ID = WTPPARAM_ID;
            newRow.WTPROWVALUES_SEMNUM = SemNum;
            FillRow(newRow);
            return Add(newRow, false);
        }
        #endregion
        /// <summary>
        /// Получение значения по названию колонки. 
        /// </summary>
        /// <param name="ColumnName">Название колонки</param>
        /// <param name="AutoCreate">Создавать значение, если не найдено</param>
        /// <returns></returns>
        public WTPRowValue GetWTPValue(string ParamName, short? SemNum, bool AutoCreate)
        {
            foreach (WTPRowValue value in this)
            {
                if (value.ParamName == ParamName && value.DataRow.WTPROWVALUES_SEMNUM == SemNum)
                {
                    return value;
                }
            }
            if (AutoCreate)
            {
                WTPParam param = WtpRow.Wtp.Params[ParamName];
                if (param != null)
                    return Add(param.DataRow.WTPPARAM_ID.GetValueOrDefault(), SemNum);
                else
                    return null;
            }
            return null;
        }
        public WTPRowValue GetWTPValue(string ParamName, short? SemNum)
        {
            return GetWTPValue(ParamName, SemNum, false);
        }

        #endregion 
    }
}
