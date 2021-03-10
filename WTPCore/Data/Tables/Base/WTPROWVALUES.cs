using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.Data.Tables.Base
{
	public class WTPROWVALUES : WTPDATAROW, IWTPROWVALUES
	{
		[DataColumnName("WTPROWVALUES_ID")]
		public Int64? WTPROWVALUES_ID
		{
			get
			{
				return GetData<Int64?>("WTPROWVALUES_ID");
			}
			set
			{
				SetData<Int64?>("WTPROWVALUES_ID", value);
			}
		}

		[DataColumnName("WTPROWVALUES_SEMNUM")]
		public Int16? WTPROWVALUES_SEMNUM
		{
			get
			{
				return GetData<Int16?>("WTPROWVALUES_SEMNUM");
			}
			set
			{
				SetData<Int16?>("WTPROWVALUES_SEMNUM", value);
			}
		}

		[DataColumnName("WTPPARAM_ID")]
		public Int64? WTPPARAM_ID
		{
			get
			{
				return GetData<Int64?>("WTPPARAM_ID");
			}
			set
			{
				SetData<Int64?>("WTPPARAM_ID", value);
			}
		}

		[DataColumnName("WTPROWVALUES_VALUE")]
        public object WTPROWVALUES_VALUE
		{
			get
			{
                return GetData<object>("WTPROWVALUES_VALUE");
			}
			set
			{
                SetData<object>("WTPROWVALUES_VALUE", value);
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
