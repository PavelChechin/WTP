using DevExpress.XtraGrid;
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
using WTPCore.WorkTeacherPlan;
using WTPCoreExample;

namespace EducPlanSolution
{
    public partial class MainForm : Form
    {
        long? WTP_ID;// = 1266; //1266;//1124;

        public MainForm()
        {
            InitializeComponent();
            ServerHelper.ConnectionHelper.SetConnection(new SqlConnection(@"Data Source=localhost; Initial Catalog=WTP; Integrated Security=True"));

            DisplayRows(WTP_ID);

            BindingList<CalendarGridRow> calendar = new BindingList<CalendarGridRow>();
            for (int j = 1; j <= 5; j++)
            {
                for (int i = 1; i <= 26; i++)
                {

                    if (i == 18) calendar.Add(new CalendarGridRow() { Cours = j, WeekNumber = i, Monday = " ", Tuesday = " ", Wednesday = " ", Thursday = " ", Friday = "*", Saturday = "*" });
                    else if (i == 19) calendar.Add(new CalendarGridRow() { Cours = j, WeekNumber = i, Monday = "*", Tuesday = "*", Wednesday = "*", Thursday = "*", Friday = "*", Saturday = " " });
                    else if (i == 20) calendar.Add(new CalendarGridRow() { Cours = j, WeekNumber = i, Monday = " ", Tuesday = " ", Wednesday = "Э", Thursday = "Э", Friday = "Э", Saturday = "Э" });
                    else if (i == 21) calendar.Add(new CalendarGridRow() { Cours = j, WeekNumber = i, Monday = "Э", Tuesday = "Э", Wednesday = "Э", Thursday = "Э", Friday = "Э", Saturday = "Э" });
                    else if (i == 22) calendar.Add(new CalendarGridRow() { Cours = j, WeekNumber = i, Monday = "Э", Tuesday = "Э", Wednesday = "Э", Thursday = "Э", Friday = "Э", Saturday = "Э" });
                    else if (i == 23) calendar.Add(new CalendarGridRow() { Cours = j, WeekNumber = i, Monday = "К", Tuesday = "К", Wednesday = "К", Thursday = "К", Friday = "К", Saturday = "К" });
                    else if (i == 42) calendar.Add(new CalendarGridRow() { Cours = j, WeekNumber = i, Monday = "Э", Tuesday = "Э", Wednesday = "Э", Thursday = "Э", Friday = "Э", Saturday = "Э" });
                    else if (i == 43) calendar.Add(new CalendarGridRow() { Cours = j, WeekNumber = i, Monday = "Э", Tuesday = "Э", Wednesday = "Э", Thursday = "Э", Friday = "Э", Saturday = "Э" });
                    else if (i == 44 || i == 45 || i == 46) calendar.Add(new CalendarGridRow() { Cours = j, WeekNumber = i, Monday = "У", Tuesday = "У", Wednesday = "У", Thursday = "У", Friday = "У", Saturday = "У" });
                    else if (i == 47 || i == 48 || i == 49 || i == 50 || i == 51 || i == 52) calendar.Add(new CalendarGridRow() { Cours = j, WeekNumber = i, Monday = "К", Tuesday = "К", Wednesday = "К", Thursday = "К", Friday = "К", Saturday = "К" });
                    else calendar.Add(new CalendarGridRow() { Cours = j, WeekNumber = i, Monday = " ", Tuesday = " ", Wednesday = " ", Thursday = " ", Friday = " ", Saturday = " " });
                }
            }

            gridControl2.DataSource = calendar;
        }


        private void importButton_Click(object sender, EventArgs e)
        {
            WTPCoreExample.ImportForm importForm = new WTPCoreExample.ImportForm();
            importForm.ShowDialog();

            //MessageBox.Show(importForm.WTPID().ToString());
            WTP_ID = importForm.WTPID();

            DisplayRows(WTP_ID);
        }

