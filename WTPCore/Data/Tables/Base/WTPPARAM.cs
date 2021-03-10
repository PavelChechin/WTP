using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.Data.Tables.Base
{
	public class WTPPARAM : WTPDATAROW, IWTPPARAM
	{
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

		[DataColumnName("WTPPARAM_NAME")]
		public String WTPPARAM_NAME
		{
			get
			{
				return GetData<String>("WTPPARAM_NAME");
			}
			set
			{
				SetData<String>("WTPPARAM_NAME", value);
			}
		}

		[DataColumnName("WTPPARAM_DESCRIPTION")]
		public String WTPPARAM_DESCRIPTION
		{
			get
			{
				return GetData<String>("WTPPARAM_DESCRIPTION");
			}
			set
			{
				SetData<String>("WTPPARAM_DESCRIPTION", value);
			}
		}

		[DataColumnName("WTPPARAM_TYPE")]
		public String WTPPARAM_TYPE
		{
			get
			{
				return GetData<String>("WTPPARAM_TYPE");
			}
			set
			{
				SetData<String>("WTPPARAM_TYPE", value);
			}
		}

		[DataColumnName("WTPPARAM_FORSEMESTER")]
		public Boolean WTPPARAM_FORSEMESTER
		{
			get
			{
				return GetData<Boolean>("WTPPARAM_FORSEMESTER");
			}
			set
			{
				SetData<Boolean>("WTPPARAM_FORSEMESTER", value);
			}
		}

		[DataColumnName("WTPPARAMINFO_ID")]
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
