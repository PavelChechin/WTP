﻿using RefDataStores.General;
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

            var SpecialFaculty = 607;
            var Speciality = 50;
            var StudYear = 33;
            var FormEduc = 1;
            var ModeEduc = 1;
            var StudYearIDVersion = 33;


            var WTP = DBManager.GetDataSourse<IWTP>().Rows.Cast<IWTP>().
                 Where(r => r.SPECIALFACULTY_ID == SpecialFaculty).
                 Where(r => r.SPECIALITY_ID == Speciality).
                 Where(r => r.STUDYEAR_ID == StudYear).
                 Where(r => r.FORMEDUC_ID == FormEduc).
                 Where(r => r.MODEEDUC_ID == ModeEduc).
                 Where(r => r.STUDYEAR_ID_VERSION == StudYearIDVersion);

            WtpPresenter planPresenter = new WtpPresenter();
            if (WTP.Count() == 0)
            {
                Wtp plan = planPresenter.CreateWtp();
                //var plan = planPresenter.Plan;
                plan.DataRow.SPECIALFACULTY_ID = 607;
                plan.DataRow.SPECIALITY_ID = 50;
                plan.DataRow.STUDYEAR_ID = 33;
                plan.DataRow.FORMEDUC_ID = 1;
                plan.DataRow.MODEEDUC_ID = 1;
                plan.DataRow.STUDYEAR_ID_VERSION = 33;
                planPresenter.Save();
                WTP_ID = (long)plan.DataRow.WTP_ID;
            }
            else
            {
                WTP_ID = Convert.ToInt64(WTP.Select(r => r.WTP_ID).First());
                planPresenter.Load(WTP_ID);
                var plan = planPresenter.Plan;
            }



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