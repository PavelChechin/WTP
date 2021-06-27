using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;

namespace WTPPresenter.WTPSqlData.DataRowContainers.Base
{
	[DataTableName("WTPPARAMINFO")]
    public class WTPPARAMINFO : WTPDRContainer, IWTPPARAMINFO
	{
		[DataColumnName("WTPPARAMINFO_ID")]
		[AllowDBNull]
		public Int64? WTPPARAMINFO_ID
		{
			get
			{
				return GetData<Int64?>("WTPPARAMINFO_ID");
			}
			set
			{
				SetData<Int64?>("WTPPARAMINFO_ID", value);
			}
		}

		[DataColumnName("WTPPARAMINFO_NAME")]
		[AllowDBNull]
		public String WTPPARAMINFO_NAME
		{
			get
			{
				return GetData<String>("WTPPARAMINFO_NAME");
			}
			set
			{
				SetData<String>("WTPPARAMINFO_NAME", value);
			}
		}

		[DataColumnName("WTPPARAMINFO_DESCRIPTION")]
		[AllowDBNull]
		public String WTPPARAMINFO_DESCRIPTION
		{
			get
			{
				return GetData<String>("WTPPARAMINFO_DESCRIPTION");
			}
			set
			{
				SetData<String>("WTPPARAMINFO_DESCRIPTION", value);
			}
		}


	}
}
