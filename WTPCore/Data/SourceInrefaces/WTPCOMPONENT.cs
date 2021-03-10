using System;

namespace WTPCore.Data.SourceInrefaces
{
    public interface IWTPCOMPONENTSOURCE
    {
        Int64? WTPCOMPONENT_ID
        {
            get;
            set;
        }
        string WTPCOMPONENT_NAME
        {
            get;
            set;
        }
        string WTPCOMPONENT_CODE
        {
            get;
            set;
        }
        Int64? WTPCOMPONENT_PARENTID
        {
            get;
            set;
        }

        Int64? WTP_ID
        {
            get;
            set;
        }

        Int32? WTPCOMPONENT_SORTINDEX
        {
            get;
            set;
        }

        Int64? STUDDISCCOMPONENT_ID
        {
            get;
            set;
        }


        String STUDDISCCOMPONENT_NAME
        {
            get;
            set;
        }


        Int64? STUDDISCIPCICLE_ID
        {
            get;
            set;
        }


        String STUDDISCIPCICLE_NAME
        {
            get;
            set;
        }


        Int64? SPECIALIZATION_ID
        {
            get;
            set;
        }

        String SPECIALIZATION_NAME
        {
            get;
            set;
        }


    }
}
