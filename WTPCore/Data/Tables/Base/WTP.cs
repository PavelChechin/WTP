using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.Data.Tables.Base
{
    public class WTP : WTPDATAROW, IWTP
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

		[DataColumnName("WTP_CHANGEDATE")]
		public DateTime? WTP_CHANGEDATE
		{
			get
			{
				return GetData<DateTime?>("WTP_CHANGEDATE");
			}
			set
			{
				SetData<DateTime?>("WTP_CHANGEDATE", value);
			}
		}

		[DataColumnName("WTP_STATE")]
		public Boolean? WTP_STATE
		{
			get
			{
				return GetData<Boolean?>("WTP_STATE");
			}
			set
			{
				SetData<Boolean?>("WTP_STATE", value);
			}
		}

		[DataColumnName("WTPTEMPLATE_ID")]
		public Int64? WTPTEMPLATE_ID
		{
			get
			{
				return GetData<Int64?>("WTPTEMPLATE_ID");
			}
			set
			{
				SetData<Int64?>("WTPTEMPLATE_ID", value);
			}
		}

		[DataColumnName("STUDYEAR_ID_VERSION")]
		public Int64? STUDYEAR_ID_VERSION
		{
			get
			{
				return GetData<Int64?>("STUDYEAR_ID_VERSION");
			}
			set
			{
				SetData<Int64?>("STUDYEAR_ID_VERSION", value);
			}
		}
        [DataColumnName("STUDYEAR_NAME_VERSION")]
        public String STUDYEAR_NAME_VERSION
        {
            get
            {
                return GetData<String>("STUDYEAR_NAME_VERSION");
            }
            set
            {
                SetData<String>("STUDYEAR_NAME_VERSION", value);
            }
        }

        [DataColumnName("STUDYEAR_SHORTNAME_VERSION")]
        public String STUDYEAR_SHORTNAME_VERSION
        {
            get
            {
                return GetData<String>("STUDYEAR_SHORTNAME_VERSION");
            }
            set
            {
                SetData<String>("STUDYEAR_SHORTNAME_VERSION", value);
            }
        }

		[DataColumnName("SPECIALFACULTY_ID")]
		public Int64? SPECIALFACULTY_ID
		{
			get
			{
				return GetData<Int64?>("SPECIALFACULTY_ID");
			}
			set
			{
				SetData<Int64?>("SPECIALFACULTY_ID", value);
			}
		}

		[DataColumnName("SPECIALITY_ID")]
		public Int64? SPECIALITY_ID
		{
			get
			{
				return GetData<Int64?>("SPECIALITY_ID");
			}
			set
			{
				SetData<Int64?>("SPECIALITY_ID", value);
			}
		}
        [DataColumnName("SPECIALITY_NAME")]
        public String SPECIALITY_NAME
        {
            get
            {
                return GetData<String>("SPECIALITY_NAME");
            }
            set
            {
                SetData<String>("SPECIALITY_NAME", value);
            }
        }

        [DataColumnName("SPECIALITY_NUMB")]
        public String SPECIALITY_NUMB
        {
            get
            {
                return GetData<String>("SPECIALITY_NUMB");
            }
            set
            {
                SetData<String>("SPECIALITY_NUMB", value);
            }
        }

        [DataColumnName("QUALIFICATION_ID")]
        public Int64? QUALIFICATION_ID
        {
            get
            {
                return GetData<Int64?>("QUALIFICATION_ID");
            }
            set
            {
                SetData<Int64?>("QUALIFICATION_ID", value);
            }
        }

        [DataColumnName("QUALIFICATION_NAME")]
        public String QUALIFICATION_NAME
        {
            get
            {
                return GetData<String>("QUALIFICATION_NAME");
            }
            set
            {
                SetData<String>("QUALIFICATION_NAME", value);
            }
        }
		[DataColumnName("STUDYEAR_ID")]
		public Int64? STUDYEAR_ID
		{
			get
			{
				return GetData<Int64?>("STUDYEAR_ID");
			}
			set
			{
				SetData<Int64?>("STUDYEAR_ID", value);
			}
		}

		[DataColumnName("STUDYEAR_NAME")]
		public String STUDYEAR_NAME
		{
			get
			{
				return GetData<String>("STUDYEAR_NAME");
			}
			set
			{
				SetData<String>("STUDYEAR_NAME", value);
			}
		}

		[DataColumnName("STUDYEAR_SHORTNAME")]
		public String STUDYEAR_SHORTNAME
		{
			get
			{
				return GetData<String>("STUDYEAR_SHORTNAME");
			}
			set
			{
				SetData<String>("STUDYEAR_SHORTNAME", value);
			}
		}

		[DataColumnName("STUDYEAR_NUM")]
		public Int32 STUDYEAR_NUM
		{
			get
			{
				return GetData<Int32>("STUDYEAR_NUM");
			}
			set
			{
				SetData<Int32>("STUDYEAR_NUM", value);
			}
		}

		[DataColumnName("MODEEDUC_ID")]
		public Int64? MODEEDUC_ID
		{
			get
			{
				return GetData<Int64?>("MODEEDUC_ID");
			}
			set
			{
				SetData<Int64?>("MODEEDUC_ID", value);
			}
		}

		[DataColumnName("MODEEDUC_NAME")]
		public String MODEEDUC_NAME
		{
			get
			{
				return GetData<String>("MODEEDUC_NAME");
			}
			set
			{
				SetData<String>("MODEEDUC_NAME", value);
			}
		}

		[DataColumnName("FORMEDUC_ID")]
		public Int64? FORMEDUC_ID
		{
			get
			{
				return GetData<Int64?>("FORMEDUC_ID");
			}
			set
			{
				SetData<Int64?>("FORMEDUC_ID", value);
			}
		}

		[DataColumnName("FORMEDUC_NAME")]
		public String FORMEDUC_NAME
		{
			get
			{
				return GetData<String>("FORMEDUC_NAME");
			}
			set
			{
				SetData<String>("FORMEDUC_NAME", value);
			}
		}

		[DataColumnName("FACULTY_ID")]
		public Int64? FACULTY_ID
		{
			get
			{
				return GetData<Int64?>("FACULTY_ID");
			}
			set
			{
				SetData<Int64?>("FACULTY_ID", value);
			}
		}

		[DataColumnName("FACULTY_FULLNAME")]
		public String FACULTY_FULLNAME
		{
			get
			{
				return GetData<String>("FACULTY_FULLNAME");
			}
			set
			{
				SetData<String>("FACULTY_FULLNAME", value);
			}
		}

		[DataColumnName("FACULTY_SHORTNAME")]
		public String FACULTY_SHORTNAME
		{
			get
			{
				return GetData<String>("FACULTY_SHORTNAME");
			}
			set
			{
				SetData<String>("FACULTY_SHORTNAME", value);
			}
		}


	}
}
