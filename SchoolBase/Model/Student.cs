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
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string FullName => $"{LastName} {FirstName} {MiddleName}";

        /// <summary>
        /// Класс в котором учится
        /// </summary>
        public Guid School { get; set; }

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
        /// Второй иностранный язык
        /// </summary>
        public Guid SecondLanguage { get; set; }

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
        public DateTime EnrollmentDecree { get; set; }

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
    }

    public class Teacher
    {
        public Guid Id;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// ФИО
        /// </summary>
        public string FullName => $"{LastName} {FirstName} {MiddleName}";
    }
}
