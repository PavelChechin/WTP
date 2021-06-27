using System;
using ServerHelper;
using SqlDataSolution;
using WTPCore.Data;
using WTPCore.Factory;
using WTPPresenter.WTPSqlData.DataRowContainers.Base;

namespace WTPPresenter.WTPSqlData
{
    public class WTPSqlDataFactory : WTPFactory
    {
        public SqlData SqlData
        {
            get;
            private set;
        }
        static Type[] SqlDataTypes;

        static WTPSqlDataFactory()
        {
            Type tipType = typeof(WTP);
            var types = tipType.Assembly.GetTypes();
            string nameSpace = tipType.Namespace;
            //Получили все интересующие нас типы
            SqlDataTypes = SqlData.GetTypes(types, nameSpace);            
        }
        protected void SetSqlData(SqlData SqlData)
        {
            this.SqlData = SqlData;
        }
        public WTPSqlDataFactory()
        {
            SetSqlData(CreateSqlData());
        }
        public WTPSqlDataFactory(SqlData SqlData)
        {
            SetSqlData(SqlData);
        }

        protected override Type[] GetTypes()
        {
            return SqlDataTypes;
        }
        
        public override IWTPDATAROW CreateRow(Type RowType)
        {
            Type containerType = GetClassType(RowType);
            var table = SqlData.GetTable(containerType);
            var newRow = table.CreateNewRow();
            table.AddRow(newRow);
            return (IWTPDATAROW)(object)newRow;
        }

        public static SqlData CreateSqlData()
        {
            SqlData sqlData = new SqlData(ConnectionHelper.GetConnection(), SqlDataTypes);            
            return sqlData;
        }
    }
}
