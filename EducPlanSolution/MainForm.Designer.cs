
namespace EducPlanSolution
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.discipNameColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.discipCodeColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.semestrNumColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lectionsColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.examsColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.practicsColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labWorksColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.midTermsColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.referatsColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.calcGraphWorksColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coursProjectsColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coursWorksColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.totalHoursColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ksrColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.independentWorkColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.discipNumberColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.addDiscipButton = new DevExpress.XtraEditors.SimpleButton();
            this.tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.formEducLabel = new System.Windows.Forms.Label();
            this.modeEducLabel = new System.Windows.Forms.Label();
            this.specialityNameLabel = new System.Windows.Forms.Label();
            this.specialityNumbLabel = new System.Windows.Forms.Label();
            this.importButton = new DevExpress.XtraEditors.SimpleButton();
            this.studYearLabel = new System.Windows.Forms.Label();
            this.facultyNameLabel = new System.Windows.Forms.Label();
            this.qualificationLabel = new System.Windows.Forms.Label();
            this.openPlanButton = new DevExpress.XtraEditors.SimpleButton();
            this.parentComponentNameColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.childComponentNameColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
            this.tabPane1.SuspendLayout();
            this.tabNavigationPage1.SuspendLayout();
            this.tabNavigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(0, 30);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1309, 391);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.parentComponentNameColumn,
            this.childComponentNameColumn,
            this.discipNameColumn,
            this.discipCodeColumn,
            this.semestrNumColumn,
            this.lectionsColumn,
            this.examsColumn,
            this.practicsColumn,
            this.labWorksColumn,
            this.midTermsColumn,
            this.referatsColumn,
            this.calcGraphWorksColumn,
            this.coursProjectsColumn,
            this.coursWorksColumn,
            this.totalHoursColumn,
            this.ksrColumn,
            this.independentWorkColumn,
            this.discipNumberColumn});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 2;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsMenu.ShowGroupSummaryEditorItem = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.parentComponentNameColumn, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.childComponentNameColumn, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // discipNameColumn
            // 
            this.discipNameColumn.Caption = "Дисциплина";
            this.discipNameColumn.FieldName = "Discip_Name";
            this.discipNameColumn.Name = "discipNameColumn";
            this.discipNameColumn.Visible = true;
            this.discipNameColumn.VisibleIndex = 0;
            this.discipNameColumn.Width = 309;
            // 
            // discipCodeColumn
            // 
            this.discipCodeColumn.Caption = "Код";
            this.discipCodeColumn.FieldName = "Code";
            this.discipCodeColumn.Name = "discipCodeColumn";
            this.discipCodeColumn.Visible = true;
            this.discipCodeColumn.VisibleIndex = 1;
            this.discipCodeColumn.Width = 76;
            // 
            // semestrNumColumn
            // 
            this.semestrNumColumn.Caption = "Семестр";
            this.semestrNumColumn.FieldName = "SemNum";
            this.semestrNumColumn.Name = "semestrNumColumn";
            this.semestrNumColumn.Visible = true;
            this.semestrNumColumn.VisibleIndex = 2;
            this.semestrNumColumn.Width = 76;
            // 
            // lectionsColumn
            // 
            this.lectionsColumn.Caption = "Лекции";
            this.lectionsColumn.FieldName = "Lections";
            this.lectionsColumn.Name = "lectionsColumn";
            this.lectionsColumn.Visible = true;
            this.lectionsColumn.VisibleIndex = 8;
            this.lectionsColumn.Width = 59;
            // 
            // examsColumn
            // 
            this.examsColumn.Caption = "Экзамен";
            this.examsColumn.FieldName = "Exams";
            this.examsColumn.Name = "examsColumn";
            this.examsColumn.Visible = true;
            this.examsColumn.VisibleIndex = 3;
            this.examsColumn.Width = 56;
            // 
            // practicsColumn
            // 
            this.practicsColumn.Caption = "Пр";
            this.practicsColumn.FieldName = "Practics";
            this.practicsColumn.Name = "practicsColumn";
            this.practicsColumn.Visible = true;
            this.practicsColumn.VisibleIndex = 11;
            this.practicsColumn.Width = 59;
            // 
            // labWorksColumn
            // 
            this.labWorksColumn.Caption = "ЛР";
            this.labWorksColumn.FieldName = "LabWorks";
            this.labWorksColumn.Name = "labWorksColumn";
            this.labWorksColumn.Visible = true;
            this.labWorksColumn.VisibleIndex = 12;
            this.labWorksColumn.Width = 54;
            // 
            // midTermsColumn
            // 
            this.midTermsColumn.Caption = "Зачёты";
            this.midTermsColumn.FieldName = "MidTerms";
            this.midTermsColumn.Name = "midTermsColumn";
            this.midTermsColumn.Visible = true;
            this.midTermsColumn.VisibleIndex = 4;
            this.midTermsColumn.Width = 60;
            // 
            // referatsColumn
            // 
            this.referatsColumn.Caption = "Реферат";
            this.referatsColumn.FieldName = "Referats";
            this.referatsColumn.Name = "referatsColumn";
            this.referatsColumn.Visible = true;
            this.referatsColumn.VisibleIndex = 9;
            this.referatsColumn.Width = 63;
            // 
            // calcGraphWorksColumn
            // 
            this.calcGraphWorksColumn.Caption = "РГР";
            this.calcGraphWorksColumn.FieldName = "CalcGraphWorks";
            this.calcGraphWorksColumn.Name = "calcGraphWorksColumn";
            this.calcGraphWorksColumn.Visible = true;
            this.calcGraphWorksColumn.VisibleIndex = 10;
            this.calcGraphWorksColumn.Width = 63;
            // 
            // coursProjectsColumn
            // 
            this.coursProjectsColumn.Caption = "КП";
            this.coursProjectsColumn.FieldName = "CoursProjects";
            this.coursProjectsColumn.Name = "coursProjectsColumn";
            this.coursProjectsColumn.Visible = true;
            this.coursProjectsColumn.VisibleIndex = 5;
            this.coursProjectsColumn.Width = 54;
            // 
            // coursWorksColumn
            // 
            this.coursWorksColumn.Caption = "КР";
            this.coursWorksColumn.FieldName = "CoursWorks";
            this.coursWorksColumn.Name = "coursWorksColumn";
            this.coursWorksColumn.Visible = true;
            this.coursWorksColumn.VisibleIndex = 6;
            this.coursWorksColumn.Width = 49;
            // 
            // totalHoursColumn
            // 
            this.totalHoursColumn.Caption = "Всего часов";
            this.totalHoursColumn.FieldName = "TotalHours";
            this.totalHoursColumn.Name = "totalHoursColumn";
            this.totalHoursColumn.Visible = true;
            this.totalHoursColumn.VisibleIndex = 7;
            this.totalHoursColumn.Width = 72;
            // 
            // ksrColumn
            // 
            this.ksrColumn.Caption = "КСР";
            this.ksrColumn.FieldName = "KSR";
            this.ksrColumn.Name = "ksrColumn";
            this.ksrColumn.Visible = true;
            this.ksrColumn.VisibleIndex = 13;
            this.ksrColumn.Width = 131;
            // 
            // independentWorkColumn
            // 
            this.independentWorkColumn.Caption = "СР";
            this.independentWorkColumn.FieldName = "IndependentWork";
            this.independentWorkColumn.Name = "independentWorkColumn";
            this.independentWorkColumn.Visible = true;
            this.independentWorkColumn.VisibleIndex = 14;
            this.independentWorkColumn.Width = 275;
            // 
            // discipNumberColumn
            // 
            this.discipNumberColumn.Caption = "Номер";
            this.discipNumberColumn.FieldName = "Number";
            this.discipNumberColumn.Name = "discipNumberColumn";
            // 
            // tabPane1
            // 
            this.tabPane1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPane1.Controls.Add(this.tabNavigationPage1);
            this.tabPane1.Controls.Add(this.tabNavigationPage2);
            this.tabPane1.Location = new System.Drawing.Point(0, 70);
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage1,
            this.tabNavigationPage2});
            this.tabPane1.RegularSize = new System.Drawing.Size(1327, 469);
            this.tabPane1.SelectedPage = this.tabNavigationPage1;
            this.tabPane1.Size = new System.Drawing.Size(1327, 469);
            this.tabPane1.TabIndex = 1;
            this.tabPane1.Text = "tabPane1";
            // 
            // tabNavigationPage1
            // 
            this.tabNavigationPage1.Caption = "План";
            this.tabNavigationPage1.Controls.Add(this.simpleButton2);
            this.tabNavigationPage1.Controls.Add(this.simpleButton1);
            this.tabNavigationPage1.Controls.Add(this.addDiscipButton);
            this.tabNavigationPage1.Controls.Add(this.gridControl1);
            this.tabNavigationPage1.Name = "tabNavigationPage1";
            this.tabNavigationPage1.Size = new System.Drawing.Size(1309, 424);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(254, 0);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(94, 23);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "Сместить вверх";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(162, 0);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(86, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Сместить вниз";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // addDiscipButton
            // 
            this.addDiscipButton.Location = new System.Drawing.Point(4, 1);
            this.addDiscipButton.Name = "addDiscipButton";
            this.addDiscipButton.Size = new System.Drawing.Size(126, 23);
            this.addDiscipButton.TabIndex = 1;
            this.addDiscipButton.Text = "Добавить дисцилину";
            this.addDiscipButton.Click += new System.EventHandler(this.addDiscipButton_Click);
            // 
            // tabNavigationPage2
            // 
            this.tabNavigationPage2.Caption = "График";
            this.tabNavigationPage2.Controls.Add(this.simpleButton5);
            this.tabNavigationPage2.Controls.Add(this.simpleButton4);
            this.tabNavigationPage2.Controls.Add(this.simpleButton3);
            this.tabNavigationPage2.Controls.Add(this.gridControl2);
            this.tabNavigationPage2.Name = "tabNavigationPage2";
            this.tabNavigationPage2.Size = new System.Drawing.Size(1309, 424);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(751, 20);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(26, 23);
            this.simpleButton5.TabIndex = 5;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(719, 20);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(26, 23);
            this.simpleButton4.TabIndex = 4;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(687, 20);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(26, 23);
            this.simpleButton3.TabIndex = 3;
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(3, 3);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(660, 249);
            this.gridControl2.TabIndex = 2;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Курс";
            this.gridColumn14.FieldName = "Cours";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 0;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Неделя";
            this.gridColumn15.FieldName = "WeekNumber";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 1;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Пн";
            this.gridColumn16.FieldName = "Monday";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 2;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Вт";
            this.gridColumn17.FieldName = "Tuesday";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 3;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Ср";
            this.gridColumn18.FieldName = "Wednesday";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 4;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Чт";
            this.gridColumn19.FieldName = "Thursday";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 5;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Пт";
            this.gridColumn20.FieldName = "Friday";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 6;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Сб";
            this.gridColumn21.FieldName = "Saturday";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 7;
            // 
            // formEducLabel
            // 
            this.formEducLabel.AutoSize = true;
            this.formEducLabel.Location = new System.Drawing.Point(219, 9);
            this.formEducLabel.Name = "formEducLabel";
            this.formEducLabel.Size = new System.Drawing.Size(78, 13);
            this.formEducLabel.TabIndex = 3;
            this.formEducLabel.Text = "formEducLabel";
            this.formEducLabel.Visible = false;
            // 
            // modeEducLabel
            // 
            this.modeEducLabel.AutoSize = true;
            this.modeEducLabel.Location = new System.Drawing.Point(219, 43);
            this.modeEducLabel.Name = "modeEducLabel";
            this.modeEducLabel.Size = new System.Drawing.Size(84, 13);
            this.modeEducLabel.TabIndex = 4;
            this.modeEducLabel.Text = "modeEducLabel";
            this.modeEducLabel.Visible = false;
            // 
            // specialityNameLabel
            // 
            this.specialityNameLabel.AutoSize = true;
            this.specialityNameLabel.Location = new System.Drawing.Point(647, 43);
            this.specialityNameLabel.Name = "specialityNameLabel";
            this.specialityNameLabel.Size = new System.Drawing.Size(104, 13);
            this.specialityNameLabel.TabIndex = 5;
            this.specialityNameLabel.Text = "specialityNameLabel";
            this.specialityNameLabel.Visible = false;
            // 
            // specialityNumbLabel
            // 
            this.specialityNumbLabel.AutoSize = true;
            this.specialityNumbLabel.Location = new System.Drawing.Point(399, 43);
            this.specialityNumbLabel.Name = "specialityNumbLabel";
            this.specialityNumbLabel.Size = new System.Drawing.Size(104, 13);
            this.specialityNumbLabel.TabIndex = 6;
            this.specialityNumbLabel.Text = "specialityNumbLabel";
            this.specialityNumbLabel.Visible = false;
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(9, 33);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(99, 23);
            this.importButton.TabIndex = 7;
            this.importButton.Text = "Импорт плана";
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // studYearLabel
            // 
            this.studYearLabel.AutoSize = true;
            this.studYearLabel.Location = new System.Drawing.Point(647, 9);
            this.studYearLabel.Name = "studYearLabel";
            this.studYearLabel.Size = new System.Drawing.Size(75, 13);
            this.studYearLabel.TabIndex = 8;
            this.studYearLabel.Text = "studYearLabel";
            this.studYearLabel.Visible = false;
            // 
            // facultyNameLabel
            // 
            this.facultyNameLabel.AutoSize = true;
            this.facultyNameLabel.Location = new System.Drawing.Point(399, 9);
            this.facultyNameLabel.Name = "facultyNameLabel";
            this.facultyNameLabel.Size = new System.Drawing.Size(92, 13);
            this.facultyNameLabel.TabIndex = 9;
            this.facultyNameLabel.Text = "facultyNameLabel";
            this.facultyNameLabel.Visible = false;
            // 
            // qualificationLabel
            // 
            this.qualificationLabel.AutoSize = true;
            this.qualificationLabel.Location = new System.Drawing.Point(863, 9);
            this.qualificationLabel.Name = "qualificationLabel";
            this.qualificationLabel.Size = new System.Drawing.Size(89, 13);
            this.qualificationLabel.TabIndex = 10;
            this.qualificationLabel.Text = "qualificationLabel";
            this.qualificationLabel.Visible = false;
            // 
            // openPlanButton
            // 
            this.openPlanButton.Location = new System.Drawing.Point(9, 4);
            this.openPlanButton.Name = "openPlanButton";
            this.openPlanButton.Size = new System.Drawing.Size(99, 23);
            this.openPlanButton.TabIndex = 11;
            this.openPlanButton.Text = "Открыть план";
            this.openPlanButton.Click += new System.EventHandler(this.openPlanButton_Click);
            // 
            // parentComponentNameColumn
            // 
            this.parentComponentNameColumn.Caption = " ";
            this.parentComponentNameColumn.FieldName = "ParentCycle";
            this.parentComponentNameColumn.Name = "parentComponentNameColumn";
            this.parentComponentNameColumn.Visible = true;
            this.parentComponentNameColumn.VisibleIndex = 0;
            this.parentComponentNameColumn.Width = 76;
            // 
            // childComponentNameColumn
            // 
            this.childComponentNameColumn.Caption = " ";
            this.childComponentNameColumn.FieldName = "ChildCycle";
            this.childComponentNameColumn.Name = "childComponentNameColumn";
            this.childComponentNameColumn.Visible = true;
            this.childComponentNameColumn.VisibleIndex = 0;
            this.childComponentNameColumn.Width = 76;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1327, 540);
            this.Controls.Add(this.openPlanButton);
            this.Controls.Add(this.qualificationLabel);
            this.Controls.Add(this.facultyNameLabel);
            this.Controls.Add(this.studYearLabel);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.specialityNumbLabel);
            this.Controls.Add(this.specialityNameLabel);
            this.Controls.Add(this.modeEducLabel);
            this.Controls.Add(this.formEducLabel);
            this.Controls.Add(this.tabPane1);
            this.Name = "MainForm";
            this.Text = "Подсистема \"Учебный план\"";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
            this.tabPane1.ResumeLayout(false);
            this.tabNavigationPage1.ResumeLayout(false);
            this.tabNavigationPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn discipNameColumn;
        private DevExpress.XtraGrid.Columns.GridColumn lectionsColumn;
        private DevExpress.XtraGrid.Columns.GridColumn examsColumn;
        private DevExpress.XtraGrid.Columns.GridColumn practicsColumn;
        private DevExpress.XtraGrid.Columns.GridColumn labWorksColumn;
        private DevExpress.XtraGrid.Columns.GridColumn midTermsColumn;
        private DevExpress.XtraGrid.Columns.GridColumn referatsColumn;
        private DevExpress.XtraGrid.Columns.GridColumn calcGraphWorksColumn;
        private DevExpress.XtraGrid.Columns.GridColumn coursProjectsColumn;
        private DevExpress.XtraGrid.Columns.GridColumn coursWorksColumn;
        private DevExpress.XtraGrid.Columns.GridColumn totalHoursColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ksrColumn;
        private DevExpress.XtraGrid.Columns.GridColumn independentWorkColumn;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage2;
        private System.Windows.Forms.Label formEducLabel;
        private System.Windows.Forms.Label modeEducLabel;
        private System.Windows.Forms.Label specialityNameLabel;
        private System.Windows.Forms.Label specialityNumbLabel;
        private DevExpress.XtraEditors.SimpleButton importButton;
        private System.Windows.Forms.Label studYearLabel;
        private System.Windows.Forms.Label qualificationLabel;
        private DevExpress.XtraEditors.SimpleButton openPlanButton;
        private DevExpress.XtraEditors.SimpleButton addDiscipButton;
        private System.Windows.Forms.Label facultyNameLabel;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraGrid.Columns.GridColumn semestrNumColumn;
        private DevExpress.XtraGrid.Columns.GridColumn discipCodeColumn;
        private DevExpress.XtraGrid.Columns.GridColumn discipNumberColumn;
        private DevExpress.XtraGrid.Columns.GridColumn parentComponentNameColumn;
        private DevExpress.XtraGrid.Columns.GridColumn childComponentNameColumn;
    }
}

