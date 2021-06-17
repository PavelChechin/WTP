using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTPCore.Data.SourceInrefaces
{
    public interface ICALENDARGRAPHVALUESSOURCE
    {
        Int64? CALENDARGRAPHVALUES_ID { get; set; }

        Int64? WTP_ID { get; set; }

        int? CALENDARGRAPHVALUES_SEMNUM { get; set; }

        int? CALENDARGRAPHVALUES_DAY { get; set; }

        Int64? TYPEACTIVITY_ID { get; set; }
    }
}