        private void addDiscipButton_Click(object sender, EventArgs e)
        {
            AddDisciplineForm addDiscipline = new AddDisciplineForm();
            addDiscipline.ShowDialog();
        }

        private void openPlanButton_Click(object sender, EventArgs e)
        {
            var row = ISEnvironment.References.Show<IWTP>(ShowSettings.DefaultLookup);
            if (row != null)
            {
                WTP_ID = row.WTP_ID;
                //MessageBox.Show(row.WTP_ID.ToString());
            }

            DisplayRows(WTP_ID);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var number = gridView1.FocusedRowHandle;
            var ds = gridControl1.DataSource as BindingList<WTPGridRow>;
            //MessageBox.Show(number.ToString());
            //MessageBox.Show(ds[number].Number.ToString());
            var discipToIncrease = ds.Where(q => q.Discip_Name == ds[number].Discip_Name).Select(q => q.Discip_Name).ToList().First();
            var num = ds.Where(q => q.Discip_Name == ds[number].Discip_Name).First().Number;
            var discipToDecrease = ds.Where(q => q.Number == num + 1).Select(q => q.Discip_Name).ToList().First();
            foreach (var row in ds.Where(q => q.Discip_Name == discipToIncrease))
            {
                row.Number += 1;
                row.Code = row.Code.Replace((char)number, (char)(row.Number));
            }
            foreach (var row in ds.Where(q => q.Discip_Name == discipToDecrease))
            {
                row.Number -= 1;
                row.Code = row.Code.Replace((char)number, (char)(row.Number));
            }
            List<WTPGridRow> sortedList = ds.OrderBy(i => i.Number).ToList();
            gridControl1.DataSource = new BindingList<WTPGridRow>(sortedList);
            gridControl1.RefreshDataSource();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var number = gridView1.FocusedRowHandle;
            var ds = gridControl1.DataSource as BindingList<WTPGridRow>;
            var discipToDecrease = ds.Where(q => q.Discip_Name == ds[number].Discip_Name).Select(q => q.Discip_Name).ToList().First();
            var num = ds.Where(q => q.Discip_Name == ds[number].Discip_Name).First().Number;
            var discipToIncrease = ds.Where(q => q.Number == num - 1).Select(q => q.Discip_Name).ToList().First();
            foreach (var row in ds.Where(q => q.Discip_Name == discipToDecrease))
            {
                row.Number -= 1;
                var changedCode = row.Code.Replace((char)(row.Number + 1), (char)(row.Number));
                row.Code = changedCode;
            }
            foreach (var row in ds.Where(q => q.Discip_Name == discipToIncrease))
            {
                row.Number += 1;
                var changedCode = row.Code.Replace((char)(row.Number - 1), (char)(row.Number));
                row.Code = changedCode;
                MessageBox.Show(row.Code);
            }
            List<WTPGridRow> sortedList = ds.OrderBy(i => i.Number).ToList();
            gridControl1.DataSource = new BindingList<WTPGridRow>(sortedList);
            gridControl1.RefreshDataSource();
        }

