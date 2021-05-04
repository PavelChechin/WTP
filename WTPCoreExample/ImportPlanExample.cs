using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using WTPCore.Comparers;
using WTPCore.Data.Interfaces.Base;
using WTPCore.WorkTeacherPlan;
using WTPPresenter.WTPSqlData;
using System.Xml.Linq;

namespace WTPCoreExample
{
    public class ImportPlanExample
    {
        const string lections = "LECTIONS";
        WtpPresenter presenter;

        public ImportPlanExample()
        {
                //Установить строку подключения
            ServerHelper.ConnectionHelper.SetConnection(new SqlConnection(@"Data Source=localhost; Initial Catalog=WTP; Integrated Security=True"));
            
        }
        public void Import()
        {
            XDocument xdoc = XDocument.Load("23.05.04_ЭЖД_(МТ;ГКР;ПКЖДТ;ТБиЛ)_2020.plx");
            presenter = new WtpPresenter();
            Wtp newPlan = presenter.CreateWtp();
            //сделать поиск аналогично дисциплине ниже (GetStudDisciplineByName),
            //реализовав метод в WtpPresenter
            //поиск сделать по коду специальности SPECIALITY_NUMB
            var StudyPlan = xdoc.Root.Element("{urn:schemas-microsoft-com:xml-diffgram-v1}diffgram").Element("{http://tempuri.org/dsMMISDB.xsd}dsMMISDB");
            var SpecialityCode = StudyPlan.Element("{http://tempuri.org/dsMMISDB.xsd}ООП").Attribute("Шифр").Value;

            var SpecialFacultyID = presenter.GetSpecialFacultyByNumb(/*"09.03.01"*/SpecialityCode, 1, 8);
            newPlan.DataRow.SPECIALFACULTY_ID = 607;//в каких подразделениях обучается на специальности; специальность табличка SPECIALITY

            var StudYearID = presenter.GetStudYearIDByYear(StudyPlan.Element("{http://tempuri.org/dsMMISDB.xsd}Планы").Attribute("ГодНачалаПодготовки").Value);
            newPlan.DataRow.STUDYEAR_ID = StudYearID;//год

            var ModeEducID = StudyPlan.Element("{http://tempuri.org/dsMMISDB.xsd}Планы").Attribute("КодФормыОбучения").Value;
            newPlan.DataRow.MODEEDUC_ID = Convert.ToInt64(ModeEducID); //1;//очное

            var FormEduc = StudyPlan.Element("{http://tempuri.org/dsMMISDB.xsd}Планы").Attribute("Сокращённое").Value;
            if (FormEduc == "false")
                newPlan.DataRow.FORMEDUC_ID = 1;            //полная-сокращенная форма
            if (FormEduc == "true")
                newPlan.DataRow.FORMEDUC_ID = 2;

            newPlan.DataRow.STUDYEAR_ID_VERSION = StudYearID; //год

            IEnumerable<XElement> parentCycles = StudyPlan.Descendants("{http://tempuri.org/dsMMISDB.xsd}ПланыЦиклы").Where(q => q.Attribute("ТипБлока").Value == "1");
            foreach (var parentCycle in parentCycles)
            {
                IWTPCOMPONENT component = presenter.CreateNewComponent();
                //поиск по имени STUDDISCIPCICLE_NAME
                var StudDiscipCicleID = presenter.GetStudDiscipCicleByName(parentCycle.Attribute("Идентификатор").Value, parentCycle.Attribute("Цикл").Value);  //поиск цикла возможно правильнее будет производить по идентификатору, а не по названию
                component.STUDDISCIPCICLE_ID = StudDiscipCicleID;
                var cycleID = parentCycle.Attribute("Код").Value;
                var wtpComponent = presenter.AddComponent(component, null);
                Save();

                var childCycles = StudyPlan.Descendants("{http://tempuri.org/dsMMISDB.xsd}ПланыЦиклы").Where(q => q.Attribute("ТипБлока").Value != "1").Where(q => q.Attribute("КодРодителя").Value == cycleID);
                if (childCycles.Count() != 0)
                {
                    foreach (var childCycle in childCycles)
                    {
                        IWTPCOMPONENT component2 = presenter.CreateNewComponent();
                        var childID = presenter.GetStudDiscipCicleByName(childCycle.Attribute("Идентификатор").Value, childCycle.Attribute("Цикл").Value);
                        component2.STUDDISCIPCICLE_ID = childID;
                        var wtpComponent2 = presenter.AddComponent(component2, wtpComponent);
                        Save();


                        IEnumerable<XElement> planRows = StudyPlan.Descendants("{http://tempuri.org/dsMMISDB.xsd}ПланыСтроки").Where(q => q.Attribute("УровеньВложения").Value == "1");
                        foreach (var planRow in planRows)
                        {
                            if (planRow.Attribute("ТипОбъекта").Value == "2")
                            {
                                var rowID = planRow.Attribute("Код").Value;
                                var discipRow = AddWTPROW(planRow, wtpComponent2);

                                var rowValues = StudyPlan.Descendants("{http://tempuri.org/dsMMISDB.xsd}ПланыНовыеЧасы").Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                AddWTPROWValues(newPlan, rowValues, discipRow);
                            }

                            if (planRow.Attribute("ТипОбъекта").Value == "1")  //дисциплины специализации
                            {
                                IWTPCOMPONENT component3 = presenter.CreateNewComponent();
                                var ID = presenter.GetStudDiscComponentByName(planRow.Attribute("Дисциплина").Value);
                                var componentID = planRow.Attribute("Код").Value;
                                var OOP = StudyPlan.Element("{http://tempuri.org/dsMMISDB.xsd}ООП").Descendants("{http://tempuri.org/dsMMISDB.xsd}ООП").Where(q => q.Attribute("Код").Value == planRow.Attribute("КодООП").Value).First();/*Where(q => q.Attribute("Код").Value == planRow.Attribute("КодООП").Value).First();*/
                                var SpecializationID = presenter.GetSpecializationByName(OOP.Attribute("Название").Value, OOP.Attribute("Шифр").Value);
                                component3.SPECIALIZATION_ID = SpecializationID;
                                var wtpComponent3 = presenter.AddComponent(component3, wtpComponent2);

                                var specRows = StudyPlan.Descendants("{http://tempuri.org/dsMMISDB.xsd}ПланыСтроки").Where(q => q.Attribute("КодРодителя").Value == componentID);
                                foreach (var specRow in specRows)
                                {
                                    var rowID = specRow.Attribute("Код").Value;
                                    var discipRow = AddWTPROW(specRow, wtpComponent3);

                                    var rowValues = StudyPlan.Descendants("{http://tempuri.org/dsMMISDB.xsd}ПланыНовыеЧасы").Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                    AddWTPROWValues(newPlan, rowValues, discipRow);
                                }

                                Save();
                            }

                            if (planRow.Attribute("ТипОбъекта").Value == "5")  //дисциплины по выбору
                            {
                                IWTPCOMPONENT component3 = presenter.CreateNewComponent();
                                var ID = presenter.GetStudDiscComponentByName(planRow.Attribute("Дисциплина").Value);
                                var componentID = planRow.Attribute("Код").Value;
                                XElement OOP = StudyPlan.Element("{http://tempuri.org/dsMMISDB.xsd}ООП");
                                if (OOP.Attribute("Код").Value != planRow.Attribute("КодООП").Value)
                                {
                                    OOP = StudyPlan.Element("{http://tempuri.org/dsMMISDB.xsd}ООП").Descendants("{http://tempuri.org/dsMMISDB.xsd}ООП").Where(q => q.Attribute("Код").Value == planRow.Attribute("КодООП").Value).First();
                                }

                                var SpecializationID = presenter.GetSpecializationByName(OOP.Attribute("Название").Value, OOP.Attribute("Шифр").Value);
                                component3.STUDDISCCOMPONENT_ID = ID;
                                component3.SPECIALIZATION_ID = SpecializationID;
                                var wtpComponent3 = presenter.AddComponent(component3, wtpComponent2);

                                var chosenRows = StudyPlan.Descendants("{http://tempuri.org/dsMMISDB.xsd}ПланыСтроки").Where(q => q.Attribute("КодРодителя").Value == componentID);
                                foreach (var chosenRow in chosenRows)
                                {
                                    var rowID = chosenRow.Attribute("Код").Value;
                                    var discipRow = AddWTPROW(chosenRow, wtpComponent3);

                                    var rowValues = StudyPlan.Descendants("{http://tempuri.org/dsMMISDB.xsd}ПланыНовыеЧасы").Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                    AddWTPROWValues(newPlan, rowValues, discipRow);

                                }

                                Save();
                            }

                            //if (planRow.Attribute("ТипОбъекта").Value == "3") { } //практики

                            Save();
                        }
                    }
                }
                Save();
            }
            Save();


            
        }

