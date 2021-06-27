using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;

namespace WTPPresenter.WTPSqlData.DataRowContainers.Base
{
	[DataTableName("CALENDARGRAPHVALUES")]
	[SaveIndex(1)]
	public class CALENDARGRAPHVALUES : WTPDRContainer, ICALENDARGRAPHVALUES
    {
		[DataColumnName("CALENDARGRAPHVALUES_ID")]
		[AllowDBNull]
		public Int64? CALENDARGRAPHVALUES_ID
		{
			get
			{
				return GetData<Int64?>("CALENDARGRAPHVALUES_ID");
			}
			set
			{
				SetData<Int64?>("CALENDARGRAPHVALUES_ID", value);
			}
		}

		
		[DataColumnName("WTP_ID")]
		[AllowDBNull]
		public Int64? WTP_ID
		{
			get
			{
				return GetData<Int64?>("WTP_ID");
			}
			set
			{
				SetData<Int64?>("WTP_ID", value);
			}
		}

		[DataColumnName("CALENDARGRAPHVALUES_SEMNUM")]
		[AllowDBNull]
		public int? CALENDARGRAPHVALUES_SEMNUM
		{
			get
			{
				return GetData<int?>("CALENDARGRAPHVALUES_SEMNUM");
			}
			set
			{
				SetData<int?>("CALENDARGRAPHVALUES_SEMNUM", value);
			}
		}

		[DataColumnName("CALENDARGRAPHVALUES_DAY")]
		[AllowDBNull]
		public int? CALENDARGRAPHVALUES_DAY
		{
			get
			{
				return GetData<int?>("CALENDARGRAPHVALUES_DAY");
			}
			set
			{
				SetData<int?>("CALENDARGRAPHVALUES_DAY", value);
			}
		}


		[DataColumnName("TYPEACTIVITY_ID")]
		[AllowDBNull]
		public Int64? TYPEACTIVITY_ID
		{
			get
			{
				return GetData<Int64?>("TYPEACTIVITY_ID");
			}
			set
			{
				SetData<Int64?>("TYPEACTIVITY_ID", value);
			}
		}
		
    }
}
