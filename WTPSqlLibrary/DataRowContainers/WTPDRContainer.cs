using System;
using SqlDataSolution;
using System.Data;

namespace WTPPresenter.WTPSqlData.DataRowContainers
{
    public class WTPDRContainer : DataRowContainer
    {

        public WTPDRContainer()
        {
            
        }

 
        public void Destroy(bool Delete)
        {
            if (Delete)
                this.Delete();
            else
                TableContainer.Remove(this);

            
        }
        
        public event EventHandler Removed;

        protected override void OnRemoved()
        {
            if (Removed != null)
            {
                Removed(this, EventArgs.Empty);
            }
        }
        public bool Deleted
        {
            get { return RowState == DataRowState.Deleted; }
        }
    }
}
