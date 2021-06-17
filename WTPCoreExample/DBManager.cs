using ISEnvironmentSolution;
using ISEnvironmentSolution.DataStores;
using RefDataStores.General;
using SqlDataSolution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTPCore.Data;
using WTPCore.Data.Interfaces.Base;
using WTPCore.Data.SourceInrefaces;
using WTPCore.Loader;
using WTPPresenter.WTPSqlData;
using WTPPresenter.WTPSqlData.DataRowContainers.Base;

namespace WTPCoreExample
{
    public class DBManager
    {
        SqlData sqlData;
        WTPSqlDataFactory factory;

        protected T[] GetRows<T>() where T : IWTPDATAROW
        {
            Type t = factory.GetClassType<T>();
            return sqlData.GetTable(t).Rows.Cast<T>().ToArray();
        }

        public DBManager()
        {
            factory = new WTPSqlDataFactory();

            sqlData = factory.SqlData;

            Type t = factory.GetClassType<IWTPPARAM>();
            sqlData.FillTable(t);
        }

        Hashtable hashRows = new Hashtable();
        
        public T[] GetRowsInternal<T>() where T : IWTPDATAROW
        {
            string key = typeof(T).Name;
            T[] rows;
            if (hashRows.ContainsKey(key))
            {
                rows = (T[])hashRows[key];
            }
            else
            {
                rows = GetRows<T>();
                
                hashRows.Add(key, rows);
            }
            return rows;
        }

        public T CreateRow<T>() where T : IWTPDATAROW
        {
            return factory.CreateRow<T>();
        }

        public WTPLoader Load(Int64 wtpId)
        {
            bool result = WTPSqlLoader.FillSqlData(sqlData, wtpId);
            Console.WriteLine(result);

            return new WTPSqlLoader(sqlData);
        }

        public void Save(params IWTPDATAROW[] rows)
        {
            sqlData.Save(rows.Cast<DataRowContainer>().ToArray());
        }

        public void SaveAll()
        {
            sqlData.SaveAll();
        }

        public static TableDataStore GetDataSourse<T>()
        {
            TableDataStore ds = ISEnvironment.DataStoreFactory.CreateStore<TableDataStore>();
            ds.RowType = typeof(T);
            ds.Fill("");
            return ds;
        }

        public TableDataStore GetDataSourсe<T>()
        {
            TableDataStore ds = ISEnvironment.DataStoreFactory.CreateStore<TableDataStore>();
            ds.RowType = typeof(T);
            ds.Fill("SPU_SPECIALFACULTY_SEL");   //параметр указан для обхода исключения "invalid object name 'OTP'"
            return ds;
        }
    }
}
