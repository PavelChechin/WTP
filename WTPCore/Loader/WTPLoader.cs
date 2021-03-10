using System;
using System.Collections;
using System.Linq;
using WTPCore.Data.Interfaces.Base;
using WTPCore.WorkTeacherPlan;
using WTPCore.Data;
using WTPCore.Factory;
using WTPCore.Comparers;

namespace WTPCore.Loader
{
    public abstract class WTPLoader
    {
        public abstract WTPFactory Factory { get; protected set; }

        public class ObjectEventArgs : EventArgs
        {
            public object NewObject
            {
                get;
                set;
            }
            public object BaseRow
            {
                get;
                protected set;
            }
            public ObjectEventArgs(object BaseRow)
                : this(BaseRow, null)
            {

            }
            public ObjectEventArgs(object BaseRow, object NewObject)
            {
                this.NewObject = NewObject;              
            }
        }       
        public delegate void ObjectEventHandler (object Sender, ObjectEventArgs e);
        public event ObjectEventHandler NeedWTPObject = null;

        Hashtable hashRows = new Hashtable();
        public static Type[] StaticTypes
        {
            get;
            private set;
        }
        static WTPLoader()
        {
            StaticTypes = new Type[]
                    {
                        typeof(IWTPPARAM)
                    };
        }

        #region Абстрактный метод, возвращающий строки
        protected abstract T[] GetRows<T>() where T : IWTPDATAROW;
        protected T[] GetRowsInternal<T>() where T : IWTPDATAROW
        {
            string key = typeof(T).Name;
            T[] rows;
            if (hashRows.ContainsKey(key))
            {
                rows = (T[])hashRows[key];
            }
            else
            {
                rows = GetRows<T>();
                if (StaticTypes.Contains(typeof(T)))
                {
                    T[] newRows = new T[rows.Length];
                    
                    for (int i = 0; i < rows.Length; i++)
                    {
                        T newRow = WTPBaseFactory.Instance.CreateRow<T>();
                        newRow.RowValues = rows[i].RowValues;
                        newRows[i] = newRow;
                    }
                    rows = newRows;
                }
                hashRows.Add(key, rows);
            }
            return rows;
        }
       
        #endregion

       
        public Wtp[] CreateWTPs()
        {
        
            Hashtable hashtableWtps = InternalCreateWTPs();

            Hashtable hashtableComponents = CreateComponents(hashtableWtps);
            
            Hashtable hashtableRows = CreateRows(hashtableWtps, hashtableComponents);

            Hashtable hashtableSemesters = CreateSemesters(hashtableRows);

            Hashtable hashtableValues = CreateValues(hashtableRows);

            Check(hashtableWtps);
            hashRows.Clear();
            //Готовим возвращаемые данные (массив планов)
            Wtp[] wtps = new Wtp[hashtableWtps.Values.Count];
            hashtableWtps.Values.CopyTo(wtps, 0);

            return wtps;
        }
        
        private Hashtable CreateComponents(Hashtable WTPs)
        {
            var componentRows = GetRowsInternal<IWTPCOMPONENT>();
            Hashtable components = new Hashtable(componentRows.Length);

            foreach (var componentRow in componentRows)
            {
                components.Add(componentRow.WTPCOMPONENT_ID.Value, CreateComponent(WTPs, componentRow));
            }
            SetComponentParent(components);
            return components;
        }
        private void SetComponentParent(Hashtable Components)
        {
            foreach (DictionaryEntry de in Components)
            {
                WTPComponent component = (WTPComponent)de.Value;
                if (component.DataRow.WTPCOMPONENT_PARENTID.HasValue)
                {
                    component.Parent = (WTPComponent)Components[component.DataRow.WTPCOMPONENT_PARENTID.Value];
                }
            }
        }
        private WTPComponent CreateComponent(Hashtable WTPs, IWTPCOMPONENT WTPComponent)
        {
            Wtp plan = (Wtp)WTPs[WTPComponent.WTP_ID.Value];

            WTPComponent newComponent = plan.Components.Add(WTPComponent, true);

            return newComponent;
        }

        private Hashtable CreateValues(Hashtable Rows)
        {
            var valueRows = GetRowsInternal<IWTPROWVALUES>();
            Hashtable values = new Hashtable(valueRows.Length);

            foreach (var valueRow in valueRows)
            {
                values.Add(valueRow.WTPROWVALUES_ID.Value, CreateRowValue(Rows, valueRow));
            }

            return values;
        }