        private void DisplayRows(long? WTP_ID)
        {
            if (WTP_ID != null)
            {
                WtpPresenter _presenter = new WtpPresenter();
                //_presenter.Load(21355);
                _presenter.Load((long)WTP_ID);
                var plan = _presenter.Plan;
                formEducLabel.Text = "Программа: " + plan.DataRow.FORMEDUC_NAME;
                modeEducLabel.Text = "Форма обучения: " + plan.DataRow.MODEEDUC_NAME;
                specialityNameLabel.Text = "Специальность: " + plan.DataRow.SPECIALITY_NAME;
                specialityNumbLabel.Text = "Код направления: " + plan.DataRow.SPECIALITY_NUMB;
                studYearLabel.Text = "Год обучения: " + plan.DataRow.STUDYEAR_NAME;
                facultyNameLabel.Text = "Факультет :" + plan.DataRow.FACULTY_FULLNAME;
                qualificationLabel.Text = "Квалификация: " + plan.DataRow.QUALIFICATION_NAME;
                formEducLabel.Visible = true;
                modeEducLabel.Visible = true;
                specialityNameLabel.Visible = true;
                specialityNumbLabel.Visible = true;
                studYearLabel.Visible = true;
                facultyNameLabel.Visible = true;
                qualificationLabel.Visible = true;
                addDiscipButton.Visible = true;
                simpleButton1.Visible = true;
                simpleButton2.Visible = true;
                tabPane1.Visible = true;
                List<WTPGridRow> list = new List<WTPGridRow>();
                //BindingList<WTPGridRow> grid = new BindingList<WTPGridRow>();
                foreach (WTPComponent parentComponent in plan.Components.Where(q => q.DataRow.WTPCOMPONENT_PARENTID == null).Where(q => q.DataRow.STUDDISCIPCICLE_ID != null))
                {
                    if (parentComponent.GetAllChildComponents().Count() == 0)
                    {
                        foreach (WTPRow wtprow in plan.Rows.Where(r => r.DataRow.WTPCOMPONENT_ID == parentComponent.DataRow.WTPCOMPONENT_ID).OrderBy(r => r.DataRow.WTPROW_SORTINDEX))
                        {
                            foreach (WTPSemester semestr in wtprow.Semesters)
                            {
                                if (semestr.DataRow.WTPSEMESTER_NUM != null)
                                {
                                    var WTPROWValues = wtprow.Values.Where(r => r.DataRow.WTPROWVALUES_SEMNUM == semestr.DataRow.WTPSEMESTER_NUM).ToList();
                                    list.Add(new WTPGridRow(parentComponent, wtprow, WTPROWValues, semestr.DataRow.WTPSEMESTER_NUM));
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (WTPComponent childComponent in plan.Components.Where(q => q.DataRow.WTPCOMPONENT_PARENTID == parentComponent.DataRow.WTPCOMPONENT_ID))
                        {

                            foreach (WTPComponent module in plan.Components.Where(q => q.DataRow.WTPCOMPONENT_PARENTID == childComponent.DataRow.WTPCOMPONENT_ID))// plan.Components.Where(q => q.DataRow.WTPCOMPONENT_PARENTID == childComponent.DataRow.WTPCOMPONENT_ID))
                            {
                                foreach (WTPRow wtprow in plan.Rows.Where(r => r.DataRow.WTPCOMPONENT_ID == module.DataRow.WTPCOMPONENT_ID))
                                {
                                    foreach (WTPSemester semestr in wtprow.Semesters)
                                    {
                                        var WTPROWValues = wtprow.Values.Where(r => r.DataRow.WTPROWVALUES_SEMNUM == semestr.DataRow.WTPSEMESTER_NUM).ToList();
                                        list.Add(new WTPGridRow(parentComponent, childComponent, module, wtprow, WTPROWValues, semestr.DataRow.WTPSEMESTER_NUM));
                                    }
                                }
                            }

                            foreach (WTPRow wtprow in plan.Rows.Where(r => r.DataRow.WTPCOMPONENT_ID == childComponent.DataRow.WTPCOMPONENT_ID).OrderBy(r => r.DataRow.WTPROW_SORTINDEX))
                            {
                                foreach (WTPSemester semestr in wtprow.Semesters)
                                {
                                    if (semestr.DataRow.WTPSEMESTER_NUM != null)
                                    {
                                        var WTPROWValues = wtprow.Values.Where(r => r.DataRow.WTPROWVALUES_SEMNUM == semestr.DataRow.WTPSEMESTER_NUM).ToList();
                                        list.Add(new WTPGridRow(parentComponent, childComponent, wtprow, WTPROWValues, semestr.DataRow.WTPSEMESTER_NUM));
                                    }
                                }
                            }
                        }
                    }
                }
                var grid = new BindingList<WTPGridRow>(list.OrderBy(r => r.SortIndex).ToList());
                gridControl1.DataSource = null;
                gridControl1.DataSource = grid;
                gridControl1.Refresh();
            }
        }
    }
}
