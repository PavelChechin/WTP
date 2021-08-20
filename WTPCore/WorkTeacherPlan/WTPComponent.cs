using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CollectionsPattern;
using WTPCore.Data.Interfaces.Base;
using WTPCore.Data.SourceInrefaces;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPComponent : DeletableRow<WTPComponents, IWTPCOMPONENT, WTPComponent>, ICascadeRowContainer
    {
        internal WTPReadOnlyCollection<WTPRow> ChildRows
        {
            get;
            private set;
        }

        internal WTPReadOnlyCollection<WTPRowGroup> ChildRowGroups
        {
            get;
            private set;
        }
        internal WTPReadOnlyCollection<WTPComponent> ChildComponents
        {
            get;
            private set;
        }
        bool localValueChanged;
        private WTPComponent parent = null;
        public int Level
        {
            get;
            private set;
        }
        public WTPComponent Parent
        {
            get
            {
                return parent;
            }
            set
            {
                if (parent != value)
                {
                    if (parent != null)
                    {
                        parent.PropertyChanged -= Parent_PropertyChanged;
                        parent.ChildComponents.Remove(this);
                    }
                    parent = value;
                    Int64? newParentID = null;
                    if (parent != null)
                    {
                        parent.ChildComponents.Add(this);
                        newParentID = value.DataRow.WTPCOMPONENT_ID;
                        parent.PropertyChanged += Parent_PropertyChanged;
                    }
                    if (newParentID != DataRow.WTPCOMPONENT_PARENTID)
                    {
                        localValueChanged = true;
                        DataRow.WTPCOMPONENT_PARENTID = newParentID;
                        localValueChanged = false;
                    }
                    RefreshLevel();
                    OnPropertyChanged(new PropertyChangedEventArgs("Parent"));
                }
            }
        }

        public string Name
        {
            get
            {
                return GetCompoentText(DataRow);
            }
        }

        void Parent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WTPCOMPONENT_ID")
            {
                localValueChanged = true;
                DataRow.WTPCOMPONENT_PARENTID = Parent.DataRow.WTPCOMPONENT_ID;
                localValueChanged = false;
            }
            else if (e.PropertyName == "Level")
            {
                RefreshLevel();
            }
        }
        public override bool New
        {
            get { return !DataRow.WTPCOMPONENT_ID.HasValue; }
        }

        public WTPComponent(WTPComponents Collection, IWTPCOMPONENT Row)
            : base(Row, Collection)
        {
            ChildRows = new WTPReadOnlyCollection<WTPRow>();
            ChildComponents = new WTPReadOnlyCollection<WTPComponent>();
            Level = 1;
            
            Row.PropertyChanging += DataRow_PropertyChanging;
        }
      
        protected void RefreshLevel()
        {
            int tempLevel = 1;
            WTPComponent tempParent = Parent;
            while (tempParent != null)
            {
                tempParent = tempParent.Parent;
                tempLevel++;
            }
            if (tempLevel != Level)
            {
                Level = tempLevel;
                OnPropertyChanged(new PropertyChangedEventArgs("Level"));
            }
        }
        void DataRow_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "WTPCOMPONENT_PARENTID":
                    {
                        if (!localValueChanged)
                        {
                            localValueChanged = false;
                            throw new Exception("После того, как IWTPCOMPONENT установлена в WTPComponent, значение WTPCOMPONENT_PARENTID можно менять только через свойство WTPComponent.Parent");
                        }
                        break;
                    }
                
            }
        }
        public static string GetCompoentText(IWTPCOMPONENTSOURCE Row)
        {
            if (Row.STUDDISCIPCICLE_ID.HasValue)
                return Row.STUDDISCIPCICLE_NAME;
            else if (Row.STUDDISCCOMPONENT_ID.HasValue)
                return Row.STUDDISCCOMPONENT_NAME;
            else if (Row.SPECIALIZATION_ID.HasValue)
                return Row.SPECIALIZATION_NAME;
            else
                return "Компонента: Ошибка: Не установлена ни одна зависимость "; 
        }
        public static string GetTypeName(IWTPCOMPONENTSOURCE Row)
        {
            if (Row.STUDDISCIPCICLE_ID.HasValue)
                return "Цикл";
            else if (Row.STUDDISCCOMPONENT_ID.HasValue)
                return "Компонент";
            else if (Row.SPECIALIZATION_ID.HasValue)
                return "Специализация/";
            else
                return "Компонента: Ошибка: Не установлена ни одна зависимость "; 
        }
        public WTPComponent[] GetChildComponents()
        {
            return ChildComponents.ToArray();
        }
        public WTPComponent[] GetAllChildComponents()
        {
            return GetAllChildComponents(GetChildComponents()).ToArray();
        }        
        private static IEnumerable<WTPComponent> GetAllChildComponents(IEnumerable<WTPComponent> Components)
        {
            foreach (var c in Components)
            {
                foreach (var cc in GetAllChildComponents(c.GetChildComponents()))
                {
                    yield return cc;
                }
                yield return c;
            }

        }
        public WTPRowGroup[] GetChildRowGroups()
        {
            return ChildRowGroups.ToArray();
        }
        public WTPRowGroup[] GetAllChildRowGroups()
        {
            return GetAllChildComponents()
                .SelectMany(c => c.GetChildRowGroups())
                .Concat(GetChildRowGroups())
                .ToArray();
        }

        public WTPRow[] GetChildRows()
        {
            return ChildRows.ToArray();
        }
        public WTPRow[] GetAllChildRows()
        {          
            return GetAllChildComponents()
                .SelectMany(c => c.GetChildRows())
                .Concat(GetChildRows())
                .ToArray();
        }
        public override void Dispose()
        {
            if (Parent != null)
            {
                ////Используем локальную переменную, чтобы не затирать DataRow.WTPCOMPONENT_PARENTID
                //Parent.PropertyChanged -= Parent_PropertyChanged;
                Parent = null;
            }
            ChildComponents.Dispose();
            ChildRows.Clear();
            ChildRows.Dispose();            
            base.Dispose();
        }

        #region ICascadeRowContainer Members

        ICascadeRow ICascadeRowContainer.CascadeRow
        {
            get { return DataRow; }
        }

        #endregion
    }
}
