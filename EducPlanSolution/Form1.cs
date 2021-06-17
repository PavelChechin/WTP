using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using WTPCore.WorkTeacherPlan;
using WTPCoreExample;

namespace EducPlanSolution
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ServerHelper.ConnectionHelper.SetConnection(new SqlConnection(@"Data Source=localhost; Initial Catalog=WTP; Integrated Security=True"));
            WtpPresenter _presenter = new WtpPresenter();
            _presenter.Load(21355);
            var plan = _presenter.Plan;
            BindingList<WTPGridRow> grid = new BindingList<WTPGridRow>();
            foreach (WTPRow row in plan.Rows)
            {
                grid.Add(new WTPGridRow(row.DataRow.STUDDISCIPLINE_NAME, row.Values));
            }

            gridControl1.DataSource = grid;
        }

    }
}
