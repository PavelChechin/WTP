using System;
using System.ComponentModel;
using System.Data;
using CollectionsPattern;

namespace WTPCore.Data
{
    public class RemovedEventArgs:EventArgs
    {
        public object Row
        {
            get;
            private set;
        }
    }
    public delegate void RemovedEventHandler(object sender, RemovedEventArgs e);
    public interface IWTPDATAROW : INotifyPropertyChanging, INotifyPropertyChanged, ICascadeRow
    {
        object[] RowValues { get; set; }      
        DataRowState RowState { get; }
        void SetDataRowState(DataRowState RowState);
    }
}
