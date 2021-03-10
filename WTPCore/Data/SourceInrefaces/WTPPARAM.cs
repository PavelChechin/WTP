using System;

namespace WTPCore.Data.SourceInrefaces
{
    public interface IWTPPARAMSOURCE
	{
        Int64? WTPPARAM_ID
        {
            get;
            set;
        }

        String WTPPARAM_NAME
        {
            get;
            set;
        }

        String WTPPARAM_DESCRIPTION
        {
            get;
            set;
        }

        String WTPPARAM_TYPE
        {
            get;
            set;
        }

        Boolean WTPPARAM_FORSEMESTER
        {
            get;
            set;
        }

        Int64? WTPPARAMINFO_ID
        {
            get;
            set;
        }

        String WTPPARAMINFO_NAME
        {
            get;
            set;
        }

        String WTPPARAMINFO_DESCRIPTION
        {
            get;
            set;
        }


	}
}
