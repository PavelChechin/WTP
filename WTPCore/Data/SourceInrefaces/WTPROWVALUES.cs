using System;

namespace WTPCore.Data.SourceInrefaces
{
    public interface IWTPROWVALUESSOURCE
	{
        Int64? WTPROWVALUES_ID
        {
            get;
            set;
        }

        Int16? WTPROWVALUES_SEMNUM
        {
            get;
            set;
        }

        Int64? WTPPARAM_ID
        {
            get;
            set;
        }

        object WTPROWVALUES_VALUE
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
