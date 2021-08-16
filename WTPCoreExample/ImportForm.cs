using RefDataStores.General;
using RefDataStores.WTP;
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
using WTPCore.WorkTeacherPlan;

namespace WTPCoreExample
{
    public partial class ImportForm : Form
    {
        Int64 WTP_ID;
        public ImportForm()
        {
            InitializeComponent();
            //ServerHelper.ConnectionHelper.SetConnection(new SqlConnection(@"Data Source=192.168.203.56\testserver; Initial Catalog=Entrant;Persist Security Info=False; User ID=superadmin;Password=rdfrfajhtdth"));
            ServerHelper.ConnectionHelper.SetConnection(new SqlConnection(@"Data Source=localhost; Initial Catalog=WTP; Integrated Security=True"));

            var years = DBManager.GetDataSourse<ISTUDYEAR>().Rows.
                                Cast<ISTUDYEAR>();
            lookUpEdit1.Properties.DataSource = years;
            lookUpEdit1.Properties.DisplayMember = "STUDYEAR_NAME";
            lookUpEdit1.Properties.ValueMember = "STUDYEAR_ID";

            var versionsYears = DBManager.GetDataSourse<ISTUDYEAR>().Rows.
                                Cast<ISTUDYEAR>();
            lookUpEdit2.Properties.DataSource = versionsYears;
            lookUpEdit2.Properties.DisplayMember = "STUDYEAR_NAME";
            lookUpEdit2.Properties.ValueMember = "STUDYEAR_ID";

            var formEduc = DBManager.GetDataSourse<IFORMEDUC>().Rows.
                                Cast<IFORMEDUC>();
            lookUpEdit3.Properties.DataSource = formEduc;
            lookUpEdit3.Properties.DisplayMember = "FORMEDUC_NAME";
            lookUpEdit3.Properties.ValueMember = "FORMEDUC_ID";

            var modeEduc = DBManager.GetDataSourse<IMODEEDUC>().Rows.
                                Cast<IMODEEDUC>();
            lookUpEdit4.Properties.DataSource = modeEduc;
            lookUpEdit4.Properties.DisplayMember = "MODEEDUC_NAME";
            lookUpEdit4.Properties.ValueMember = "MODEEDUC_ID";

            var specialities = DBManager.GetDataSourse<ISPECIALITY>().Rows.
                                Cast<ISPECIALITY>();
            lookUpEdit5.Properties.DataSource = specialities;
            lookUpEdit5.Properties.DisplayMember = "SPECIALITY_NAME";
            lookUpEdit5.Properties.ValueMember = "SPECIALITY_ID";

            var specializations = DBManager.GetDataSourse<ISPECIALIZATION>().Rows.
                                Cast<ISPECIALIZATION>();
            lookUpEdit6.Properties.DataSource = specializations;
            lookUpEdit6.Properties.DisplayMember = "SPECIALIZATION_NAME";
            lookUpEdit6.Properties.ValueMember = "SPECIALIZATION_ID";

            var faculties = DBManager.GetDataSourse<IFACULTY>().Rows.
                                Cast<IFACULTY>();
            lookUpEdit7.Properties.DataSource = faculties;
            lookUpEdit7.Properties.DisplayMember = "FACULTY_FULLNAME";
            lookUpEdit7.Properties.ValueMember = "FACULTY_ID";

            var placetrains = DBManager.GetDataSourse<IPLACETRAIN>().Rows.
                                Cast<IPLACETRAIN>();
            lookUpEdit8.Properties.DataSource = placetrains;
            lookUpEdit8.Properties.DisplayMember = "PLACETRAIN_NAME";
            lookUpEdit8.Properties.ValueMember = "PLACETRAIN_ID";
        }

        public Int64 WTPID()
        {
            return WTP_ID;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var SpecialFaculty = 1231;
            var Speciality = 50;
            var StudYear = 22;
            var FormEduc = 1;
            var ModeEduc = 1;
            var StudYearIDVersion = 22;


            //var WTP = DBManager.GetDataSourse<IWTP>().Rows.Cast<IWTP>().
            //     Where(r => r.SPECIALFACULTY_ID == SpecialFaculty).
            //     Where(r => r.SPECIALITY_ID == Speciality).
            //     Where(r => r.STUDYEAR_ID == StudYear).
            //     Where(r => r.FORMEDUC_ID == FormEduc).
            //     Where(r => r.MODEEDUC_ID == ModeEduc).
            //     Where(r => r.STUDYEAR_ID_VERSION == StudYearIDVersion);

            WtpPresenter planPresenter = new WtpPresenter();
            //if (WTP.Count() == 0)
            //{
                Wtp plan = planPresenter.CreateWtp();
            //var plan = planPresenter.Plan;
            plan.DataRow.SPECIALFACULTY_ID = 1231;
                plan.DataRow.SPECIALITY_ID = 50;
                plan.DataRow.STUDYEAR_ID = 22;
                plan.DataRow.FORMEDUC_ID = 1;
                plan.DataRow.MODEEDUC_ID = 1;
                plan.DataRow.STUDYEAR_ID_VERSION = 22;
                planPresenter.Save();
                WTP_ID = (long)plan.DataRow.WTP_ID;
            //}
            //else
            //{
            //    WTP_ID = Convert.ToInt64(WTP.Select(r => r.WTP_ID).First());
            //    planPresenter.Load(WTP_ID);
            //    var plan = planPresenter.Plan;
            //}



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