        public void Save()
        {
            presenter.Save();
        }

        public WTPRow AddWTPROW(XElement planRow, WTPComponent ParentComponent)
        {
            Int64 discipId = presenter.GetStudDisciplineByName(planRow.Attribute("Дисциплина").Value, "", 1);
            var newrow = presenter.CreateNewRow();
            //newrow.CHAIR_ID = presenter.GetChairByCode(planRow.Attribute("КодКафедры").Value);
            newrow.STUDDISCIPLINE_ID = discipId;
            var discipRow = presenter.AddRow(newrow, ParentComponent);
            Save();
            return discipRow;
        }
        
        public void AddWTPROWValues(Wtp newPlan, IEnumerable<XElement> rowValues, WTPRow discipRow)
        {
            int firstSemester = (int.Parse(rowValues.First().Attribute("Курс").Value) - 1) * 2 + int.Parse(rowValues.First().Attribute("Семестр").Value);
            int lastSemester = (int.Parse(rowValues.Last().Attribute("Курс").Value) - 1) * 2 + int.Parse(rowValues.Last().Attribute("Семестр").Value);

            for (int i = firstSemester; i <= lastSemester; i++)
            {
                var semestr = presenter.CreateNewSemester();
                //номер семестра
                semestr.WTPSEMESTER_NUM = i;
                discipRow.Semesters.Add(semestr, false);

                IEnumerable<XElement> valuesFromSemestr;

                if (i % 2 == 0)
                {
                    valuesFromSemestr = rowValues.Where(q => q.Attribute("Курс").Value == (i / 2).ToString()).Where(q => q.Attribute("Семестр").Value == "2").Where(q => q.Attribute("КодТипаЧасов").Value == "1");
                }
                else
                {
                    valuesFromSemestr = rowValues.Where(q => q.Attribute("Курс").Value == (i / 2 + 1).ToString()).Where(q => q.Attribute("Семестр").Value == "1").Where(q => q.Attribute("КодТипаЧасов").Value == "1");
                }

                foreach (var valueFromSemestr in valuesFromSemestr)
                {
                    var value = presenter.CreateNewRowValues();
                    value.WTPROWVALUES_SEMNUM = (short?)i;
                    value.WTPROWVALUES_VALUE = valueFromSemestr.Attribute("Количество").Value;

                    string WorkType = valueFromSemestr.Attribute("КодВидаРаботы").Value;
                    switch (WorkType)
                    {
                        case "1":
                            value.WTPPARAM_ID = newPlan.Params["EXAMS"].DataRow.WTPPARAM_ID;
                            break;

                        case "2":
                            value.WTPPARAM_ID = newPlan.Params["MIDTERMS"].DataRow.WTPPARAM_ID;
                            break;

                        case "101":
                            value.WTPPARAM_ID = newPlan.Params["LECTIONS"].DataRow.WTPPARAM_ID;
                            break;

                        case "103":
                            value.WTPPARAM_ID = newPlan.Params["PRACTICS"].DataRow.WTPPARAM_ID;
                            break;

                        case "102":
                            value.WTPPARAM_ID = newPlan.Params["LABWORKS"].DataRow.WTPPARAM_ID;
                            break;

                        case "10":
                            value.WTPPARAM_ID = newPlan.Params["REFERATS"].DataRow.WTPPARAM_ID;
                            break;

                        case "11":
                            value.WTPPARAM_ID = newPlan.Params["CALCGRAPHWORKS"].DataRow.WTPPARAM_ID;
                            break;

                        case "4":
                            value.WTPPARAM_ID = newPlan.Params["COURSPROJECTS"].DataRow.WTPPARAM_ID;
                            break;

                        case "5":
                            value.WTPPARAM_ID = newPlan.Params["COURSWORKS"].DataRow.WTPPARAM_ID;
                            break;

                        case "1000":
                            value.WTPPARAM_ID = newPlan.Params["EXAMS"].DataRow.WTPPARAM_ID;
                            break;

                        case "107":
                            value.WTPPARAM_ID = newPlan.Params["IndependentWork"].DataRow.WTPPARAM_ID;
                            break;

                        case "106":
                            value.WTPPARAM_ID = newPlan.Params["KSR"].DataRow.WTPPARAM_ID;
                            break;

                        default:
                            continue;
                    }

                    var rowValue = presenter.AddRowValue(value, discipRow);
                    Save();
                }
            }
        }
    }
}
