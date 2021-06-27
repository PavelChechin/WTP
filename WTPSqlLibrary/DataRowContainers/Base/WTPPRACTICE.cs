using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;
using WTPCore.Data.SourceInrefaces;

namespace WTPPresenter.WTPSqlData.DataRowContainers.Base
{
	[DataTableName("WTPPRACTICE")]
	[SaveIndex(4)]
	public class WTPPRACTICE : WTPDRContainer, IWTPPRACTICE
	{
		[DataColumnName("WTPPRACTICE_ID")]
		[AllowDBNull]
		public Int64? WTPPRACTICE_ID
		{
			get
			{
				return GetData<Int64?>("WTPPRACTICE_ID");
			}
			set
			{
				SetData<Int64?>("WTPPRACTICE_ID", value);
			}
		}

		[DataColumnName("WTPROW_ID")]
		[AllowDBNull]
		public Int64? WTPROW_ID
		{
			get
			{
				return GetData<Int64?>("WTPROW_ID");
			}
			set
			{
				SetData<Int64?>("WTPROW_ID", value);
			}
		}

		[DataColumnName("TYPEPRACTICE_ID")]
		[AllowDBNull]
		public Int64? TYPEPRACTICE_ID
		{
			get
			{
				return GetData<Int64?>("TYPEPRACTICE_ID");
			}
			set
			{
				SetData<Int64?>("TYPEPRACTICE_ID", value);
			}
		}

		[DataColumnName("WTPPRACTICE_WEEKSCOUNT")]
		[AllowDBNull]
		public Int64? WTPPRACTICE_WEEKSCOUNT
		{
			get
			{
				return GetData<Int64?>("WTPPRACTICE_WEEKSCOUNT");
			}
			set
			{
				SetData<Int64?>("WTPPRACTICE_WEEKSCOUNT", value);
			}
		}

    }
}