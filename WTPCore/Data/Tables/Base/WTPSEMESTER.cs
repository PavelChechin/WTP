using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.Data.Tables.Base
{
	public class WTPSEMESTER : WTPDATAROW, IWTPSEMESTER
	{
		[DataColumnName("WTPSEMESTER_ID")]
		public Int64? WTPSEMESTER_ID
		{
			get
			{
				return GetData<Int64?>("WTPSEMESTER_ID");
			}
			set
			{
				SetData<Int64?>("WTPSEMESTER_ID", value);
			}
		}

		[DataColumnName("WTPSEMESTER_NUM")]
		public int WTPSEMESTER_NUM
		{
			get
			{
				return GetData<int>("WTPSEMESTER_NUM");
			}
			set
			{
                SetData<int>("WTPSEMESTER_NUM", value);
			}
		}

		[DataColumnName("WTPROW_ID")]
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


	}
}
