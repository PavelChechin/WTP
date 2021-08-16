using ISEnvironmentSolution;
using ISEnvironmentSolution.References.Settings;
using RefDataStores.General;
using RefDataStores.WTP;
using RefLib;
using SqlDataSolution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WTPCoreExample;

namespace EducPlanSolution
{
    public partial class AddDisciplineForm : Form
    {
        public AddDisciplineForm()
        {
            InitializeComponent();

            ServerHelper.ConnectionHelper.SetConnection(new SqlConnection(@"Data Source=localhost; Initial Catalog=WTP; Integrated Security=True"));

            var disciplines = SqlData.GetDataSource<STUDDISCIPLINE>(ServerHelper.ConnectionHelper.GetConnection(), "SPU_STUDDISCIPLINE_SEL", null).Rows.
                                Cast<ISTUDDISCIPLINE>();
            disciplineChoice.Properties.DataSource = disciplines;
            disciplineChoice.Properties.DisplayMember = "STUDDISCIPLINE_NAME";
            disciplineChoice.Properties.ValueMember = "STUDDISCIPLINE_ID";

            ServerHelper.ConnectionHelper.SetConnection(new SqlConnection(@"Data Source=localhost; Initial Catalog=WTP; Integrated Security=True"));

            var components = DBManager.GetDataSourse<ISTUDDISCCOMPONENT>().Rows.
                                Cast<ISTUDDISCCOMPONENT>();
            componentChoice.Properties.DataSource = components;
            componentChoice.Properties.DisplayMember = "STUDDISCCOMPONENT_NAME";
            componentChoice.Properties.ValueMember = "STUDDISCCOMPONENT_ID";


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(disciplineChoice.EditValue.ToString());
            var row = ISEnvironment.References.Show<IWTP>(ShowSettings.DefaultLookup);
            if (row != null)
            {
                //WTP_ID = row.WTP_ID;
                MessageBox.Show(row.WTP_ID.ToString());
            }
        }
    }
}
