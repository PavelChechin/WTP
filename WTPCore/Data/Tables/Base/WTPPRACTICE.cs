using System;
using SqlDataSolution.Attributes;
using WTPCore.Data.Interfaces.Base;

namespace WTPCore.Data.Tables.Base
{
    public class WTPPRACTICE : WTPDATAROW, IWTPPRACTICE
    {
        [DataColumnName("WTPPRACTICE_ID")]
        public Int64? WTPPRACTICE_ID
        {
            get
            {
                return GetData<Int64?>("WTPPRACTICE_ID");
            }
            set
            {
                SetData<Int64?>("WTPPRACTICE_ID", value);
            }
        }

        [DataColumnName("WTPPRACTICE_WEEKSCOUNT")]
        public Int64? WTPPRACTICE_WEEKSCOUNT
        {
            get
            {
                return GetData<Int64?>("WTPPRACTICE_WEEKSCOUNT");
            }
            set
            {
                SetData<Int64?>("WTPPRACTICE_WEEKSCOUNT", value);
            }
        }

        [DataColumnName("TYPEPRACTICE_ID")]
        public Int64? TYPEPRACTICE_ID
        {
            get
            {
                return GetData<Int64?>("TYPEPRACTICE_ID");
            }
            set
            {
                SetData<Int64?>("TYPEPRACTICE_ID", value);
            }
        }

        [DataColumnName("WTPROW_ID")]
        public Int64? WTPROW_ID
        {
            get
            {
                return GetData<Int64?>("WTPROW_ID");
            }
            set
            {
                SetData<Int64?>("WTPROW_ID", value);
            }
        }
    }
}
