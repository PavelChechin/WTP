using RefDataStores.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WTPCore.Comparers;
using WTPCore.Data.Interfaces.Base;
using WTPCore.Data.SourceInrefaces;
using WTPCore.Factory;
using WTPCore.Loader;
using WTPCore.WorkTeacherPlan;
using WTPPresenter.WTPSqlData;

namespace WTPCoreExample
{
    public class WtpPresenter
    {
        public Wtp Plan
        {
            get;
            private set;
        }

        public DBManager DbManager
        {
            get;
            private set;
        }

        public WtpPresenter()
        {
            DbManager = new DBManager();
        }

        #region Load
        public Wtp Load(long id)
        {
            var loader = DbManager.Load(id);
            Wtp[] wtps = loader.CreateWTPs();

            return wtps[0];
        }

        void LoadPlan(WTPLoader loader)
        {   
            loader.CreateWTPs();
        }
        #endregion

        #region Create and Add Methods
        public Wtp CreateWtp()
        {
            if (Plan != null)
                throw new Exception("План уже создан");
            IWTP newPlan = DbManager.CreateRow<IWTP>();
            Wtp plan = new Wtp(newPlan);

            Plan = plan;

            var paramRows = DbManager.GetRowsInternal<IWTPPARAM>();

            foreach (var row in paramRows)
            {
                Plan.Params.Add(row);
            }

            return plan;
        }

        public IWTPROW CreateNewRow()
        {
            IWTPROW newDataRow = DbManager.CreateRow<IWTPROW>();
            return newDataRow;
        }

        public IWTPROWVALUES CreateNewRowValues()
        {
            IWTPROWVALUES newDataRow = DbManager.CreateRow<IWTPROWVALUES>();
            return newDataRow;
        }

        public IWTPSEMESTER CreateNewSemester()
        {
            IWTPSEMESTER newDataRow = DbManager.CreateRow<IWTPSEMESTER>();
            return newDataRow;
        }

        public WTPRow AddRow(IWTPROW row, WTPComponent ParentComponent)
        {
            Plan.Rows.FillRow(row);
            WTPRow newRow = Plan.Rows.Add(row, false);
            newRow.Component = ParentComponent;
            return newRow;
        }

        public  WTPRowValue AddRowValue(IWTPROWVALUES valueRow, WTPRow row)
        {
            row.Values.FillRow(valueRow);
            return row.Values.Add(valueRow, false);
        }

        public WTPSemester AddSemester(IWTPSEMESTER semester, WTPRow wtpRow)
        {
            wtpRow.Semesters.FillRow(semester);
            WTPSemester wtpSemester = wtpRow.Semesters.Add(semester, false);
            return wtpSemester;
        }

        public WTPParam AddParam(IWTPPARAM param, WTPRowValue rowValue)
        {
            return Plan.Params.Add(param);
        }

        public IWTPCOMPONENT CreateNewComponent()
        {
            IWTPCOMPONENT newRow = DbManager.CreateRow<IWTPCOMPONENT>();
            return newRow;
        }

        public WTPComponent AddComponent(IWTPCOMPONENT row, WTPComponent ParentComponent)
        {
            
            Plan.Components.FillRow(row);
            WTPComponent newComponent = Plan.Components.Add(row, false);
            newComponent.Parent = ParentComponent;
            return newComponent;
        }

        public void AddComponent(WTPComponent ParentComponent)
        {
            IWTPCOMPONENT newRow = DbManager.CreateRow<IWTPCOMPONENT>();

            AddComponent(newRow, ParentComponent);
        }
        #endregion

        #region Delete
        public void DeleteRows(WTPRow[] DeletedRows)
        {
            foreach (var row in DeletedRows)
            {
                row.Delete();
            }
        }
        #endregion

        #region Save
        public void Save()
        {
            DbManager.Save(Plan.DataRow);
            var components = Plan.Components
                .Where(c => c.DataRow.RowState != DataRowState.Unchanged)
                .OrderBy(r => r, WTPComponentComparer.Instance)
                .Select(c => c.DataRow);

            DbManager.Save(components.ToArray());
            DbManager.SaveAll();
        }
        #endregion
        
