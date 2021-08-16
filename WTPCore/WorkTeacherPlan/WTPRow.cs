using System;
using WTPCore.Data.Interfaces.Base;
using CollectionsPattern;
using System.ComponentModel;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPRow : DeletableRow<WTPRows, IWTPROW, WTPRow>, ICascadeRowContainer
    {
        bool localValueChanged = false;
        public Wtp Wtp
        {
            get
            {
                return Collection.Wtp;
            }
        }

        public WTPSemesters Semesters
        {
            get;
            private set;
        }
        public WTPRowValues Values
        {
            get;
            private set;
        }
        public WTPPractices Practices
        {
            get;
            private set;
        }
        public override bool New
        {
            get { return !DataRow.WTPROW_ID.HasValue; }
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
                    component.ChildRows.Remove(this);
                    component.PropertyChanged -= Component_PropertyChanged;
                }
                component = value;
                Int64? newComponentID = null;
                if (component != null)
                {
                    component.ChildRows.Add(this);
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
        private WTPVariation variation = null;
        public WTPVariation Variation
        {
            get
            {
                return variation;
            }
            set
            {
                if (variation != value)
                {
                    WTPVariation oldStream = variation;
                    variation = value;
                    if (oldStream != null)
                    {
                        if (DataRow != null)
                            DataRow.WTPROW_VARIATIONID = 0;
                        if (oldStream.DataRow != null)
                            oldStream.DataRow.PropertyChanged -= VariationDataRow_PropertyChanged;
                        oldStream.RemoveRow(this);
                    }
                    if (value != null)
                    {
                        value.DataRow.PropertyChanged += VariationDataRow_PropertyChanged;
                        value.AddRow(this);
                    }

                }
            }
        }

        void VariationDataRow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WTPVARIATION_ID")
            {
                DataRow.WTPROW_VARIATIONID = Variation.DataRow.WTPVARIATION_ID;
            }
        }
        public WTPRow(WTPRows Rows, IWTPROW WtpRow)
            : base(WtpRow, Rows)
        {
            Semesters = new WTPSemesters(this);
            Values = new WTPRowValues(this);
            Practices = new WTPPractices(this);

            Semesters.PropertyChanged += OnPropertyChanged;
            Values.PropertyChanged += OnPropertyChanged;
            WtpRow.PropertyChanging += DataRow_PropertyChanging;
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
                            throw new Exception("После того, как IWTPROW установлена в WTPRow, значение WTPCOMPONENT_ID можно менять только через свойство WTPRow.Component");
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
