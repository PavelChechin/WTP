using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;
namespace WTPCore.Data.Tables.Base
{
    public class WTPCOMPONENT : WTPDATAROW, IWTPCOMPONENT
    {
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

        [DataColumnName("WTPCOMPONENT_PARENTID")]
        public Int64? WTPCOMPONENT_PARENTID
        {
            get
            {
                return GetData<Int64?>("WTPCOMPONENT_PARENTID");
            }
            set
            {
                SetData<Int64?>("WTPCOMPONENT_PARENTID", value);
            }
        }
        [DataColumnName("WTPCOMPONENT_NAME")]
        public string WTPCOMPONENT_NAME
        {
            get
            {
                return GetData<string>("WTPCOMPONENT_NAME");
            }
            set
            {
                SetData<string>("WTPCOMPONENT_NAME", value);
            }
        }
        [DataColumnName("WTPCOMPONENT_CODE")]
        public string WTPCOMPONENT_CODE
        {
            get
            {
                return GetData<string>("WTPCOMPONENT_CODE");
            }
            set
            {
                SetData<string>("WTPCOMPONENT_CODE", value);
            }
        }
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
        [DataColumnName("WTPCOMPONENT_SORTINDEX")]
        public Int32? WTPCOMPONENT_SORTINDEX
        {
            get
            {
                return GetData<Int32?>("WTPCOMPONENT_SORTINDEX");
            }
            set
            {
                SetData<Int32?>("WTPCOMPONENT_SORTINDEX", value);
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


    }
}
