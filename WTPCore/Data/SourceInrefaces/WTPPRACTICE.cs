using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTPCore.Data.SourceInrefaces
{
    public interface IWTPPRACTICESOURCE
    {
        Int64? WTPPRACTICE_ID { get; set; }

        Int64? WTPPRACTICE_WEEKSCOUNT { get; set; }

        long? WTPROW_ID { get; set; }

        long? TYPEPRACTICE_ID { get; set; }
    }
}
