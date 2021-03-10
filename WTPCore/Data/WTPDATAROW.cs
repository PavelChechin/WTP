using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using CustomDescriptor;
using SqlDataSolution;
using System.Reflection;
using System.Data;

namespace WTPCore.Data
{
    public class WTPDATAROW : RowContainer, IWTPDATAROW
    {
        private static Dictionary<string, List<string>> Columns = new Dictionary<string, List<string>>();

        public WTPDATAROW()
        {
            Type thisType = GetType();
            if (!Columns.ContainsKey(thisType.FullName))
            {


                PropertyInfo[] properties = thisType.GetProperties();
                properties = properties.Where(p => !p.IsDefined(
                    typeof(ServicePropertyAttribute), true)).ToArray();
                Columns.Add(thisType.FullName, properties
                                                .Select(p => p.Name)
                                                .Where(c => 
                                                    c != "RowValues" 
                                                    && c != "RowState"
                                                    && c != "Deleted")
                                                .OrderBy(c => c)
                                                .ToList());
            }

        }
        public DataRowState RowState
        {
            get;
            private set;
        }
        Hashtable data = new Hashtable();
       // Dictionary<string, object> data = new Dictionary<string, object>();
        [ServiceProperty]
        public override object[] RowValues
        {
            get
            {
                var columns = Columns[GetType().FullName];
                object[] array = new object[columns.Count];
                for (int i = 0; i < columns.Count; i++)
                {
                    array[i] = GetValue(columns[i]);
                }                
                return array;
            }
            set
            {
                var columns = Columns[GetType().FullName];          
                for (int i = 0; i < columns.Count; i++)
                {
                    SetValue(columns[i], value[i]);
                }  
            }
        }

        protected override object GetValue(string PropertyName)
        {
            if (data.ContainsKey(PropertyName))
                return data[PropertyName];
            else
                return null;
        }

        protected override void SetValue(string PropertyName, object Value)
        {
            if (!data.ContainsKey(PropertyName))
                data.Add(PropertyName, Value);
            else
                data[PropertyName] = Value;

            RowState = DataRowState.Modified;
        }
        public void AcceptChanges()
        {
            RowState = DataRowState.Unchanged;
        }
        public void Destroy(bool Delete)
        {
            if (Delete)
                RowState = DataRowState.Deleted;

            OnRemoved();
        }

        #region ITIPDATAROW Members


        public event EventHandler Removed;

        private void OnRemoved()
        {
            if (Removed != null)
            {
                Removed(this, EventArgs.Empty);
            }
        }
        public void SetDataRowState(DataRowState RowState)
        {
            this.RowState = RowState;
        }
        #endregion

        #region IDeletable Members

        public bool Deleted
        {
            get { return RowState == DataRowState.Deleted; }
        }

        #endregion

    
    }
}
