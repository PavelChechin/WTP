using System;

namespace WTPCore.Data.SourceInrefaces
{
    public interface IWTPSOURCE
	{

        Int64? WTP_ID
        {
            get;
            set;
        }

        DateTime? WTP_CHANGEDATE
        {
            get;
            set;
        }

        Boolean? WTP_STATE
        {
            get;
            set;
        }

        Int64? WTPTEMPLATE_ID
        {
            get;
            set;
        }

        Int64? STUDYEAR_ID_VERSION
        {
            get;
            set;
        }

        String STUDYEAR_NAME_VERSION
        {
            get;
            set;
        }

        String STUDYEAR_SHORTNAME_VERSION
        {
            get;
            set;
        }

        Int64? SPECIALFACULTY_ID
        {
            get;
            set;
        }

        Int64? SPECIALIZATION_ID
        {
            get;
            set;
        }

        string SPECIALIZATION_NAME
        {
            get;
            set;
        }

        Int64? SPECIALITY_ID
        {
            get;
            set;
        }

        String SPECIALITY_NAME
        {
            get;
            set;
        }

        String SPECIALITY_NUMB
        {
            get;
            set;
        }

        Int64? QUALIFICATION_ID
        {
            get;
            set;
        }

        String QUALIFICATION_NAME
        {
            get;
            set;
        }

        Int64? STUDYEAR_ID
        {
            get;
            set;
        }

        String STUDYEAR_NAME
        {
            get;
            set;
        }

        String STUDYEAR_SHORTNAME
        {
            get;
            set;
        }

        Int32 STUDYEAR_NUM
        {
            get;
            set;
        }

        Int64? MODEEDUC_ID
        {
            get;
            set;
        }

        String MODEEDUC_NAME
        {
            get;
            set;
        }

        Int64? FORMEDUC_ID
        {
            get;
            set;
        }

        String FORMEDUC_NAME
        {
            get;
            set;
        }

        Int64? FACULTY_ID
        {
            get;
            set;
        }

        String FACULTY_FULLNAME
        {
            get;
            set;
        }

        String FACULTY_SHORTNAME
        {
            get;
            set;
        }


	}
}
