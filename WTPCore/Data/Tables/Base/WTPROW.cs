using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.Data.Tables.Base
{
	public class WTPROW :  WTPDATAROW, IWTPROW
	{
        [DataColumnName("WTP_ID")]
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

        [DataColumnName("WTPROW_SORTINDEX")]
        public Int32? WTPROW_SORTINDEX
		{
			get
			{
                return GetData<Int32?>("WTPROW_SORTINDEX");
			}
			set
			{
                SetData<Int32?>("WTPROW_SORTINDEX", value);
			}
		}
        [DataColumnName("WTPROW_INDEX")]
        public string WTPROW_INDEX
        {
            get
            {
                return GetData<string>("WTPROW_INDEX");
            }
            set
            {
                SetData<string>("WTPROW_INDEX", value);
            }
        }
		
		[DataColumnName("WTPROW_LEVEL")]
		public Int64? WTPROW_LEVEL
		{
			get
			{
				return GetData<Int64?>("WTPROW_LEVEL");
			}
			set
			{
				SetData<Int64?>("WTPROW_LEVEL", value);
			}
		}

		[DataColumnName("WTPROW_DISCIPLINESPEC")]
		public Boolean? WTPROW_DISCIPLINESPEC
		{
			get
			{
				return GetData<Boolean?>("WTPROW_DISCIPLINESPEC");
			}
			set
			{
				SetData<Boolean?>("WTPROW_DISCIPLINESPEC", value);
			}
		}

		[DataColumnName("WTPROW_VARIATCHAST")]
		public Boolean? WTPROW_VARIATCHAST
		{
			get
			{
				return GetData<Boolean?>("WTPROW_VARIATCHAST");
			}
			set
			{
				SetData<Boolean?>("WTPROW_VARIATCHAST", value);
			}
		}
        [DataColumnName("WTPCOMPONENT_ID")]
        public Int64? WTPCOMPONENT_ID
        {
            get
            {
                return GetData<Int64?>("WTPCOMPONENT_ID");
            }
            set
            {
                SetData<Int64?>("WTPCOMPONENT_ID", value);
            }
        }
        [DataColumnName("STUDDISCCOMPONENT_ID")]
        public Int64? STUDDISCCOMPONENT_ID
		{
			get
			{
                return GetData<Int64?>("STUDDISCCOMPONENT_ID");
			}
			set
			{
                SetData<Int64?>("STUDDISCCOMPONENT_ID", value);
			}
		}

        [DataColumnName("STUDDISCCOMPONENT_NAME")]
        public String STUDDISCCOMPONENT_NAME
		{
			get
			{
                return GetData<String>("STUDDISCCOMPONENT_NAME");
			}
			set
			{
                SetData<String>("STUDDISCCOMPONENT_NAME", value);
			}
		}

        [DataColumnName("STUDDISCCOMPONENT_CODE")]
        public String STUDDISCCOMPONENT_CODE
		{
			get
			{
                return GetData<String>("STUDDISCCOMPONENT_CODE");
			}
			set
			{
                SetData<String>("STUDDISCCOMPONENT_CODE", value);
			}
		}

        [DataColumnName("STUDDISCCOMPONENT_NUM")]
        public Int32? STUDDISCCOMPONENT_NUM
		{
			get
			{
                return GetData<Int32?>("STUDDISCCOMPONENT_NUM");
			}
			set
			{
                SetData<Int32?>("STUDDISCCOMPONENT_NUM", value);
			}
		}

		[DataColumnName("CHAIR_ID")]
		public Int64? CHAIR_ID
		{
			get
			{
				return GetData<Int64?>("CHAIR_ID");
			}
			set
			{
				SetData<Int64?>("CHAIR_ID", value);
			}
		}

		[DataColumnName("CHAIR_NAME")]
		public String CHAIR_NAME
		{
			get
			{
				return GetData<String>("CHAIR_NAME");
			}
			set
			{
				SetData<String>("CHAIR_NAME", value);
			}
		}

		[DataColumnName("CHAIR_SHORTNAME")]
		public String CHAIR_SHORTNAME
		{
			get
			{
				return GetData<String>("CHAIR_SHORTNAME");
			}
			set
			{
				SetData<String>("CHAIR_SHORTNAME", value);
			}
		}
        [DataColumnName("CHAIR_CODE")]
        public String CHAIR_CODE
        {
            get
            {
                return GetData<String>("CHAIR_CODE");
            }
            set
            {
                SetData<String>("CHAIR_CODE", value);
            }
        }

		[DataColumnName("STUDDISCIPLINE_ID")]
		public Int64? STUDDISCIPLINE_ID
		{
			get
			{
				return GetData<Int64?>("STUDDISCIPLINE_ID");
			}
			set
			{
				SetData<Int64?>("STUDDISCIPLINE_ID", value);
			}
		}

		[DataColumnName("STUDDISCIPLINE_NAME")]
		public String STUDDISCIPLINE_NAME
		{
			get
			{
				return GetData<String>("STUDDISCIPLINE_NAME");
			}
			set
			{
				SetData<String>("STUDDISCIPLINE_NAME", value);
			}
		}

		[DataColumnName("STUDDISCIPLINE_SHORTNAME")]
		public String STUDDISCIPLINE_SHORTNAME
		{
			get
			{
				return GetData<String>("STUDDISCIPLINE_SHORTNAME");
			}
			set
			{
				SetData<String>("STUDDISCIPLINE_SHORTNAME", value);
			}
		}

		[DataColumnName("STUDDISCIPTYPE_ID")]
		public Int64? STUDDISCIPTYPE_ID
		{
			get
			{
				return GetData<Int64?>("STUDDISCIPTYPE_ID");
			}
			set
			{
				SetData<Int64?>("STUDDISCIPTYPE_ID", value);
			}
		}

		[DataColumnName("STUDDISCIPCICLE_ID")]
		public Int64? STUDDISCIPCICLE_ID
		{
			get
			{
				return GetData<Int64?>("STUDDISCIPCICLE_ID");
			}
			set
			{
				SetData<Int64?>("STUDDISCIPCICLE_ID", value);
			}
		}

		[DataColumnName("STUDDISCIPCICLE_NAME")]
		public String STUDDISCIPCICLE_NAME
		{
			get
			{
				return GetData<String>("STUDDISCIPCICLE_NAME");
			}
			set
			{
				SetData<String>("STUDDISCIPCICLE_NAME", value);
			}
		}

		[DataColumnName("STUDDISCIPCICLE_CODE")]
		public String STUDDISCIPCICLE_CODE
		{
			get
			{
				return GetData<String>("STUDDISCIPCICLE_CODE");
			}
			set
			{
				SetData<String>("STUDDISCIPCICLE_CODE", value);
			}
		}

		[DataColumnName("STUDDISCIPCICLE_NUM")]
		public Int32? STUDDISCIPCICLE_NUM
		{
			get
			{
				return GetData<Int32?>("STUDDISCIPCICLE_NUM");
			}
			set
			{
				SetData<Int32?>("STUDDISCIPCICLE_NUM", value);
			}
		}

		[DataColumnName("SPECFACSPECIALIZATION_ID")]
		public Int64? SPECFACSPECIALIZATION_ID
		{
			get
			{
				return GetData<Int64?>("SPECFACSPECIALIZATION_ID");
			}
			set
			{
				SetData<Int64?>("SPECFACSPECIALIZATION_ID", value);
			}
		}

		[DataColumnName("SPECIALIZATION_ID")]
		public Int64? SPECIALIZATION_ID
		{
			get
			{
				return GetData<Int64?>("SPECIALIZATION_ID");
			}
			set
			{
				SetData<Int64?>("SPECIALIZATION_ID", value);
			}
		}

		[DataColumnName("SPECIALIZATION_NUMB")]
		public String SPECIALIZATION_NUMB
		{
			get
			{
				return GetData<String>("SPECIALIZATION_NUMB");
			}
			set
			{
				SetData<String>("SPECIALIZATION_NUMB", value);
			}
		}

		[DataColumnName("SPECIALIZATION_NAME")]
		public String SPECIALIZATION_NAME
		{
			get
			{
				return GetData<String>("SPECIALIZATION_NAME");
			}
			set
			{
				SetData<String>("SPECIALIZATION_NAME", value);
			}
		}

		[DataColumnName("SPECIALIZATION_SHORTNAME")]
		public String SPECIALIZATION_SHORTNAME
		{
			get
			{
				return GetData<String>("SPECIALIZATION_SHORTNAME");
			}
			set
			{
				SetData<String>("SPECIALIZATION_SHORTNAME", value);
			}
		}

        [DataColumnName("WTPROW_VARIATIONID")]
        public long WTPROW_VARIATIONID
        {
            get
            {
                return GetData<long>("WTPROW_VARIATIONID");
            }
            set
            {
                SetData<long>("WTPROW_VARIATIONID", value);
            }
        }
    }
}
