using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolBase.Model
{
    /// <summary>
    /// Ученик
    /// </summary>
    public class Student
    {
        public Guid Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Класс
        /// </summary>
        public Guid ClassRoom { get; set; }
        
        /// <summary>
        /// Группа класса в котором учится
        /// </summary>
        public Guid GroupGuid { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public string Birthdate { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// Номер личного дела
        /// </summary>
        public string PersonalFileNumber { get; set; }

        /// <summary>
        /// Дата поступления
        /// </summary>
        public string AvailableDate { get; set; }

        /// <summary>
        /// Дата убытия
        /// </summary>
        public string DismissalDate { get; set; }

        /// <summary>
        /// Первый иностранный язык
        /// </summary>
        public Guid FirstLanguage { get; set; }

        /// <summary>
        /// Получить название языка
        /// </summary>
        public string FirstLanguageName => DbProxy.SchoolDb.Languages.FirstOrDefault(c => c.Id == FirstLanguage) != null
            ? DbProxy.SchoolDb.Languages.FirstOrDefault(c => c.Id == FirstLanguage)?.Value
            : "";

        /// <summary>
        /// Второй иностранный язык
        /// </summary>
        public Guid SecondLanguage { get; set; }

        /// <summary>
        /// Получить название языка
        /// </summary>
        public string SecondLanguageName => DbProxy.SchoolDb.Languages.FirstOrDefault(c => c.Id == SecondLanguage) != null
            ? DbProxy.SchoolDb.Languages.FirstOrDefault(c => c.Id == SecondLanguage)?.Value
            : "";

        /// <summary>
        /// Откуда прибыл номер#область#город
        /// </summary>
        public string FromSchool { get; set; }

        /// <summary>
        /// Инвалидность
        /// </summary>
        public bool Invalidity { get; set; }

        /// <summary>
        /// Инклюзив(ВШУ)
        /// </summary>
        public bool Inclusive { get; set; }

        /// <summary>
        /// Приказ о зачислении дата
        /// </summary>
        public string EnrollmentDecree { get; set; }

        /// <summary>
        /// Приказ об отчислении дата
        /// </summary>
        public string DismissalDecree { get; set; }

        /// <summary>
        /// Надомное обучение
        /// </summary>
        public bool HomeSchooling { get; set; }

        /// <summary>
        /// Находится в архиве
        /// </summary>
        public bool IsArhive { get; set; }

        /// <summary>
        /// Класс при помещении в архив
        /// </summary>
        public string ArhivGroup { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string OtherText { get; set; }

        /// <summary>
        /// Условно переведен
        /// </summary>
        public bool ProbationTransferred { get; set; }

        public string ClassAndGroupName {
            get
            {
               SchoolClass sc= DbProxy.SchoolDb.SchoolClasses.FirstOrDefault(c => c.Id == ClassRoom);
               GroupSchoolClass gsc= DbProxy.SchoolDb.GroupSchoolClasses.FirstOrDefault(c => c.Id == GroupGuid);
               string res = "";
               res += sc != null ? sc.FullValue : "";
               res += gsc != null ? $"({gsc.FullValue})" : "";
               return res;
            }
        }
    }

    public class Teacher
    {
        public Guid Id;

        /// <summary>
        /// ФИО
        /// </summary>
        public string FullName { get; set; }
    }
}
