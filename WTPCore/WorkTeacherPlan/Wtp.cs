using CollectionsPattern;
using WTPCore.Data.Interfaces.Base;
using System.ComponentModel;
using WTPCore.Factory;

namespace WTPCore.WorkTeacherPlan
{
    public class Wtp : BaseSimpleRow<IWTP>, INotifyPropertyChanged
    {
        public WTPFactory Factory
        {
            get;
            set;
        }
        public WTPVariations Variations
        {
            get;
            private set;
        }
        public WTPParams Params
        {
            get;
            private set;
        }

        public WTPRows Rows
        {
            get;
            private set;
        }

        public WTPComponents Components
        {
            get;
            private set;
        }

        public Wtp(IWTP DataRow)
            : base(null)//DataRow устанавливается в методе Init
        {
            if (DataRow != null)
                Init(DataRow);
        }

        public Wtp()
            : this(null)
        {

        }

        public void Init(IWTP DataRow)
        {
            SetDataRow(DataRow);
            Params = new WTPParams();
            Components = new WTPComponents(this);
            Rows = new WTPRows(this);
            Variations = new WTPVariations(this);
            Rows.PropertyChanged += OnPropertyChanged;
            Rows.PropertyChanged += Rows_PropertyChanged;
            Components.PropertyChanged += OnPropertyChanged;
            DataRow.PropertyChanged += DataRow_PropertyChanged;
        }

        private void DataRow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WTP_ID")
            {
                long? id = DataRow.WTP_ID;
                foreach (WTPRow item in Rows)
                {
                    item.DataRow.WTP_ID = id;
                }

                foreach (WTPComponent item in Components)
                {
                    item.DataRow.WTP_ID = id;
                }
            }
        }

        void Rows_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
            if (sender is WTPRowValue)
            {
                WTPRowValue wtpValue = (WTPRowValue)sender;
                if (wtpValue.IsDefaultValue())
                {
                    wtpValue.Delete();
                }
            }
        }
    }
}
