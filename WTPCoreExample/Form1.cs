using RefDataStores.General;
using RefLib;
using RefLib.WTP;
using ServerHelper;
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
using System.Xml.Linq;

namespace WTPCoreExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //ServerHelper.ConnectionHelper.SetConnection(new SqlConnection(@"Data Source=192.168.203.56\testserver; Initial Catalog=Entrant;Persist Security Info=False; User ID=superadmin;Password=rdfrfajhtdth"));
            ServerHelper.ConnectionHelper.SetConnection(new SqlConnection(@"Data Source=localhost; Initial Catalog=WTP; Integrated Security=True"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;
            
            WtpPresenter planPresenter = new WtpPresenter();
            planPresenter.CreateWtp();
            var plan = planPresenter.Plan;
            plan.DataRow.SPECIALFACULTY_ID = 607;
            plan.DataRow.SPECIALITY_ID = 50;
            plan.DataRow.STUDYEAR_ID = 33;
            plan.DataRow.FORMEDUC_ID = 1;
            plan.DataRow.MODEEDUC_ID = 1;
            plan.DataRow.STUDYEAR_ID_VERSION = 33;
            planPresenter.Save();

            ImportPlanExample importer = new ImportPlanExample();
            XDocument xdoc = XDocument.Load(ofd.FileName);

            
            //planPresenter.Load(1297);
            if (importer.CheckImportFile(planPresenter.Plan, xdoc, out string ErrorMessage))
            {
                if (MessageBox.Show(ErrorMessage + " Продолжить?", "Импорт УП", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;
            }

            if (!importer.Import(planPresenter, xdoc, out string ErrorMessage2))
                MessageBox.Show(ErrorMessage);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var disciplines = SqlData.GetDataSource<STUDDISCIPLINE>(ServerHelper.ConnectionHelper.GetConnection(), "SPU_STUDDISCIPLINE_SEL", null);
            

            //var disciplines = DBManager.GetDataSourse<ISTUDDISCIPLINE>();
            var newDiscip = disciplines.CreateNewRow<STUDDISCIPLINE>();
            newDiscip.STUDDISCIPLINE_NAME = "kfghjcgjtjc";
            newDiscip.STUDDISCIPLINE_SHORTNAME = "yguykghgh";
            newDiscip.STUDDISCIPLINE_STATE = true;
            newDiscip.STUDDISCIPTYPE_ID = 1;
            newDiscip.STUDDISCIPLINE_DIPLOMANAME = "hjkgfgfjhf";
            

            disciplines.AddRow(newDiscip);
            disciplines.SqlData.SaveAll();
        }
    }
}
