using System;

namespace WTPCore.Data.SourceInrefaces
{
    public interface IWTPSEMESTERSOURCE
	{
        Int64? WTPSEMESTER_ID
        {
            get;
            set;
        }

        int WTPSEMESTER_NUM
        {
            get;
            set;
        }

        Int64? WTPROW_ID
        {
            get;
            set;
        }


	}
}
