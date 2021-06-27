using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;

namespace WTPPresenter.WTPSqlData.DataRowContainers.Base
{
    [DataTableName("WTPVARIATION")]
    [SqlCommands(null, "SPU_WTPVARIATION_NEXTVALUE_SEL", null, null)]
    [SaveIndex(1)]
    public class WTPVARIATION : WTPDRContainer, IWTPVARIATION
    {
        [DataColumnName("ID")]
        [AllowDBNull]
        [AutoIncrement]
        public Int32 ID
        {
            get
            {
                return GetData<Int32>("ID");
            }
            set
            {
                SetData<Int32>("ID", value);

            }
        }
        [DataColumnName("WTPVARIATION_ID")]
        [AllowDBNull]
        public Int64 WTPVARIATION_ID
        {
            get
            {
                return GetData<Int64>("WTPVARIATION_ID");
            }
            set
            {
                SetData<Int64>("WTPVARIATION_ID", value);
               
            }
        }
    }
}
