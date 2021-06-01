using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTPCoreExample
{
    static class ImportConverter
    {
        public static Int64 GetModeEducId(string extmodeEduc)
        {
            return Convert.ToInt64(extmodeEduc);
        }

        public static Int64 GetFormEducId(string extFormeEduc)
        {
            if (extFormeEduc == "false")
                return 1;            
            else
                return 6;

        }
    }
}
