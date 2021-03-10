using System;
using WTPCore.Data.Interfaces.Base;
using CollectionsPattern;
using System.Collections;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPParams : BaseCollection<WTPParam>
    {
        Hashtable columnNames = new Hashtable();
        Hashtable columnIDs = new Hashtable();

        public WTPParam Add(IWTPPARAM Row)
        {
            WTPParam column = new WTPParam(this, Row);
            if (!Contains(Row.WTPPARAM_NAME))
            {
                AddToList(column);
                columnNames.Add(Row.WTPPARAM_NAME, column);
                columnIDs.Add(Row.WTPPARAM_ID, column);
            }
            return column;
        }

        public WTPParam this[string ColumnName]
        {
            get
            {
                if (columnNames.ContainsKey(ColumnName))
                    return (WTPParam)columnNames[ColumnName];
                return null;
            }
        }


        public WTPParam GetByID(Int64 ParamID)
        {
            if (columnIDs.Contains(ParamID))
                return (WTPParam)columnIDs[ParamID];
            return null;
        }

        public bool Contains(string ColumnName)
        {
            return columnNames.ContainsKey(ColumnName);
        }

        public override void Dispose()
        {
            columnNames.Clear();
            columnIDs.Clear();
            base.Dispose();
        }
    }
}
