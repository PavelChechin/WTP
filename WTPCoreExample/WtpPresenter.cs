using RefDataStores.General;
using RefDataStores.PRACTICE;
using RefLib;
using RefLib.WTP;
using SqlDataSolution;
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
            Plan = wtps[0];
            return Plan;
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

        public IWTPROWGROUP CreateNewRowGroup()
        {
            IWTPROWGROUP newDataRow = DbManager.CreateRow<IWTPROWGROUP>();
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

        public IWTPPRACTICE CreateNewPractice()
        {
            IWTPPRACTICE newDataRow = DbManager.CreateRow<IWTPPRACTICE>();
            return newDataRow;
        }

        public ICALENDARGRAPHVALUES CreateNewGraphValue()
        {
            ICALENDARGRAPHVALUES newDataRow = DbManager.CreateRow<ICALENDARGRAPHVALUES>();
            return newDataRow;
        }

        public WTPRowGroup AddRowGroup(IWTPROWGROUP rowGroup, WTPComponent ParentComponent)
        {
            Plan.RowGroups.FillRow(rowGroup);
            WTPRowGroup newRowGroup = Plan.RowGroups.Add(rowGroup, false);
            newRowGroup.Component = ParentComponent;
            return newRowGroup;
        }

        public WTPRow AddRow(IWTPROW row, WTPComponent ParentComponent)
        {
            Plan.Rows.FillRow(row);
            WTPRow newRow = Plan.Rows.Add(row, false);
            newRow.Component = ParentComponent;
            return newRow;
        }

        public WTPRowValue AddRowValue(IWTPROWVALUES valueRow, WTPRow row)
        {
            row.Values.FillRow(valueRow);
            return row.Values.Add(valueRow, false);
        }

        public WTPPractice AddPractice(IWTPPRACTICE practiceRow, WTPRow row)
        {
            row.Practices.FillRow(practiceRow);
            return row.Practices.Add(practiceRow, false);
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


        //public void AddGraphValue(CalendarGraphValue GraphValue)
        //{
        //    ICALENDARGRAPHVALUES newValue = DbManager.CreateRow<>
        //}
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
            var disciplines = SqlData.GetDataSource<STUDDISCIPLINE>(ServerHelper.ConnectionHelper.GetConnection(), "SPU_STUDDISCIPLINE_SEL", null);
            var discipline = disciplines.Rows.
                                Cast<ISTUDDISCIPLINE>().
                                Where(r => r.STUDDISCIPLINE_DIPLOMANAME.Trim() == Name).
                                Select(r => r.STUDDISCIPLINE_ID);
            Int64 id = 0;
            if (discipline.Count() == 0)
            {
                var newDiscip = disciplines.CreateNewRow<STUDDISCIPLINE>();
                newDiscip.STUDDISCIPLINE_NAME = Name;
                newDiscip.STUDDISCIPLINE_SHORTNAME = Name;
                newDiscip.STUDDISCIPLINE_STATE = true;
                newDiscip.STUDDISCIPTYPE_ID = DiscipTypeId;
                newDiscip.STUDDISCIPLINE_DIPLOMANAME = Name;

                disciplines.AddRow(newDiscip);
                disciplines.SqlData.SaveAll();
                id = newDiscip.STUDDISCIPLINE_ID;
            }
            else
            {
                id = discipline.First();
            }
            return id;
        }


        public Int64 GetSpecialityByNumb(string NUMB)
        {
            var specialities = DBManager.GetDataSourse<ISPECIALITY>();
            var speciality = specialities.Rows.
                                Cast<ISPECIALITY>().
                                Where(r => r.SPECIALITY_NUMB == NUMB).
                                Select(r => r.SPECIALITY_ID);
            Int64 SpecialityID = 0;
            SpecialityID = (long)speciality.First();
            return SpecialityID;
        }

        //public Int64? GetStudDiscipCicleByName(string Name)
        //{
        //    var StudDiscipCicles = DbManager.GetDataSourse<RefDataStores.WTP.ISTUDDISCIPCICLE>();
        //    var StudDiscipCicle = StudDiscipCicles.Rows.
        //                            Cast<RefDataStores.WTP.ISTUDDISCIPCICLE>().
        //                            Where(r => r.STUDDISCIPCICLE_NAME == Name).
        //                            Select(r => r.STUDDISCIPCICLE_ID);
        //    Int64? id = 0;
        //    id = StudDiscipCicle.First();
        //    return id;
        //}


        public Int64? GetStudDiscipCicleByName(string Identifier, string Name)
        {
            var StudDiscipCicles = DBManager.GetDataSourse<RefDataStores.WTP.ISTUDDISCIPCICLE>();
            var StudDiscipCicle = StudDiscipCicles.Rows.
                                    Cast<RefDataStores.WTP.ISTUDDISCIPCICLE>().
                                    Where(r => r.STUDDISCIPCICLE_NAME == Name).
                                    Where(r => r.STUDDISCIPCICLE_CODE == Identifier).
                                    Select(r => r.STUDDISCIPCICLE_ID);
            Int64? id = 0;
            if (StudDiscipCicle.Count() == 0)
            {
                var newStudDiscipCicle = StudDiscipCicles.CreateRow<RefDataStores.WTP.ISTUDDISCIPCICLE>();
                newStudDiscipCicle.STUDDISCIPCICLE_CODE = Identifier;
                newStudDiscipCicle.STUDDISCIPCICLE_NAME = Name;

                StudDiscipCicles.Add<RefDataStores.WTP.ISTUDDISCIPCICLE>(newStudDiscipCicle);
                StudDiscipCicles.Save();
                id = newStudDiscipCicle.STUDDISCIPCICLE_ID;
            }
            else
            {
                id = StudDiscipCicle.First();
            }
            return id;
        }

        public Int64? GetChairByCode(string Code)
        {
            var ImportChairs = DBManager.GetDataSourse<IWTPIMPORTCHAIR>();
            var ImportChair = ImportChairs.Rows.
                            Cast<IWTPIMPORTCHAIR>().
                            Where(r => r.WTPIMPORTCHAIR_EXTCODE == Code).
                            Select(r => r.CHAIR_ID);
            Int64? id = 0;
            if (ImportChair.Count() == 0)
            {

            }
            id = ImportChair.First();
            return id;
        }

        public static Int64 GetStudYearIDByYear(string Year)
        {
            var StudYears = DBManager.GetDataSourse<ISTUDYEAR>();
            var StudYear = StudYears.Rows.
                            Cast<ISTUDYEAR>().
                            Where(r => r.STUDYEAR_NUM == Convert.ToInt32(Year)).
                            Select(r => r.STUDYEAR_ID);
            Int64 ID = 0;
            ID = StudYear.First();
            return ID;
        }

        public Int64? GetStudDiscComponentByName(string Name, string Code)
        {
            var StudDiscComponents = SqlData.GetDataSource<STUDDISCCOMPONENT>(ServerHelper.ConnectionHelper.GetConnection(), "SPU_STUDDISCCOMPONENT_SEL", null); ;
            var StudDiscComponent = StudDiscComponents.Rows.
                                    Cast<RefDataStores.WTP.ISTUDDISCCOMPONENT>().
                                    Where(r => r.STUDDISCCOMPONENT_NAME == Name).
                                    Where(r => r.STUDDISCCOMPONENT_CODE == Code).
                                    Select(r => r.STUDDISCCOMPONENT_ID);
            Int64? ID = 0;
            if (StudDiscComponent.Count() == 0)
            {
                var newStudDiscComponent = StudDiscComponents.CreateNewRow<STUDDISCCOMPONENT>();
                newStudDiscComponent.STUDDISCCOMPONENT_NAME = Name;
                newStudDiscComponent.STUDDISCCOMPONENT_CODE = Code;


                //StudDiscComponents.Add<RefDataStores.WTP.ISTUDDISCCOMPONENT>(newStudDiscComponent);
                //StudDiscComponents.Save();
                //ID = newStudDiscComponent.STUDDISCCOMPONENT_ID;

                StudDiscComponents.AddRow(newStudDiscComponent);
                StudDiscComponents.SqlData.SaveAll();
                ID = newStudDiscComponent.STUDDISCCOMPONENT_ID;
            }
            else
            {
                ID = StudDiscComponent.First();
            }
            return ID;
        }

        public Int64? GetSpecializationByName(string Name, string Code)
        {
            var Specializations = DBManager.GetDataSourse<ISPECIALIZATION>();
            var Specialization = Specializations.Rows.
                                    Cast<ISPECIALIZATION>().
                                    Where(r => r.SPECIALIZATION_NAME == Name).
                                    Select(r => r.SPECIALIZATION_ID);
            Int64? ID = 0;
            if (Specialization.Count() == 0)
            {
                var newSpecialization = Specializations.CreateRow<ISPECIALIZATION>();
                newSpecialization.SPECIALIZATION_NAME = Name;
                newSpecialization.SPECIALIZATION_NUMB = Code;

                Specializations.Add<ISPECIALIZATION>(newSpecialization);
                Specializations.Save();
                ID = newSpecialization.SPECIALIZATION_ID;
            }
            else
            {
                ID = Specialization.First();
            }

            return ID;
        }

        //public long GetTypeActivityIDByCode(int Code)
        //{
        //    var Activities = DBManager.GetDataSourse<ITYPEACTIVITY>();   //создать интерфейс в RefDataStores
        //    var Activity = Activities.Rows.
        //                    Cast<ITYPEACTIVITY>().
        //                    Where(r => r.TYPEACTIVITY_CODE == Code).
        //                    Select(r => r.TYPEACTIVITY_ID);
        //    long ID = 0;
        //    if (Activity.Count() == 0)      //возможно стоит оставить только поиск
        //    {
        //        var newActivity = Activities.CreateRow<ITYPEACTIVITY>();
        //        newActivity.TYPEACTIVITY_CODE = Code;

        //        Activities.Add<ITYPEACTIVITY>(newActivity);
        //        Activities.Save();
        //        ID = newActivity.TYPEACTIVITY_ID;
        //    }
        //    else
        //    {
        //        ID = Activity.First();
        //    }

        //    return ID;
        //}

        public long GetTypePracticeIDByName(string Name)
        {
            var Types = DBManager.GetDataSourse<ITYPEPRACTICE>();
            var Type = Types.Rows.
                        Cast<ITYPEPRACTICE>().
                        Where(r => r.TYPEPRACTICE_NAME == Name).
                        Select(r => r.TYPEPRACTICE_ID);
            long ID = 0;
            if (Type.Count() == 0)
            {
                var newType = Types.CreateRow<ITYPEPRACTICE>();
                newType.TYPEPRACTICE_NAME = Name;

                Types.Add<ITYPEPRACTICE>(newType);
                Types.Save();
                ID = newType.TYPEPRACTICE_ID;
            }
            else
            {
                ID = Type.First();
            }
            return ID;
        }

        //public Int64 GetLastVariationID()
        //{
        //    var Rows = DBManager.GetDataSourse<IWTPROW>();
        //var ID = Rows.Rows.Cast<IWTPROW>().Select(r => r.WTPROW_VARIATIONID).Last();
        //    return ID;
        //}

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
