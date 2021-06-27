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
                tabPane1.Visible = true;
                List<WTPGridRow> list = new List<WTPGridRow>();
                //BindingList<WTPGridRow> grid = new BindingList<WTPGridRow>();
                foreach (WTPComponent parentComponent in plan.Components.Where(q => q.DataRow.WTPCOMPONENT_PARENTID == null))
                {
                    foreach (WTPComponent childComponent in plan.Components.Where(q => q.DataRow.WTPCOMPONENT_PARENTID == parentComponent.DataRow.WTPCOMPONENT_ID))
                    {
                        foreach (WTPRow wtprow in plan.Rows.Where(r => r.DataRow.WTPCOMPONENT_ID == childComponent.DataRow.WTPCOMPONENT_ID))
                        {
                            foreach (WTPSemester semestr in wtprow.Semesters)
                            {
                                if (semestr.DataRow.WTPSEMESTER_NUM != null)
                                {
                                    list.Add(new WTPGridRow(parentComponent.DataRow.WTPCOMPONENT_NAME, childComponent, wtprow, wtprow.Values, semestr.DataRow.WTPSEMESTER_NUM));
                                }
                            }
                        }
                    }
                }
                list = list.OrderBy(r => r.Number).ToList();
                var grid = new BindingList<WTPGridRow>(list);
                //semestrNumColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridControl1.DataSource = grid;
            }

            BindingList<CalendarGridRow> calendar = new BindingList<CalendarGridRow>();
            calendar.Add(new CalendarGridRow() {Cours = 1,WeekNumber = 1,Monday ="1", Tuesday = "1", Wednesday = "1", Thursday = "1", Friday = "1", Saturday = "1" });
            calendar.Add(new CalendarGridRow() { Cours = 1, WeekNumber = 2, Monday = "1", Tuesday = "1", Wednesday = "1", Thursday = "1", Friday = "1", Saturday = "1" });
            calendar.Add(new CalendarGridRow() { Cours = 1, WeekNumber = 3, Monday = "1", Tuesday = "1", Wednesday = "1", Thursday = "1", Friday = "1", Saturday = "1" });
            calendar.Add(new CalendarGridRow() { Cours = 1, WeekNumber = 4, Monday = "1", Tuesday = "1", Wednesday = "1", Thursday = "1", Friday = "1", Saturday = "1" });
            calendar.Add(new CalendarGridRow() { Cours = 1, WeekNumber = 5, Monday = "1", Tuesday = "1", Wednesday = "1", Thursday = "1", Friday = "1", Saturday = "1" });
            calendar.Add(new CalendarGridRow() { Cours = 1, WeekNumber = 6, Monday = "1", Tuesday = "1", Wednesday = "1", Thursday = "1", Friday = "1", Saturday = "1" });

            gridControl2.DataSource = calendar;
        }


        private void importButton_Click(object sender, EventArgs e)
        {
            WTPCoreExample.ImportForm importForm = new WTPCoreExample.ImportForm();
            importForm.ShowDialog();

            MessageBox.Show(importForm.WTPID().ToString());
            WTP_ID = importForm.WTPID();
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
                tabPane1.Visible = true;
                List<WTPGridRow> list = new List<WTPGridRow>();
                //BindingList<WTPGridRow> grid = new BindingList<WTPGridRow>();
                foreach (WTPComponent parentComponent in plan.Components.Where(q => q.DataRow.WTPCOMPONENT_PARENTID == null))
                {
                    foreach (WTPComponent childComponent in plan.Components.Where(q => q.DataRow.WTPCOMPONENT_PARENTID == parentComponent.DataRow.WTPCOMPONENT_ID))
                    {
                        foreach (WTPRow wtprow in plan.Rows.Where(r => r.DataRow.WTPCOMPONENT_ID == childComponent.DataRow.WTPCOMPONENT_ID))
                        {
                            foreach (WTPSemester semestr in wtprow.Semesters)
                            {
                                if (semestr.DataRow.WTPSEMESTER_NUM != null)
                                {
                                    list.Add(new WTPGridRow(parentComponent.DataRow.WTPCOMPONENT_NAME, childComponent, wtprow, wtprow.Values, semestr.DataRow.WTPSEMESTER_NUM));
                                }
                            }
                        }
                    }
                }
                list = list.OrderBy(r => r.Number).ToList();
                var grid = new BindingList<WTPGridRow>(list);
                //semestrNumColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridControl1.DataSource = grid;
            }
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
                MessageBox.Show(row.WTP_ID.ToString());
            }

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
                tabPane1.Visible = true;
                List<WTPGridRow> list = new List<WTPGridRow>();
                //BindingList<WTPGridRow> grid = new BindingList<WTPGridRow>();
                foreach (WTPComponent parentComponent in plan.Components.Where(q => q.DataRow.WTPCOMPONENT_PARENTID == null))
                {
                    foreach (WTPComponent childComponent in plan.Components.Where(q => q.DataRow.WTPCOMPONENT_PARENTID == parentComponent.DataRow.WTPCOMPONENT_ID))
                    {
                        foreach (WTPRow wtprow in plan.Rows.Where(r => r.DataRow.WTPCOMPONENT_ID == childComponent.DataRow.WTPCOMPONENT_ID))
                        {
                            foreach (WTPSemester semestr in wtprow.Semesters)
                            {
                                if (semestr.DataRow.WTPSEMESTER_NUM != null)
                                {
                                    list.Add(new WTPGridRow(parentComponent.DataRow.WTPCOMPONENT_NAME, childComponent, wtprow, wtprow.Values, semestr.DataRow.WTPSEMESTER_NUM));
                                }
                            }
                        }
                    }
                }
                list = list.OrderBy(r => r.Number).ToList();
                var grid = new BindingList<WTPGridRow>(list);
                //semestrNumColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridControl1.DataSource = grid;

                //WtpPresenter _presenter = new WtpPresenter();
                ////_presenter.Load(21355);
                //_presenter.Load((long)WTP_ID);
                //var plan = _presenter.Plan;
                //formEducLabel.Text = "Программа: " + plan.DataRow.FORMEDUC_NAME;
                //modeEducLabel.Text = "Форма обучения: " + plan.DataRow.MODEEDUC_NAME;
                //specialityNameLabel.Text = "Специальность: " + plan.DataRow.SPECIALITY_NAME;
                //specialityNumbLabel.Text = "Код направления: " + plan.DataRow.SPECIALITY_NUMB;
                //studYearLabel.Text = "Год обучения: " + plan.DataRow.STUDYEAR_NAME;
                //facultyNameLabel.Text = "Факультет :" + plan.DataRow.FACULTY_FULLNAME;
                //qualificationLabel.Text = "Квалификация: " + plan.DataRow.QUALIFICATION_NAME;
                //formEducLabel.Visible = true;
                //modeEducLabel.Visible = true;
                //specialityNameLabel.Visible = true;
                //specialityNumbLabel.Visible = true;
                //studYearLabel.Visible = true;
                //facultyNameLabel.Visible = true;
                //qualificationLabel.Visible = true;
                //addDiscipButton.Visible = true;
                //tabPane1.Visible = true;

                //BindingList<WTPGridRow> grid = new BindingList<WTPGridRow>();
                //foreach (WTPRow wtprow in plan.Rows)
                //{
                //    foreach (int? semestr in wtprow.Values.Select(r => r.DataRow.WTPROWVALUES_SEMNUM).Distinct())
                //    {
                //        if (semestr != null)
                //        {
                //            grid.Add(new WTPGridRow(wtprow, wtprow.Values, semestr));
                //        }
                //    }
                //}

                //gridControl1.DataSource = grid;
                //semestrNumColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            }
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
    }
}
