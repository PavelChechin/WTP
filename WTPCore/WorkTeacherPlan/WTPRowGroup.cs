using System;
using WTPCore.Data.Interfaces.Base;
using CollectionsPattern;
using System.ComponentModel;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPRowGroup : DeletableRow<WTPRowGroups, IWTPROWGROUP, WTPRowGroup>, ICascadeRowContainer
    {
        bool localValueChanged = false;
        public Wtp Wtp
        {
            get
            {
                return Collection.Wtp;
            }
        }

        public WTPRows Rows
        {
            get;
            private set;
        }

        public override bool New
        {
            get { return !DataRow.WTPROWGROUP_ID.HasValue; }
        }

        private WTPComponent component = null;
        public WTPComponent Component
        {
            get
            {
                return component;
            }
            set
            {
                if (component != null)
                {
                    component.ChildRowGroups.Remove(this);
                    component.PropertyChanged -= Component_PropertyChanged;
                }
                component = value;
                Int64? newComponentID = null;
                if (component != null)
                {
                    component.ChildRowGroups.Add(this);
                    newComponentID = value.DataRow.WTPCOMPONENT_ID;
                    component.PropertyChanged += Component_PropertyChanged;
                }
                if (newComponentID != DataRow.WTPCOMPONENT_ID)
                {
                    localValueChanged = true;
                    DataRow.WTPCOMPONENT_ID = newComponentID;
                    localValueChanged = false;
                }
            }
        }

        void Component_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WTPCOMPONENT_ID")
            {
                localValueChanged = true;
                DataRow.WTPCOMPONENT_ID = Component.DataRow.WTPCOMPONENT_ID;
                localValueChanged = false;
            }
        }

        public WTPRowGroup(WTPRowGroups RowGroups, IWTPROWGROUP WtpRowGroup)
            : base(WtpRowGroup, RowGroups)
        {
            Rows = new WTPRows(this);

            Rows.PropertyChanged += OnPropertyChanged;
            WtpRowGroup.PropertyChanging += DataRow_PropertyChanging;
        }

        void DataRow_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "WTPCOMPONENT_ID":
                    {
                        if (!localValueChanged)
                        {
                            localValueChanged = false;
                            throw new Exception("После того, как IWTPROWGROUP установлена в WTPRowGroup, значение WTPCOMPONENT_ID можно менять только через свойство WTPRowGroup.Component");
                        }
                        break;
                    }
            }
        }

        public override void Dispose()
        {
            if (Component != null)
            {
                ////Используем локальную переменную, чтобы не затирать DataRow.WTPCOMPONENT_PARENTID
                //Component.PropertyChanged -= Component_PropertyChanged;
                Component = null;
            }
            DataRow.PropertyChanging -= DataRow_PropertyChanging;
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