        /// <summary>
        /// Поиск в таблице дисциплин, если не найдено добавляем
        /// </summary>
        /// <param name="Name">Полное наименование</param>
        /// <param name="ShortName">Краткое наименование</param>
        /// <param name="DiscipTypeId">1 - учебные дисциплины, 2 - Практика, 3 - дипломная нагрузка</param>
        /// <returns></returns>
        public Int64 GetStudDisciplineByName(string Name, string ShortName, Int64 DiscipTypeId)
        {
            var disciplines = DbManager.GetDataSourse<ISTUDDISCIPLINE>();
            var discipline = disciplines.Rows.
                                Cast<ISTUDDISCIPLINE>().
                                Where(r => r.STUDDISCIPLINE_NAME == Name).
                                Select(r => r.STUDDISCIPLINE_ID);
            Int64 id = 0;
            if (discipline.Count() == 0)
            {
                var newDiscip = disciplines.CreateRow<ISTUDDISCIPLINE>();
                newDiscip.STUDDISCIPLINE_NAME = Name;
                newDiscip.STUDDISCIPLINE_SHORTNAME = ShortName; 
                newDiscip.STUDDISCIPLINE_STATE = true;
                newDiscip.STUDDISCIPTYPE_ID = DiscipTypeId; 
                newDiscip.STUDDISCIPLINE_DIPLOMANAME = Name;

                disciplines.Add<ISTUDDISCIPLINE>(newDiscip);
                disciplines.Save();
                id = newDiscip.STUDDISCIPLINE_ID;
            }
            else
            {
                id = discipline.First();
            }
            return id;
        }
        /// <summary>
        /// Поиск подразделений по номеру специальности
        /// </summary>
        /// <param name="NUMB">Номер специальности</param>
        /// <param name="PlaceTrainID">ID филиала</param>
        /// <param name="FacultyID">ID факультета</param>
        /// <returns></returns>
        public Int64? GetSpecialFacultyByNumb(string NUMB, Int64 PlaceTrainID, Int64 FacultyID)
        {
            var specialFaculties = DbManager.GetDataSourсe<ISPECIALFACULTY>();
            var specialFaculty = specialFaculties.Rows.
                                Cast<ISPECIALFACULTY>().
                                Where(r => r.SPECIALITY_NUMB == NUMB).
                                Where(r => r.PLACETRAIN_ID == PlaceTrainID).
                                Where(r => r.FACULTY_ID == FacultyID).
                                Select(r => r.SPECIALFACULTY_ID);
            Int64? ID = 0;
            if (specialFaculty.Count() == 0)
            {
                var newSpecialFaculty = specialFaculties.CreateRow<ISPECIALFACULTY>();
                newSpecialFaculty.SPECIALITY_ID = GetSpecialityByNumb(NUMB);
                newSpecialFaculty.PLACETRAIN_ID = PlaceTrainID;
                newSpecialFaculty.FACULTY_ID = FacultyID;

                specialFaculties.Add<ISPECIALFACULTY>(newSpecialFaculty);
                specialFaculties.Save();
                ID = newSpecialFaculty.SPECIALFACULTY_ID;
            }
            else
            {
                ID = specialFaculty.First();
            }
            return ID;
        }

        public Int64 GetSpecialityByNumb(string NUMB)
        {
            var specialities = DbManager.GetDataSourse<ISPECIALITY>();
            var speciality = specialities.Rows.
                                Cast<ISPECIALITY>().
                                Where(r => r.SPECIALITY_NUMB == NUMB).
                                Select(r => r.SPECIALITY_ID);
            Int64 SpecialityID = 0;
            SpecialityID = (long)speciality.First();
            return SpecialityID;
        }

        public Int64? GetStudDiscipCicleByName(string Name)
        {
            var StudDiscipCicles = DbManager.GetDataSourse<RefDataStores.WTP.ISTUDDISCIPCICLE>();
            var StudDiscipCicle = StudDiscipCicles.Rows.
                                    Cast<RefDataStores.WTP.ISTUDDISCIPCICLE>().
                                    Where(r => r.STUDDISCIPCICLE_NAME == Name).
                                    Select(r => r.STUDDISCIPCICLE_ID);
            Int64? id = 0;
            id = StudDiscipCicle.First();
            return id;
        }
         
        public Int64? GetChairByCode(string Code)
        {
            var ImportChairs = DbManager.GetDataSourse<IWTPIMPORTCHAIR>();
            var ImportChair = ImportChairs.Rows.
                            Cast<IWTPIMPORTCHAIR>().
                            Where(r => r.WTPIMPORTCHAIR_EXTCODE == Code).
                            Select(r => r.CHAIR_ID);
            Int64? id = 0;
            id = ImportChair.First();
            return id;
        }

        public Int64 GetStudYearIDByYear(string Year)
        {         
            var StudYears = DbManager.GetDataSourse<ISTUDYEAR>();
            var StudYear = StudYears.Rows.
                            Cast<ISTUDYEAR>().
                            Where(r => r.STUDYEAR_NUM == Convert.ToInt32(Year)).
                            Select(r => r.STUDYEAR_ID);
            Int64 ID = 0;
            ID = StudYear.First();
            return ID;
        }

        private WTPComponent GetParentComponent(object Row)
        {
            if (Row is WTPComponent)
                return ((WTPComponent)Row);
            if (Row is WTPRow)
                return ((WTPRow)Row).Component;
            if (Row is WTPSemester)
                return ((WTPSemester)Row).WtpRow.Component;
            return null;
        }
       
    }
}
