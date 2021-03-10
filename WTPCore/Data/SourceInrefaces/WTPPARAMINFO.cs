using System;

namespace WTPCore.Data.SourceInrefaces
{
    public interface IWTPPARAMINFOSOURCE
	{
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
