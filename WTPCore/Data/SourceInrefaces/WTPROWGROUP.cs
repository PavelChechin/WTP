using System;


namespace WTPCore.Data.SourceInrefaces
{
    public interface IWTPROWGROUPSOURCE
    {
        Int64? WTP_ID
        {
            get;
            set;
        }

        Int64? WTPROWGROUP_ID
        {
            get;
            set;
        }

        string WTPROWGROUP_CODE
        {
            get;
            set;
        }

        string WTPROWGROUP_NAME
        {
            get;
            set;
        }

        int? WTPROWGROUP_SORTINDEX
        {
            get;
            set;
        }

        int? WTPROWGROUP_NUMBER
        {
            get;
            set;
        }

        Int64? WTPCOMPONENT_ID
        {
            get;
            set;
        }

        Int64? SPECIALIZATION_ID
        {
            get;
            set;
        }

        string SPECIALIZATION_NAME
        {
            get;
            set;
        }

        string SPECIALIZATION_NUMB
        {
            get;
            set;
        }

        string SPECIALIZATION_SHORTNAME
        {
            get;
            set;
        }
    }
}
