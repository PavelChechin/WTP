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
using SqlDataSolution;

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
            var SpecialityCode = StudyPlan.Element(XmlConst.OOP).Attribute("Шифр").Value;
            var SpecialityIDFromFile = StudyPlan.Element(XmlConst.OOP).Attribute("Код").Value;
            var SpecializationIDFromFile =  StudyPlan.Element(XmlConst.OOP).Descendants(XmlConst.OOP).Where(r => r.Attribute("Название").Value == plan.DataRow.SPECIALIZATION_NAME).Select(r => r.Attribute("Код").Value).FirstOrDefault();
            if (SpecializationIDFromFile == null) SpecializationIDFromFile = "-1";
            var StudYearID = WtpPresenter.GetStudYearIDByYear(StudyPlan.Element(XmlConst.Plans).Attribute("ГодНачалаПодготовки").Value);

            IEnumerable<XElement> parentCycles = StudyPlan.Descendants(XmlConst.Cycles).Where(q => q.Attribute("ТипБлока").Value == "1");
            foreach (var parentCycle in parentCycles)
            {
                var StudDiscipCicleID = presenter.GetStudDiscipCicleByName(parentCycle.Attribute("Идентификатор").Value, parentCycle.Attribute("Цикл").Value);  
                var StudDiscipCicleName = parentCycle.Attribute("Цикл").Value;
                var parentCycleID = parentCycle.Attribute("Код").Value;
                var Code = parentCycle.Attribute("Идентификатор").Value;
                var SortIndex = int.Parse(parentCycle.Attribute("{urn:schemas-microsoft-com:xml-msdata}rowOrder").Value) + 1;

                //если компонент с указанными параметрами не существует, то создать его, иначе найденный будет являться wtpComponent
                var parentComponent = plan.Components.Where(r => r.DataRow.STUDDISCIPCICLE_ID == StudDiscipCicleID).
                                    Where(r => r.DataRow.WTPCOMPONENT_CODE == Code).
                                    Where(r => r.DataRow.WTPCOMPONENT_PARENTID == null).
                                    Where(r => r.DataRow.STUDDISCCOMPONENT_ID == null).
                                    Where(r => r.DataRow.SPECIALIZATION_ID == null).
                                    Where(r => r.DataRow.WTPCOMPONENT_SORTINDEX == SortIndex);
                WTPComponent wtpComponent;
                if (parentComponent.Count() == 0)
                {
                    IWTPCOMPONENT component = presenter.CreateNewComponent();
                    //поиск по имени STUDDISCIPCICLE_NAME
                    component.STUDDISCIPCICLE_ID = StudDiscipCicleID;
                    component.STUDDISCIPCICLE_NAME = parentCycle.Attribute("Цикл").Value;
                    component.WTPCOMPONENT_CODE = Code;

                    wtpComponent = presenter.AddComponent(component, null);
                }
                else
                {
                    wtpComponent = parentComponent.First();
                }

                //IWTPCOMPONENT component = presenter.CreateNewComponent();
                ////поиск по имени STUDDISCIPCICLE_NAME
                //component.STUDDISCIPCICLE_ID = StudDiscipCicleID;
                //component.STUDDISCIPCICLE_NAME = parentCycle.Attribute("Цикл").Value;
                //component.WTPCOMPONENT_CODE = Code;

                //var wtpComponent = presenter.AddComponent(component, null);
                

                var childCycles = StudyPlan.Descendants(XmlConst.Cycles).Where(q => q.Attribute("ТипБлока").Value != "1").Where(q => q.Attribute("КодРодителя").Value == parentCycleID);
                if (childCycles.Count() != 0)
                {
                    foreach (var childCycle in childCycles)
                    {
                        var childStudDiscipCicleID = presenter.GetStudDiscipCicleByName(childCycle.Attribute("Идентификатор").Value, childCycle.Attribute("Цикл").Value);
                        var childComponent = plan.Components.Where(r => r.DataRow.STUDDISCIPCICLE_ID == childStudDiscipCicleID).
                                    Where(r => r.DataRow.WTPCOMPONENT_CODE == Code).
                                    Where(r => r.DataRow.WTPCOMPONENT_PARENTID == wtpComponent.DataRow.WTPCOMPONENT_ID).
                                    Where(r => r.DataRow.STUDDISCCOMPONENT_ID == null).
                                    Where(r => r.DataRow.SPECIALIZATION_ID == null).
                                    Where(r => r.DataRow.WTPCOMPONENT_SORTINDEX == SortIndex);
                        var childCycleID = childCycle.Attribute("Код").Value;
                        WTPComponent wtpComponent2;
                        if (childComponent.Count() == 0)
                        {
                            IWTPCOMPONENT component2 = presenter.CreateNewComponent();
                            component2.STUDDISCIPCICLE_ID = childStudDiscipCicleID;
                            component2.STUDDISCIPCICLE_NAME = childCycle.Attribute("Цикл").Value;
                            component2.WTPCOMPONENT_CODE = childCycle.Attribute("Идентификатор").Value;
                            component2.WTPCOMPONENT_SORTINDEX = int.Parse(childCycle.Attribute("{urn:schemas-microsoft-com:xml-msdata}rowOrder").Value) + 1;
                            wtpComponent2 = presenter.AddComponent(component2, wtpComponent);
                        }
                        else
                        {
                            wtpComponent2 = childComponent.First();
                        }

                            //IWTPCOMPONENT component2 = presenter.CreateNewComponent();
                            //var childID = presenter.GetStudDiscipCicleByName(childCycle.Attribute("Идентификатор").Value, childCycle.Attribute("Цикл").Value);
                            //var childCycleID = childCycle.Attribute("Код").Value;
                            //component2.STUDDISCIPCICLE_ID = childID;
                            //component2.STUDDISCIPCICLE_NAME = childCycle.Attribute("Цикл").Value;
                            //component2.WTPCOMPONENT_CODE = childCycle.Attribute("Идентификатор").Value;
                            //component2.WTPCOMPONENT_SORTINDEX = int.Parse(childCycle.Attribute("{urn:schemas-microsoft-com:xml-msdata}rowOrder").Value) + 1;
                            //var wtpComponent2 = presenter.AddComponent(component2, wtpComponent);

                            //сохранение после каждого действия нужно будет убрать, сохранять только в конце импорта



                        IEnumerable<XElement> planRows = StudyPlan.Descendants(XmlConst.Rows).Where(q => q.Attribute("УровеньВложения").Value == "1").Where(q => q.Attribute("КодБлока").Value == childCycleID);
                        foreach (var planRow in planRows)
                        {
                            if (planRow.Attribute("ТипОбъекта").Value == "2" & (planRow.Attribute("КодООП").Value == SpecializationIDFromFile | planRow.Attribute("КодООП").Value == SpecialityIDFromFile))
                            {
                                if (planRow.Attribute("СчитатьВПлане").Value == "true")
                                {
                                    var rowID = planRow.Attribute("Код").Value;
                                    var discipRow = AddWTPROW(planRow, wtpComponent2);

                                    var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                    AddWTPROWValues(rowValues, discipRow);
                                }
                                else continue;
                                //var rowID = planRow.Attribute("Код").Value;
                                //var discipRow = AddWTPROW(planRow, wtpComponent2);

                                //var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                //AddWTPROWValues(plan, rowValues, discipRow);
                            } else

                            if (planRow.Attribute("ТипОбъекта").Value == "1" & (planRow.Attribute("КодООП").Value == SpecializationIDFromFile | planRow.Attribute("КодООП").Value == SpecialityIDFromFile))
                            {  //дисциплины специализации 
                                WTPRowGroup wtpRowGroup;
                                IWTPROWGROUP rowGroup = presenter.CreateNewRowGroup();
                                rowGroup.SPECIALIZATION_ID = SpecializationID;
                                rowGroup.WTPROWGROUP_CODE = groupCode;
                                rowGroup.WTPROWGROUP_NAME = groupName;
                                rowGroup.WTPROWGROUP_SORTINDEX = groupSortIndex;
                                rowGroup.WTPROWGROUP_NUMBER = groupNumber;
                                wtpRowGroup = presenter.AddRowGroup(rowGroup, wtpComponent2);


                                //var moduleID = presenter.GetStudDiscComponentByName(planRow.Attribute("Идентификатор").Value, planRow.Attribute("Цикл").Value);
                                //var OOP = StudyPlan.Element(XmlConst.OOP).Descendants(XmlConst.OOP).Where(q => q.Attribute("Код").Value == planRow.Attribute("КодООП").Value).First();
                                //var SpecializationID = presenter.GetSpecializationByName(OOP.Attribute("Название").Value, SpecialityCode);
                                //var module = plan.Components.
                                //                    Where(r => r.DataRow.STUDDISCIPCICLE_ID == null).
                                //                    Where(r => r.DataRow.WTPCOMPONENT_CODE == Code).
                                //                    Where(r => r.DataRow.WTPCOMPONENT_PARENTID == wtpComponent2.DataRow.WTPCOMPONENT_ID).
                                //                    Where(r => r.DataRow.STUDDISCCOMPONENT_ID == moduleID).
                                //                    Where(r => r.DataRow.SPECIALIZATION_ID == SpecializationID).
                                //                    Where(r => r.DataRow.WTPCOMPONENT_SORTINDEX == SortIndex);
                                //WTPComponent wtpModule;
                                //if (module == null)
                                //{
                                //    IWTPCOMPONENT component3 = presenter.CreateNewComponent();
                                //    var childID = presenter.GetStudDiscipCicleByName(planRow.Attribute("Идентификатор").Value, planRow.Attribute("Цикл").Value);
                                //    component3.SPECIALIZATION_ID = SpecializationID;
                                //    component3.STUDDISCIPCICLE_NAME = planRow.Attribute("Цикл").Value;
                                //    component3.WTPCOMPONENT_CODE = planRow.Attribute("ДисциплинаКод").Value;
                                //    wtpModule = presenter.AddComponent(component3, wtpComponent2);
                                //}
                                //else
                                //{
                                //    wtpModule = module.First();
                                //}


                                //IWTPCOMPONENT component3 = presenter.CreateNewComponent();
                                //string componentCode = planRow.Attribute("ДисциплинаКод").Value;
                                //var studDiscComponentID = presenter.GetStudDiscComponentByName(planRow.Attribute("Дисциплина").Value, componentCode.Remove(componentCode.Length - 3));

                                //var OOP = StudyPlan.Element(XmlConst.OOP).Descendants(XmlConst.OOP).Where(q => q.Attribute("Код").Value == planRow.Attribute("КодООП").Value).First();/*Where(q => q.Attribute("Код").Value == planRow.Attribute("КодООП").Value).First();*/
                                //var SpecializationID = presenter.GetSpecializationByName(OOP.Attribute("Название").Value, OOP.Attribute("Шифр").Value);
                                //component3.SPECIALIZATION_ID = SpecializationID;
                                //component3.SPECIALIZATION_NAME = OOP.Attribute("Название").Value;
                                //component3.WTPCOMPONENT_CODE = planRow.Attribute("ДисциплинаКод").Value;
                                //component3.STUDDISCCOMPONENT_ID = studDiscComponentID;
                                //component3.STUDDISCCOMPONENT_NAME = planRow.Attribute("Дисциплина").Value;
                                //var wtpComponent3 = presenter.AddComponent(component3, wtpComponent2);

                                var componentID = planRow.Attribute("Код").Value;

                                var specRows = StudyPlan.Descendants(XmlConst.Rows).Where(q => q.Attribute("УровеньВложения").Value == "2").Where(q => q.Attribute("КодРодителя").Value == componentID);
                                foreach (var specRow in specRows)
                                {
                                    if (specRow.Attribute("СчитатьВПлане").Value == "true")
                                    {
                                        var rowID = specRow.Attribute("Код").Value;
                                        var discipRow = AddWTPROW(specRow, wtpComponent2);
                                        //var discipRow = AddWTPROW(chosenRow, wtpComponent2, SpecializationID);

                                        var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                        AddWTPROWValues(rowValues, discipRow);
                                    }
                                    else continue;
                                    //else continue;
                                    //var rowID = specRow.Attribute("Код").Value;
                                    //var discipRow = AddWTPROW(specRow, wtpComponent2);

                                    //var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                    //AddWTPROWValues(plan, rowValues, discipRow);
                                }

                            } else

                            if (planRow.Attribute("ТипОбъекта").Value == "5" & (planRow.Attribute("КодООП").Value == SpecializationIDFromFile | planRow.Attribute("КодООП").Value == SpecialityIDFromFile))
                            {  //дисциплины по выбору

                                int VariationID;
                                using (SqlConnection conn = ServerHelper.ConnectionHelper.GetConnection())
                                {
                                    SqlDataAdapter adapter = new SqlDataAdapter();
                                    conn.Open();
                                    SqlCommand command = new SqlCommand("SPU_WTPVARIATION_NEXTVALUE_SEL",conn);
                                    command.CommandType = CommandType.StoredProcedure;
                                    adapter.SelectCommand = command;
                                    VariationID = Convert.ToInt32(command.ExecuteScalar());
                                    conn.Close();
                                }


                                var chosenRows = StudyPlan.Descendants(XmlConst.Rows).Where(q => q.Attribute("УровеньВложения").Value == "2").Where(q => q.Attribute("КодРодителя").Value == planRow.Attribute("Код").Value);
                                foreach (var chosenRow in chosenRows)
                                {
                                    if (chosenRow.Attribute("СчитатьВПлане").Value == "true")
                                    {
                                        var rowID = chosenRow.Attribute("Код").Value;
                                        var discipRow = AddWTPROW(chosenRow, wtpComponent2, VariationID);
                                        //var discipRow = AddWTPROW(chosenRow, wtpComponent2, SpecializationID);

                                        var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                        AddWTPROWValues(rowValues, discipRow);
                                    }
                                    else continue;

                                }

                            } else





                            if (planRow.Attribute("ТипОбъекта").Value == "3" & (planRow.Attribute("КодООП").Value == SpecializationIDFromFile | planRow.Attribute("КодООП").Value == SpecialityIDFromFile))
                            {      //практики
                                var rowID = planRow.Attribute("Код").Value;
                                var practiceRow = AddWTPROW(planRow, wtpComponent2);
                                // добавить значения часов практики

                                var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID).Where(r => r.Attribute("КодТипаЧасов").Value == "1");
                                var ZE = int.Parse(rowValues.Where(r => r.Attribute("КодВидаРаботы").Value == "50").Select(r => r.Attribute("Количество").Value).First());
                                IWTPPRACTICE newPractice = presenter.CreateNewPractice();
                                newPractice.WTPPRACTICE_WEEKSCOUNT = (long)Math.Round(ZE / 1.5);   //позже изменить
                                string PracticeType = StudyPlan.Descendants(XmlConst.PracticeCatalog).Where(q => q.Attribute("Код").Value == planRow.Attribute("ВидПрактики").Value).Select(q => q.Attribute("Наименование").Value).First();
                                newPractice.TYPEPRACTICE_ID = presenter.GetTypePracticeIDByName(PracticeType);

                                var practice = presenter.AddPractice(newPractice, practiceRow);

                                AddWTPROWValues(rowValues, practiceRow);
                            }
                        }
                    }
                }
                else
                {
                    IEnumerable<XElement> planRows = StudyPlan.Descendants(XmlConst.Rows).Where(q => q.Attribute("УровеньВложения").Value == "1").Where(q => q.Attribute("КодБлока").Value == parentCycleID);
                    foreach (var planRow in planRows)
                    {
                        if (planRow.Attribute("СчитатьВПлане").Value == "true")
                        {
                            if (planRow.Attribute("ТипОбъекта").Value == "6" & (planRow.Attribute("КодООП").Value == SpecializationIDFromFile | planRow.Attribute("КодООП").Value == SpecialityIDFromFile))  //государственная итоговая аттестация
                            {
                                var rowID = planRow.Attribute("Код").Value;
                                var diplomaRow = AddWTPROW(planRow, wtpComponent);

                                var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                AddWTPROWValues(rowValues, diplomaRow);
                            }
                            else

                            if (planRow.Attribute("ТипОбъекта").Value == "2")  //факультативы
                            {
                                var rowID = planRow.Attribute("Код").Value;
                                var diplomaRow = AddWTPROW(planRow, wtpComponent);

                                var rowValues = StudyPlan.Descendants(XmlConst.NewHours).Where(q => q.Attribute("КодОбъекта").Value == rowID);

                                AddWTPROWValues(rowValues, diplomaRow);
                            }
                        }
                        else continue;
                    }
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
            //        var graph = presenter.AddGraphValue(newValue);//написать метод в презентере для добавления значений графика
            //    }
            //}



            Save();


            return true;
        }

        public void Save()
        {
            _presenter.Save();
        }

        public WTPRow AddWTPROW(XElement planRow, WTPComponent ParentComponent, int VariationID) //для дисциплин по выбору
        {
            Int64 discipId = _presenter.GetStudDisciplineByName(planRow.Attribute("Дисциплина").Value, "", 1);
            var SortIndex = int.Parse(planRow.Attribute("Порядок").Value);
            var Number = int.Parse(planRow.Attribute("Номер").Value);
            var Index = planRow.Attribute("ДисциплинаКод").Value;
            var ChairID = _presenter.GetChairByCode(planRow.Attribute("КодКафедры").Value);
            var discipline = _presenter.Plan.Rows.
                                Where(r => r.DataRow.WTPROW_SORTINDEX == SortIndex).
                                Where(r => r.DataRow.WTPROW_NUMBER == Number).
                                Where(r => r.DataRow.STUDDISCIPLINE_ID == discipId).
                                Where(r => r.DataRow.WTPROW_VARIATIONID == VariationID).
                                Where(r => r.DataRow.CHAIR_ID == ChairID).
                                Where(r => r.DataRow.WTPROW_INDEX == Index);

            WTPRow discipRow;
            if (discipline.Count() == 0)
            {
                var newrow = _presenter.CreateNewRow();
                newrow.CHAIR_ID = _presenter.GetChairByCode(planRow.Attribute("КодКафедры").Value);
                newrow.STUDDISCIPLINE_ID = discipId;
                newrow.WTPROW_SORTINDEX = SortIndex;
                newrow.WTPROW_NUMBER = Number;
                newrow.WTPROW_INDEX = Index;
                newrow.WTPROW_VARIATIONID = VariationID;
                discipRow = _presenter.AddRow(newrow, ParentComponent);
            }
            else
            {
                discipRow = discipline.First();
            }
            return discipRow;

            //Int64 discipId = _presenter.GetStudDisciplineByName(planRow.Attribute("Дисциплина").Value, "", 1);
            //var newrow = _presenter.CreateNewRow();
            ////newrow.CHAIR_ID = presenter.GetChairByCode(planRow.Attribute("КодКафедры").Value);
            //newrow.STUDDISCIPLINE_ID = discipId;
            //newrow.WTPROW_SORTINDEX = int.Parse(planRow.Attribute("Порядок").Value);
            //newrow.WTPROW_NUMBER = int.Parse(planRow.Attribute("Номер").Value);
            //newrow.WTPROW_VARIATIONID = VariationID;
            //var discipRow = _presenter.AddRow(newrow, ParentComponent);
            //return discipRow;
        }



        public WTPRow AddWTPROW(XElement planRow, WTPComponent ParentComponent)
        {
            Int64 discipId = _presenter.GetStudDisciplineByName(planRow.Attribute("Дисциплина").Value, "", 1);
            var SortIndex = int.Parse(planRow.Attribute("Порядок").Value);
            var Number = int.Parse(planRow.Attribute("Номер").Value);
            var Index = planRow.Attribute("ДисциплинаКод").Value;
            var ChairID = _presenter.GetChairByCode(planRow.Attribute("КодКафедры").Value);
            var discipline = _presenter.Plan.Rows.
                                Where(r => r.DataRow.WTPROW_SORTINDEX == SortIndex).
                                Where(r => r.DataRow.WTPROW_NUMBER == Number).
                                Where(r => r.DataRow.STUDDISCIPLINE_ID == discipId).
                                Where(r => r.DataRow.CHAIR_ID == ChairID).
                                Where(r => r.DataRow.WTPROW_INDEX == Index);
                                
            WTPRow discipRow;
            if (discipline.Count() == 0)
            {
                var newrow = _presenter.CreateNewRow();
                newrow.CHAIR_ID = _presenter.GetChairByCode(planRow.Attribute("КодКафедры").Value);
                newrow.STUDDISCIPLINE_ID = discipId;
                newrow.WTPROW_SORTINDEX = SortIndex;
                newrow.WTPROW_NUMBER = Number;
                newrow.WTPROW_INDEX = Index;
                discipRow = _presenter.AddRow(newrow, ParentComponent);
            }
            else
            {
                discipRow = discipline.First();
            }
            //var newrow = _presenter.CreateNewRow();
            ////newrow.CHAIR_ID = presenter.GetChairByCode(planRow.Attribute("КодКафедры").Value);
            //newrow.STUDDISCIPLINE_ID = discipId;
            //newrow.WTPROW_SORTINDEX = int.Parse(planRow.Attribute("Порядок").Value);
            //newrow.WTPROW_NUMBER = int.Parse(planRow.Attribute("Номер").Value);
            //var discipRow = _presenter.AddRow(newrow, ParentComponent);
            return discipRow;
        }

        public void AddWTPROWValues(IEnumerable<XElement> rowValues, WTPRow discipRow)
        {
            List<int> semesters = new List<int>();
            foreach (var rowValue in rowValues.Where(r => r.Attribute("КодТипаЧасов").Value == "1"))    //where используется для того, чтобы отсеивать значения предметов в часах в неделю, которые присутствуют в файле, но не отображаются в Шахтах
            {
                int semestr = (int.Parse(rowValue.Attribute("Курс").Value) - 1) * 2 + int.Parse(rowValue.Attribute("Семестр").Value);
                semesters.Add(semestr);
            }
            semesters = semesters.Distinct().ToList();

            foreach (int semestr in semesters)
            {
                var newSemestr = _presenter.CreateNewSemester();
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
                            ParamID = _presenter.Plan.Params["ZE"].DataRow.WTPPARAM_ID;
                            break;

                        case "1":
                            ParamID = _presenter.Plan.Params["EXAMS"].DataRow.WTPPARAM_ID;
                            break;

                        case "2":
                            ParamID = _presenter.Plan.Params["MIDTERMS"].DataRow.WTPPARAM_ID;
                            break;

                        case "3":
                            ParamID = _presenter.Plan.Params["DIFMIDTERMS"].DataRow.WTPPARAM_ID;  //зачет с оценкой
                            break;

                        case "108";
                            ParamID = _presenter.Plan.Params["CONTROL"].DataRow.WTPPARAM_ID;   //Часы на контроль
                            break;

                        case "101":
                            ParamID = _presenter.Plan.Params["LECTIONS"].DataRow.WTPPARAM_ID;
                            break;

                        case "103":
                            ParamID = _presenter.Plan.Params["PRACTICS"].DataRow.WTPPARAM_ID;
                            break;

                        case "102":
                            ParamID = _presenter.Plan.Params["LABWORKS"].DataRow.WTPPARAM_ID;
                            break;

                        case "10":
                            ParamID = _presenter.Plan.Params["REFERATS"].DataRow.WTPPARAM_ID;
                            break;

                        case "11":
                            ParamID = _presenter.Plan.Params["CALCGRAPHWORKS"].DataRow.WTPPARAM_ID;
                            break;

                        case "4":
                            ParamID = _presenter.Plan.Params["COURSPROJECTS"].DataRow.WTPPARAM_ID;
                            break;

                        case "5":
                            ParamID = _presenter.Plan.Params["COURSWORKS"].DataRow.WTPPARAM_ID;
                            break;

                        case "1000":
                            ParamID = _presenter.Plan.Params["TOTALHOURS"].DataRow.WTPPARAM_ID;
                            break;

                        case "107":
                            ParamID = _presenter.Plan.Params["IndependentWork"].DataRow.WTPPARAM_ID;
                            break;

                        case "106":
                            ParamID = _presenter.Plan.Params["KSR"].DataRow.WTPPARAM_ID;
                            break;

                        default:
                            continue;
                    }
                    if (ParamID != 0)
                    {
                        if(_presenter.Plan.Values == null)
                        {
                            var value = _presenter.CreateNewRowValues();
                            value.WTPROWVALUES_SEMNUM = SemNum;
                            value.WTPROWVALUES_VALUE = Value;
                            value.WTPPARAM_ID = ParamID;
                            var rowValue = _presenter.AddRowValue(value, discipRow);
                        }
                        else
                        {
                            var RowValues = _presenter.Plan.Values.
                                            Where(r => r.DataRow.WTPROWVALUES_SEMNUM == SemNum).
                                            Where(r => r.DataRow.WTPROWVALUES_VALUE.ToString() == Value).
                                            Where(r => r.DataRow.WTPPARAM_ID == ParamID);
                            if (RowValues.Count() == 0)
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
    }
}
