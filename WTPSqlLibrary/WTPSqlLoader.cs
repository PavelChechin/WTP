using System;
using System.Linq;
using SqlDataSolution;
using WTPCore.Loader;
using WTPCore.Factory;

namespace WTPPresenter.WTPSqlData
{
    public class WTPSqlLoader : WTPLoader
    {
 public SqlData SqlData
        {
            get;
            private set;
        }
        public override WTPFactory Factory
        {
            get;
            protected set;
        }
       
        public WTPSqlLoader(SqlData SqlData)
        {
            this.SqlData = SqlData;
            Factory = new WTPSqlDataFactory(SqlData);
        }
       
        protected override T[] GetRows<T>()
        {
            Type t = Factory.GetClassType<T>();
            return SqlData.GetTable(t).Rows.Cast<T>().ToArray();
        }

        #region SqlData
        //#region Получение TipID
        //public static Int64? GetTipID(SqlConnection Connection, Int64 STUDYEAR_ID, Int64 TEACHERCHAIR_ID)
        //{
        //    DataTableContainer container = SqlData.GetDataSource<TIP>(Connection, null,
        //        new SpParameter("@STUDYEAR_ID", STUDYEAR_ID), new SpParameter("@TEACHERCHAIR_ID", TEACHERCHAIR_ID));
        //    TIP row = container.Rows.FirstOrDefault() as TIP;

        //    if (row != null)
        //        return row.TIP_ID;
        //    else
        //        return null;
        //}
        //#endregion

        //public static Int64? CreateTIP(Int64 TeacherChairID, Int64 StudYearID, SqlData SqlData)
        //{
        //    var container = SqlData.GetTable<TIP>();
        //    var newRow = container.CreateNewRow<TIP>();
        //    newRow.TEACHERCHAIR_ID = TeacherChairID;
        //    newRow.STUDYEAR_ID = StudYearID;
        //    container.AddRow<TIP>(newRow);
        //    SqlData.SaveAll();
        //    return newRow.TIP_ID;
        //}
        //public static Int64? CreateTIP(Int64 TeacherChairID, Int64 StudYearID)
        //{
        //    SqlData sqlData = new SqlData(new LocalConnection().GetConnection(), typeof(TIP));
        //    return CreateTIP(TeacherChairID, StudYearID, sqlData);
        //}

        #region (FillSqlData) Зполнение SqlData. Фактически получение данных с сервера
       
        public static bool FillSqlData(SqlData SqlData, Int64? WTP_ID)
        {
            //Logger.Start("FillSqlData");
            
            foreach (var table in SqlData.GetTables())
            {
                if (WTP_ID.HasValue)
                {
                    if (table.ContainsSqlParameter(SqlCommandType.Select, "@WTP_ID"))
                        table.SetSqlParameterValue(SqlCommandType.Select, "@WTP_ID", WTP_ID.Value);
                }               
            }
            var factory = new WTPSqlDataFactory(SqlData);
            Type[] TableTypes = factory.Types.ToArray();
            //Logger.Start("SqlData.FillTables");
            bool ok = SqlData.FillTables(TableTypes);
            //Logger.Finish("SqlData.FillTables");
            //Logger.Finish("FillSqlData");

            return ok;
        }
        #endregion
        #endregion
    }
}