        private WTPRowValue CreateRowValue(Hashtable Rows, IWTPROWVALUES ValueRow)
        {
            WTPRow row = (WTPRow)Rows[ValueRow.WTPROW_ID.Value];

            WTPRowValue newRowValue = row.Values.Add(ValueRow, true);

            return newRowValue;
        }

        private Hashtable CreateSemesters(Hashtable Rows)
        {
            var semesterRows = GetRowsInternal<IWTPSEMESTER>();
            Hashtable semesters = new Hashtable(semesterRows.Length);

            foreach (var semesterRow in semesterRows)
            {
                semesters.Add(semesterRow.WTPSEMESTER_ID.Value, CreateSemester(Rows, semesterRow));
            }

            return semesters;
        }

        private object CreateSemester(Hashtable Rows, IWTPSEMESTER SemesterRow)
        {
            WTPRow row = (WTPRow)Rows[SemesterRow.WTPROW_ID.Value];

            WTPSemester newSemester = row.Semesters.Add(SemesterRow, true);

            return newSemester;
        }

        private Hashtable CreateRows(Hashtable WTPs, Hashtable Components)
        {
            var discRows = GetRowsInternal<IWTPROW>();
            Hashtable disciplines = new Hashtable(discRows.Length);

            foreach (var discRow in discRows)
            {
                disciplines.Add(discRow.WTPROW_ID.Value, CreateWTPRow(WTPs, Components, discRow));
            }

            return disciplines;
        }

        private WTPRow CreateWTPRow(Hashtable WTPs, Hashtable Components, IWTPROW WTPRow)
        {
            Wtp plan = (Wtp)WTPs[WTPRow.WTP_ID.Value];

            WTPRow newRow = plan.Rows.Add(WTPRow, true);
            newRow.Component = (WTPComponent)Components[WTPRow.WTPCOMPONENT_ID.Value];
            return newRow;
        }

        private void Check(Hashtable WTPs)
        {    
            foreach (DictionaryEntry de in WTPs)
            {
                Wtp wtp = (Wtp)de.Value;
                WTPRow[] sortedRows = wtp.Rows.OrderBy(r => r, WTPRowComparer.Instance).ToArray();
                for (int i = 0; i < sortedRows.Length; i++)
                {
                    if (sortedRows[i].DataRow.WTPROW_SORTINDEX != i)
                        sortedRows[i].DataRow.WTPROW_SORTINDEX = i;
                }
                WTPComponent[] sortedComponents = wtp.Components.OrderBy(c => c, WTPComponentComparer.Instance).ToArray();
                for (int i = 0; i < sortedComponents.Length; i++)
                {
                    if (sortedComponents[i].DataRow.WTPCOMPONENT_SORTINDEX != i)
                        sortedComponents[i].DataRow.WTPCOMPONENT_SORTINDEX = i;
                }
                wtp.Variations.Refresh(); 
            }
        }

        #region Создание самих планов
        private Hashtable InternalCreateWTPs()
        {
            var rows = GetRowsInternal<IWTP>();
            Hashtable hashtable = new Hashtable(rows.Length);
            foreach (var row in rows)
            {
                hashtable.Add(row.WTP_ID.Value, CreateWTP(row));
            }

            return hashtable;
        }

        private Wtp CreateWTP(IWTP WTPRow)
        {
            Wtp wtp = GetWTPObject(WTPRow);
            if (wtp.Factory == null)
                wtp.Factory = Factory;

            wtp.Init(WTPRow);

            //Создание учебных колонок
            //Колонки созадем до страниц, так как там они уже нужны
            var paramRows = GetRowsInternal<IWTPPARAM>();

            foreach (var row in paramRows)
            {
                wtp.Params.Add(row);
            }

            return wtp;
        }

        Wtp GetWTPObject(IWTP BaseRow)
        {
            if (NeedWTPObject != null)
            {
                var newObject = new ObjectEventArgs(BaseRow);
                NeedWTPObject(null, newObject);
                if (newObject.NewObject != null)
                    return (Wtp)newObject.NewObject;
                else
                    return new Wtp();
            }
            else
                return new Wtp();
        }
        #endregion

       
    }
}
