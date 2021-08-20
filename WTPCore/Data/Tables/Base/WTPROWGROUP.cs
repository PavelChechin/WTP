using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.Data.Tables.Base
{
    public class WTPROWGROUP : WTPDATAROW, IWTPROWGROUP
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

        [DataColumnName("WTPROWGROUP_ID")]
        public Int64? WTPROWGROUP_ID
        {
            get
            {
                return GetData<Int64?>("WTPROWGROUP_ID");
            }
            set
            {
                SetData<Int64?>("WTPROWGROUP_ID", value);
            }
        }

        [DataColumnName("WTPROWGROUP_CODE")]
        public string WTPROWGROUP_CODE
        {
            get
            {
                return GetData<string>("WTPROWGROUP_CODE");
            }
            set
            {
                SetData<string>("WTPROWGROUP_CODE", value);
            }
        }

        [DataColumnName("WTPROWGROUP_NAME")]
        public string WTPROWGROUP_NAME
        {
            get
            {
                return GetData<string>("WTPROWGROUP_NAME");
            }
            set
            {
                SetData<string>("WTPROWGROUP_NAME", value);
            }
        }

        [DataColumnName("WTPROWGROUP_SORTINDEX")]
        public int? WTPROWGROUP_SORTINDEX
        {
            get
            {
                return GetData<int?>("WTPROWGROUP_SORTINDEX");
            }
            set
            {
                SetData<int?>("WTPROWGROUP_SORTINDEX", value);
            }
        }

        [DataColumnName("WTPROWGROUP_NUMBER")]
        public int? WTPROWGROUP_NUMBER
        {
            get
            {
                return GetData<int?>("WTPROWGROUP_NUMBER");
            }
            set
            {
                SetData<int?>("WTPROWGROUP_NUMBER", value);
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
    }
}
