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
        WtpPresenter _presenter;

        public ImportPlanExample()
        {
            
        }

        public bool CheckImportFile(Wtp plan, XDocument xdoc, out string errorMessage)
        {
            var StudyPlan = xdoc.Root.Element(XmlConst.diffgram).Element(XmlConst.dsMMISDB);
            var SpecialityCode = StudyPlan.Element(XmlConst.OOP).Attribute("Шифр").Value;
            var ModeEducID = StudyPlan.Element(XmlConst.Plans).Attribute("КодФормыОбучения").Value;
            var StudYearID = WtpPresenter.GetStudYearIDByYear(StudyPlan.Element(XmlConst.Plans).Attribute("ГодНачалаПодготовки").Value);

            errorMessage = "Не совпадает";
            bool error = false;

            //if(plan.DataRow.SPECIALITY_NUMB != SpecialityCode.Trim())  //поправить ошибку
            //{
            //    errorMessage += " код специальности,";
            //    error = true;
            //}

            if (plan.DataRow.MODEEDUC_ID != Convert.ToInt64(ModeEducID))
            {
                errorMessage += " форма обучения,";
                error = true;
            }

            var FormEduc = StudyPlan.Element(XmlConst.Plans).Attribute("Сокращённое").Value;
            if (plan.DataRow.FORMEDUC_ID != ImportConverter.GetFormEducId(FormEduc))
            {
                errorMessage += " полный или сокращенный срок обучения,";
                error = true;
            }
            errorMessage = errorMessage.TrimEnd(',') + ".";

            return error;

        }
        public bool Import(WtpPresenter presenter, XDocument xdoc, out string errorMessage)
        {
            _presenter = presenter;
            
            var plan = presenter.Plan;
            errorMessage = string.Empty;
            //Создание плана скорее всего будет отдельно, поэтому вынесла этот процесс из логики импорта, 
            //Wtp newPlan = presenter.CreateWtp();

            var StudyPlan = xdoc.Root.Element(XmlConst.diffgram).Element(XmlConst.dsMMISDB);

            var StudYearID = WtpPresenter.GetStudYearIDByYear(StudyPlan.Element(XmlConst.Plans).Attribute("ГодНачалаПодготовки").Value);

            IEnumerable<XElement> parentCycles = StudyPlan.Descendants(XmlConst.Cycles).Where(q => q.Attribute("ТипБлока").Value == "1");
            foreach (var parentCycle in parentCycles)
            {
                var StudDiscipCicleID = presenter.GetStudDiscipCicleByName(parentCycle.Attribute("Идентификатор").Value, parentCycle.Attribute("Цикл").Value);  
                var StudDiscipCicleName = parentCycle.Attribute("Цикл").Value;
                var parentCycleID = parentCycle.Attribute("Код").Value;
                //var cycle = DBManager.GetDataSourse<IWTPCOMPONENT>().Rows.Cast<IWTPCOMPONENT>().
                //    Where(r => r.STUDDISCIPCICLE_ID == StudDiscipCicleID).
                //    Where(r => r.STUDDISCIPCICLE_NAME == StudDiscipCicleName);
                //if (cycle.Count() == 0)
                //{
                //    IWTPCOMPONENT component = presenter.CreateNewComponent();
                //    //поиск по имени STUDDISCIPCICLE_NAME
                //    component.STUDDISCIPCICLE_ID = StudDiscipCicleID;
                //    component.STUDDISCIPCICLE_NAME = parentCycle.Attribute("Цикл").Value;
                //}
                //else
                //{

                //}

                IWTPCOMPONENT component = presenter.CreateNewComponent();
                //поиск по имени STUDDISCIPCICLE_NAME
                component.STUDDISCIPCICLE_ID = StudDiscipCicleID;
                component.STUDDISCIPCICLE_NAME = parentCycle.Attribute("Цикл").Value;
                component.WTPCOMPONENT_CODE = parentCycle.Attribute("Идентификатор").Value;
                component.WTPCOMPONENT_SORTINDEX = int.Parse(parentCycle.Attribute("{urn:schemas-microsoft-com:xml-msdata}rowOrder").Value) + 1;

                var wtpComponent = presenter.AddComponent(component, null);
                

                var childCycles = StudyPlan.Descendants(XmlConst.Cycles).Where(q => q.Attribute("ТипБлока").Value != "1").Where(q => q.Attribute("КодРодителя").Value == parentCycleID);
                if (childCycles.Count() != 0)
                {
                    foreach (var childCycle in childCycles)
                    {
                        IWTPCOMPONENT component2 = presenter.CreateNewComponent();
                        var childID = presenter.GetStudDiscipCicleByName(childCycle.Attribute("Идентификатор").Value, childCycle.Attribute("Цикл").Value);
                        var childCycleID = childCycle.Attribute("Код").Value;
                        component2.STUDDISCIPCICLE_ID = childID;
                        component2.STUDDISCIPCICLE_NAME = childCycle.Attribute("Цикл").Value;
                        component2.WTPCOMPONENT_CODE = childCycle.Attribute("Идентификатор").Value;
                        component2.WTPCOMPONENT_SORTINDEX = int.Parse(childCycle.Attribute("{urn:schemas-microsoft-com:xml-msdata}rowOrder").Value) + 1;
                        var wtpComponent2 = presenter.AddComponent(component2, wtpComponent);

                        //сохранение после каждого действия нужно будет убрать, сохранять только в конце импорта
                        


                        IEnumerable<XElement> planRows = StudyPlan.Descendants(XmlConst.Rows).Where(q => q.Attribute("УровеньВложения").Value == "1").Where(q => q.Attribute("КодБлока").Value == childCycleID);
                        foreach (var planRow in planRows)
                        {
                            //if (planRow.Attribute("ТипОбъекта").Value == "2")
                            //{
                            //    var rowID = planRow.Attribute("Код").Value;
                            //    var discipRow = AddWTPROW(planRow, wtpComponent2);

                            //    var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                            //    AddWTPROWValues(plan, rowValues, discipRow);
                            //}

                            //if (planRow.Attribute("ТипОбъекта").Value == "1")  //дисциплины специализации 
                            //{
                            //    IWTPCOMPONENT component3 = presenter.CreateNewComponent();
                            //    string componentCode = planRow.Attribute("ДисциплинаКод").Value;
                            //    var studDiscComponentID = presenter.GetStudDiscComponentByName(planRow.Attribute("Дисциплина").Value, componentCode.Remove(componentCode.Length - 3));

                            //    var OOP = StudyPlan.Element(XmlConst.OOP).Descendants(XmlConst.OOP).Where(q => q.Attribute("Код").Value == planRow.Attribute("КодООП").Value).First();/*Where(q => q.Attribute("Код").Value == planRow.Attribute("КодООП").Value).First();*/
                            //    var SpecializationID = presenter.GetSpecializationByName(OOP.Attribute("Название").Value, OOP.Attribute("Шифр").Value);
                            //    component3.SPECIALIZATION_ID = SpecializationID;
                            //    component3.SPECIALIZATION_NAME = OOP.Attribute("Название").Value;
                            //    component3.WTPCOMPONENT_CODE = planRow.Attribute("ДисциплинаКод").Value;
                            //    component3.STUDDISCCOMPONENT_ID = studDiscComponentID;
                            //    component3.STUDDISCCOMPONENT_NAME = planRow.Attribute("Дисциплина").Value;
                            //    var wtpComponent3 = presenter.AddComponent(component3, wtpComponent2);

                            //    var componentID = planRow.Attribute("Код").Value;

                            //    var specRows = StudyPlan.Descendants(XmlConst.Rows).Where(q => q.Attribute("УровеньВложения").Value == "2").Where(q => q.Attribute("КодРодителя").Value == componentID);
                            //    foreach (var specRow in specRows)
                            //    {
                            //        var rowID = specRow.Attribute("Код").Value;
                            //        var discipRow = AddWTPROW(specRow, wtpComponent3);

                            //        var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                            //        AddWTPROWValues(plan, rowValues, discipRow);
                            //    }

                            //}

                            //if (planRow.Attribute("ТипОбъекта").Value == "5")  //дисциплины по выбору     вносить только те дисциплины, у которых стоит галочка Считать в плане
                            //{
                            //    //XElement OOP = StudyPlan.Element(XmlConst.OOP);
                            //    //if (OOP.Attribute("Код").Value != planRow.Attribute("КодООП").Value)
                            //    //{
                            //    //    OOP = StudyPlan.Element(XmlConst.OOP).Descendants(XmlConst.OOP).Where(q => q.Attribute("Код").Value == planRow.Attribute("КодООП").Value).First();
                            //    //}

                            //    //var SpecializationID = presenter.GetSpecializationByName(OOP.Attribute("Название").Value, OOP.Attribute("Шифр").Value);
                            //    //component3.STUDDISCCOMPONENT_ID = ID;
                            //    //component3.STUDDISCCOMPONENT_NAME = planRow.Attribute("Дисциплина").Value;
                            //    //component3.SPECIALIZATION_ID = SpecializationID;
                            //    //component3.SPECIALIZATION_NAME = OOP.Attribute("Название").Value;
                            //    //var wtpComponent3 = presenter.AddComponent(component3, wtpComponent2);

                            //    //var VariationID = presenter.GetLastVariationID() + 1;
                            //    var chosenRows = StudyPlan.Descendants(XmlConst.Rows).Where(q => q.Attribute("УровеньВложения").Value == "2").Where(q => q.Attribute("КодРодителя").Value == componentID);
                            //    foreach (var chosenRow in chosenRows)
                            //    {
                            //        var rowID = chosenRow.Attribute("Код").Value;
                            //        var discipRow = AddWTPROW(chosenRow, wtpComponent2);

                            //        var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                            //        AddWTPROWValues(plan, rowValues, discipRow);

                            //    }

                            //}





                            if (planRow.Attribute("ТипОбъекта").Value == "3")  //практики
                            {
                                var rowID = planRow.Attribute("Код").Value;
                                var practiceRow = AddWTPROW(planRow, wtpComponent2);
                                // добавить значения часов практики

                                var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID).Where(r => r.Attribute("КодТипаЧасов").Value == "1");

                                IWTPPRACTICE newPractice = presenter.CreateNewPractice();
                                newPractice.WTPPRACTICE_WEEKSCOUNT = 3;
                                string PracticeType = StudyPlan.Descendants(XmlConst.PracticeCatalog).Where(q => q.Attribute("Код").Value == planRow.Attribute("ВидПрактики").Value).Select(q => q.Attribute("Наименование").Value).First();
                                newPractice.TYPEPRACTICE_ID = presenter.GetTypePracticeIDByName(PracticeType);

                                var practice = presenter.AddPractice(newPractice, practiceRow);

                                //////////////временное решение, пока не будет решён вопрос с длительностью практик///////////////////////////
                                List<int> semesters = new List<int>();
                                foreach (var rowValue in rowValues)
                                {
                                    int semestr = (int.Parse(rowValue.Attribute("Курс").Value) - 1) * 2 + int.Parse(rowValue.Attribute("Семестр").Value);
                                    semesters.Add(semestr);
                                }
                                semesters = semesters.Distinct().ToList();

                                foreach (int semestr in semesters)
                                {
                                    var newSemestr = _presenter.CreateNewSemester();
                                    //номер семестра
                                    newSemestr.WTPSEMESTER_NUM = semestr;
                                    practiceRow.Semesters.Add(newSemestr, false);
                                }
                                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            }
                        }
                    }
                }
                else
                {
                    //IEnumerable<XElement> planRows = StudyPlan.Descendants(XmlConst.Rows).Where(q => q.Attribute("УровеньВложения").Value == "1").Where(q => q.Attribute("КодБлока").Value == parentCycleID);
                    //foreach (var planRow in planRows)
                    //{
                    //    if (planRow.Attribute("ТипОбъекта").Value == "6" || planRow.Attribute("ТипОбъекта").Value == "2")  //государственная итоговая аттестация и факультативы
                    //    {
                    //        var rowID = planRow.Attribute("Код").Value;
                    //        var diplomaRow = AddWTPROW(planRow, wtpComponent);

                    //        var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                    //        AddWTPROWValues(plan, rowValues, diplomaRow);
                    //    }
                    //}
                }
            }

            //импорт календарного учебного графика
            //int maxCourse = int.Parse(StudyPlan.Descendants("{http://tempuri.org/dsMMISDB.xsd}Заезды").Last().Attribute("Курс").Value);   
            //for (int i = 1; i <= maxCourse; i++)
            //{
            //    IEnumerable<XElement> graphValues = StudyPlan.Descendants(XmlConst.Graph).Where(q => q.Attribute("Курс").Value == i.ToString());
            //    foreach (XElement graphValue in graphValues)
            //    {
            //        int semestr = (int.Parse(graphValue.Attribute("Курс").Value) - 1) * 2 + int.Parse(graphValue.Attribute("Семестр").Value);
            //        int weekNumber = int.Parse(graphValue.Attribute("НомерНедели").Value);
            //        long typeActivity = presenter.GetTypeActivityIDByCode(int.Parse(graphValue.Attribute("КодВидаДеятельности").Value));  //написать метод в презентере для поиска по коду деятельности

            //        var newValue = presenter.CreateNewGraphValue();
            //        newValue.CALENDARGRAPHVALUES_SEMNUM = semestr;
            //        //newValue.CALENDARGRAPHVALUES_WEEK = weekNumber;
            //        newValue.CALENDARGRAPHVALUES_ID = typeActivity;
            //        var graph = presenter.AddGraphValue();//написать метод в презентере для добавления значений графика
            //    }
            //}



            Save();


            return true;
        }

        public void Save()
        {
            _presenter.Save();
        }

        //public WTPRow AddWTPROW(XElement planRow, WTPComponent ParentComponent, Int64 VariationID) //для дисциплин по выбору
        //{
        //    Int64 discipId = _presenter.GetStudDisciplineByName(planRow.Attribute("Дисциплина").Value, "", 1);
        //    var newrow = _presenter.CreateNewRow();
        //    //newrow.CHAIR_ID = presenter.GetChairByCode(planRow.Attribute("КодКафедры").Value);
        //    newrow.STUDDISCIPLINE_ID = discipId;
        //    newrow.WTPROW_VARIATIONID = VariationID;
        //    var discipRow = _presenter.AddRow(newrow, ParentComponent);
        //    return discipRow;
        //}



        public WTPRow AddWTPROW(XElement planRow, WTPComponent ParentComponent)
        {
            Int64 discipId = _presenter.GetStudDisciplineByName(planRow.Attribute("Дисциплина").Value, "", 1);
            var newrow = _presenter.CreateNewRow();
            //newrow.CHAIR_ID = presenter.GetChairByCode(planRow.Attribute("КодКафедры").Value);
            newrow.STUDDISCIPLINE_ID = discipId;
            newrow.WTPROW_SORTINDEX = int.Parse(planRow.Attribute("Порядок").Value);
            newrow.WTPROW_NUMBER = int.Parse(planRow.Attribute("Номер").Value);
            var discipRow = _presenter.AddRow(newrow, ParentComponent);
            return discipRow;
        }

        public void AddWTPROWValues(Wtp newPlan, IEnumerable<XElement> rowValues, WTPRow discipRow)
        {
            List<int> semesters = new List<int>();
            foreach (var rowValue in rowValues.Where(r => r.Attribute("КодТипаЧасов").Value == "1"))
            {
                int semestr = (int.Parse(rowValue.Attribute("Курс").Value) - 1) * 2 + int.Parse(rowValue.Attribute("Семестр").Value);
                semesters.Add(semestr);
            }
            semesters = semesters.Distinct().ToList();

            //int firstSemester = (int.Parse(rowValues.Where(r => r.Attribute("КодТипаЧасов").Value == "1").First().Attribute("Курс").Value) - 1) * 2 + int.Parse(rowValues.Where(r => r.Attribute("КодТипаЧасов").Value == "1").First().Attribute("Семестр").Value);    //where используется для того, чтобы отсеиваться значения предметов в часах в неделю
            //int lastSemester = (int.Parse(rowValues.Where(r => r.Attribute("КодТипаЧасов").Value == "1").Last().Attribute("Курс").Value) - 1) * 2 + int.Parse(rowValues.Where(r => r.Attribute("КодТипаЧасов").Value == "1").Last().Attribute("Семестр").Value);       //которые присутствуют в файле, но не отображаются в Шахтах

            foreach (int semestr in semesters)
            {
                var newSemestr = _presenter.CreateNewSemester();
                //номер семестра
                newSemestr.WTPSEMESTER_NUM = semestr;
                discipRow.Semesters.Add(newSemestr, false);

                IEnumerable<XElement> valuesFromSemestr;

                if (semestr % 2 == 0)
                {
                    valuesFromSemestr = rowValues.Where(q => q.Attribute("Курс").Value == (semestr / 2).ToString()).Where(q => q.Attribute("Семестр").Value == "2").Where(q => q.Attribute("КодТипаЧасов").Value == "1");
                }
                else
                {
                    valuesFromSemestr = rowValues.Where(q => q.Attribute("Курс").Value == (semestr / 2 + 1).ToString()).Where(q => q.Attribute("Семестр").Value == "1").Where(q => q.Attribute("КодТипаЧасов").Value == "1");
                }

                foreach (var valueFromSemestr in valuesFromSemestr)
                {
                    var SemNum = (short?)semestr;
                    var Value = valueFromSemestr.Attribute("Количество").Value;
                    Int64? ParamID = 0;

                    string WorkType = valueFromSemestr.Attribute("КодВидаРаботы").Value;
                    switch (WorkType)
                    {
                        case "50":
                            ParamID = newPlan.Params["ZE"].DataRow.WTPPARAM_ID;
                            break;

                        case "1":
                            ParamID = newPlan.Params["EXAMS"].DataRow.WTPPARAM_ID;
                            break;

                        case "2":
                            ParamID = newPlan.Params["MIDTERMS"].DataRow.WTPPARAM_ID;
                            break;

                        case "101":
                            ParamID = newPlan.Params["LECTIONS"].DataRow.WTPPARAM_ID;
                            break;

                        case "103":
                            ParamID = newPlan.Params["PRACTICS"].DataRow.WTPPARAM_ID;
                            break;

                        case "102":
                            ParamID = newPlan.Params["LABWORKS"].DataRow.WTPPARAM_ID;
                            break;

                        case "10":
                            ParamID = newPlan.Params["REFERATS"].DataRow.WTPPARAM_ID;
                            break;

                        case "11":
                            ParamID = newPlan.Params["CALCGRAPHWORKS"].DataRow.WTPPARAM_ID;
                            break;

                        case "4":
                            ParamID = newPlan.Params["COURSPROJECTS"].DataRow.WTPPARAM_ID;
                            break;

                        case "5":
                            ParamID = newPlan.Params["COURSWORKS"].DataRow.WTPPARAM_ID;
                            break;

                        case "1000":
                            ParamID = newPlan.Params["TOTALHOURS"].DataRow.WTPPARAM_ID;
                            break;

                        case "107":
                            ParamID = newPlan.Params["IndependentWork"].DataRow.WTPPARAM_ID;
                            break;

                        case "106":
                            ParamID = newPlan.Params["KSR"].DataRow.WTPPARAM_ID;
                            break;

                        default:
                            continue;
                    }
                    if (ParamID != 0)
                    {
                        var value = _presenter.CreateNewRowValues();
                        value.WTPROWVALUES_SEMNUM = SemNum;
                        value.WTPROWVALUES_VALUE = Value;
                        value.WTPPARAM_ID = ParamID;
                        var rowValue = _presenter.AddRowValue(value, discipRow);
                    }
                }
            }
        }
    }
}
