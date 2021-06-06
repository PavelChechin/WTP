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

            if(plan.DataRow.SPECIALITY_NUMB != SpecialityCode.Trim())  //поправить ошибку
            {
                errorMessage += " код специальности,";
                error = true;
            }

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

            /*Нужно создать статический класс констант для хранения имен нодов и неймспейса xml.
             * Чтобы все константы были собраны в одном месте
             * чтобы обращение было таким: StudyPlan.Descendants(XmlConst.Cycles)
             */
            IEnumerable<XElement> parentCycles = StudyPlan.Descendants(XmlConst.Cycles).Where(q => q.Attribute("ТипБлока").Value == "1");
            foreach (var parentCycle in parentCycles)
            {
                IWTPCOMPONENT component = presenter.CreateNewComponent();
                //поиск по имени STUDDISCIPCICLE_NAME
                var StudDiscipCicleID = presenter.GetStudDiscipCicleByName(parentCycle.Attribute("Идентификатор").Value, parentCycle.Attribute("Цикл").Value);  
                component.STUDDISCIPCICLE_ID = StudDiscipCicleID;
                var cycleID = parentCycle.Attribute("Код").Value;
                var wtpComponent = presenter.AddComponent(component, null);
                

                var childCycles = StudyPlan.Descendants(XmlConst.Cycles).Where(q => q.Attribute("ТипБлока").Value != "1").Where(q => q.Attribute("КодРодителя").Value == cycleID);
                if (childCycles.Count() != 0)
                {
                    foreach (var childCycle in childCycles)
                    {
                        IWTPCOMPONENT component2 = presenter.CreateNewComponent();
                        var childID = presenter.GetStudDiscipCicleByName(childCycle.Attribute("Идентификатор").Value, childCycle.Attribute("Цикл").Value);
                        component2.STUDDISCIPCICLE_ID = childID;
                        var wtpComponent2 = presenter.AddComponent(component2, wtpComponent);

                        //сохранение после каждого действия нужно будет убрать, сохранять только в конце импорта
                        


                        IEnumerable<XElement> planRows = StudyPlan.Descendants(XmlConst.Rows).Where(q => q.Attribute("УровеньВложения").Value == "1");
                        foreach (var planRow in planRows)
                        {
                            if (planRow.Attribute("ТипОбъекта").Value == "2")
                            {
                                var rowID = planRow.Attribute("Код").Value;
                                var discipRow = AddWTPROW(planRow, wtpComponent2);

                                var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                AddWTPROWValues(plan, rowValues, discipRow);
                            }

                            if (planRow.Attribute("ТипОбъекта").Value == "1")  //дисциплины специализации
                            {
                                IWTPCOMPONENT component3 = presenter.CreateNewComponent();
                                var ID = presenter.GetStudDiscComponentByName(planRow.Attribute("Дисциплина").Value);
                                var componentID = planRow.Attribute("Код").Value;
                                var OOP = StudyPlan.Element(XmlConst.OOP).Descendants(XmlConst.OOP).Where(q => q.Attribute("Код").Value == planRow.Attribute("КодООП").Value).First();/*Where(q => q.Attribute("Код").Value == planRow.Attribute("КодООП").Value).First();*/
                                var SpecializationID = presenter.GetSpecializationByName(OOP.Attribute("Название").Value, OOP.Attribute("Шифр").Value);
                                component3.SPECIALIZATION_ID = SpecializationID;
                                component3.SPECIALIZATION_NAME = OOP.Attribute("Название").Value;
                                var wtpComponent3 = presenter.AddComponent(component3, wtpComponent2);

                                var specRows = StudyPlan.Descendants(XmlConst.Rows).Where(q => q.Attribute("УровеньВложения").Value == "2").Where(q => q.Attribute("КодРодителя").Value == componentID);
                                foreach (var specRow in specRows)
                                {
                                    var rowID = specRow.Attribute("Код").Value;
                                    var discipRow = AddWTPROW(specRow, wtpComponent3);

                                    var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                    AddWTPROWValues(plan, rowValues, discipRow);
                                }

                            }

                            if (planRow.Attribute("ТипОбъекта").Value == "5")  //дисциплины по выбору
                            {
                                IWTPCOMPONENT component3 = presenter.CreateNewComponent();
                                var ID = presenter.GetStudDiscComponentByName(planRow.Attribute("Дисциплина").Value);
                                var componentID = planRow.Attribute("Код").Value;
                                XElement OOP = StudyPlan.Element(XmlConst.OOP);
                                if (OOP.Attribute("Код").Value != planRow.Attribute("КодООП").Value)
                                {
                                    OOP = StudyPlan.Element(XmlConst.OOP).Descendants(XmlConst.OOP).Where(q => q.Attribute("Код").Value == planRow.Attribute("КодООП").Value).First();
                                }

                                var SpecializationID = presenter.GetSpecializationByName(OOP.Attribute("Название").Value, OOP.Attribute("Шифр").Value);
                                component3.STUDDISCCOMPONENT_ID = ID;
                                component3.STUDDISCCOMPONENT_NAME = planRow.Attribute("Дисциплина").Value;
                                component3.SPECIALIZATION_ID = SpecializationID;
                                component3.SPECIALIZATION_NAME = OOP.Attribute("Название").Value;
                                var wtpComponent3 = presenter.AddComponent(component3, wtpComponent2);

                                var chosenRows = StudyPlan.Descendants(XmlConst.Rows).Where(q => q.Attribute("УровеньВложения").Value == "2").Where(q => q.Attribute("КодРодителя").Value == componentID);
                                foreach (var chosenRow in chosenRows)
                                {
                                    var rowID = chosenRow.Attribute("Код").Value;
                                    var discipRow = AddWTPROW(chosenRow, wtpComponent3);

                                    var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                    AddWTPROWValues(plan, rowValues, discipRow);

                                }

                            }

                            //if (planRow.Attribute("ТипОбъекта").Value == "3") { } //практики

                        }
                    }
                }
            }
            Save();


            return true;
        }

        public void Save()
        {
            _presenter.Save();
        }

        public WTPRow AddWTPROW(XElement planRow, WTPComponent ParentComponent)
        {
            Int64 discipId = _presenter.GetStudDisciplineByName(planRow.Attribute("Дисциплина").Value, "", 1);
            var newrow = _presenter.CreateNewRow();
            //newrow.CHAIR_ID = presenter.GetChairByCode(planRow.Attribute("КодКафедры").Value);
            newrow.STUDDISCIPLINE_ID = discipId;
            var discipRow = _presenter.AddRow(newrow, ParentComponent);
            return discipRow;
        }
        
        public void AddWTPROWValues(Wtp newPlan, IEnumerable<XElement> rowValues, WTPRow discipRow)
        {
            int firstSemester = (int.Parse(rowValues.First().Attribute("Курс").Value) - 1) * 2 + int.Parse(rowValues.First().Attribute("Семестр").Value);
            int lastSemester = (int.Parse(rowValues.Last().Attribute("Курс").Value) - 1) * 2 + int.Parse(rowValues.Last().Attribute("Семестр").Value);

            for (int i = firstSemester; i <= lastSemester; i++)
            {
                var semestr = _presenter.CreateNewSemester();
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
                    var value = _presenter.CreateNewRowValues();
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
                            value.WTPPARAM_ID = newPlan.Params["TOTALHOURS"].DataRow.WTPPARAM_ID;
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

                    var rowValue = _presenter.AddRowValue(value, discipRow);
                }
            }
        }
    }
}
