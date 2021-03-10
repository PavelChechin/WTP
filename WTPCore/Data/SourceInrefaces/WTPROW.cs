using System;

namespace WTPCore.Data.SourceInrefaces
{
    public interface IWTPROWSOURCE
	{
        Int64? WTP_ID
        {
            get;
            set;
        }
        Int64? WTPROW_ID
        {
            get;
            set;
        }

        Int32? WTPROW_SORTINDEX
        {
            get;
            set;
        }
        string WTPROW_INDEX
        {
            get;
            set;
        }
     
        Int64? WTPROW_LEVEL
        {
            get;
            set;
        }

        Boolean? WTPROW_DISCIPLINESPEC
        {
            get;
            set;
        }

        Boolean? WTPROW_VARIATCHAST
        {
            get;
            set;
        }
        Int64? WTPCOMPONENT_ID
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

        String STUDDISCCOMPONENT_CODE
        {
            get;
            set;
        }

        Int32? STUDDISCCOMPONENT_NUM
        {
            get;
            set;
        }

        Int64? CHAIR_ID
        {
            get;
            set;
        }

        String CHAIR_NAME
        {
            get;
            set;
        }

        String CHAIR_SHORTNAME
        {
            get;
            set;
        }
        String CHAIR_CODE
        {
            get;
            set;
        }
        Int64? STUDDISCIPLINE_ID
        {
            get;
            set;
        }

        String STUDDISCIPLINE_NAME
        {
            get;
            set;
        }

        String STUDDISCIPLINE_SHORTNAME
        {
            get;
            set;
        }

        Int64? STUDDISCIPTYPE_ID
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

        String STUDDISCIPCICLE_CODE
        {
            get;
            set;
        }

        Int32? STUDDISCIPCICLE_NUM
        {
            get;
            set;
        }

        Int64? SPECFACSPECIALIZATION_ID
        {
            get;
            set;
        }

        Int64? SPECIALIZATION_ID
        {
            get;
            set;
        }

        String SPECIALIZATION_NUMB
        {
            get;
            set;
        }

        String SPECIALIZATION_NAME
        {
            get;
            set;
        }

        String SPECIALIZATION_SHORTNAME
        {
            get;
            set;
        }

        Int64 WTPROW_VARIATIONID
        {
            get;
            set;
        }
	}
}
