using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;

namespace WTPPresenter.WTPSqlData.DataRowContainers.Base
{
    [DataTableName("WTPCOMPONENT")]
    [SaveIndex(1)]
    public class WTPCOMPONENT : WTPDRContainer, IWTPCOMPONENT
    {
        [DataColumnName("WTPCOMPONENT_ID")]
        [AllowDBNull]
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
        [AllowDBNull]
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
        [AllowDBNull]
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
        [DataColumnName("STUDDISCCOMPONENT_NAME")]
        [AllowDBNull]
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
        [AllowDBNull]
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
        [AllowDBNull]
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
        [AllowDBNull]
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
        [AllowDBNull]
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
