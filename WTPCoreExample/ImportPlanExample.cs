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

            var StudYearID = presenter.GetStudYearIDByYear(StudyPlan.Element("Планы").Attribute("ГодНачалаПодготовки").Value);
            newPlan.DataRow.STUDYEAR_ID = StudYearID;//год

            var ModeEducID = StudyPlan.Element("Планы").Attribute("КодФормыОбучения").Value;
            newPlan.DataRow.MODEEDUC_ID = Convert.ToInt64(ModeEducID); //1;//очное

            var FormEduc = StudyPlan.Element("Планы").Attribute("Сокращенное").Value;
            if (FormEduc == "false")
                newPlan.DataRow.FORMEDUC_ID = 1;            //полная-сокращенная форма
            if (FormEduc == "true")
                newPlan.DataRow.FORMEDUC_ID = 2;

            newPlan.DataRow.STUDYEAR_ID_VERSION = StudYearID; //год
            
            IWTPCOMPONENT component = presenter.CreateNewComponent();
            //поиск по имени STUDDISCIPCICLE_NAME
            var StudDiscipCicleID = presenter.GetStudDiscipCicleByName("Факультативы");
            component.STUDDISCIPCICLE_ID = StudDiscipCicleID;
            var wtpComponent = presenter.AddComponent(component, null);

            IWTPCOMPONENT component2 = presenter.CreateNewComponent();
            component2.STUDDISCIPCICLE_ID = 6;
            
            var wtpComponent2 = presenter.AddComponent(component2, wtpComponent);

            //пример поиска по дисциплине
            Int64 discipId = presenter.GetStudDisciplineByName("Математика123", "сокр", 1);
                        
            var newrow = presenter.CreateNewRow();
            //поиск по коду см файл Таблица соответствия кодов кафедр в АСУ и в Шахтах.xlsx

            newrow.CHAIR_ID = 25;
            newrow.STUDDISCIPLINE_ID = discipId;
            var discipRow = presenter.AddRow(newrow, wtpComponent2);

            var semestr = presenter.CreateNewSemester();
            //номер семестра
            semestr.WTPSEMESTER_NUM = 1;
            discipRow.Semesters.Add(semestr, false);

            var value = presenter.CreateNewRowValues();
            value.WTPROWVALUES_SEMNUM = 1;
            value.WTPROWVALUES_VALUE = 4;
            
            value.WTPPARAM_ID = newPlan.Params[lections].DataRow.WTPPARAM_ID; ;//Лекции, см табличку WTPPARAM

            var rowValue1 = presenter.AddRowValue(value, discipRow);
            
            Save();


            
        }

        public void Save()
        {
            presenter.Save();
        }

        
    }
}
