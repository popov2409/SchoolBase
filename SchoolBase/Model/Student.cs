using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolBase.Model
{
   public class Student 
   { 
     public int Id { get; set; }

    /// <summary>
    /// ФИО целиком
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Класс в котором учится
    /// </summary>
    public GroupClass Group { get; set; }

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
    public string FirstLanguage { get; set; }

    /// <summary>
    /// Второй иностранный язык
    /// </summary>
    public string SecondLanguage { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public string Adress { get; set; }

    /// <summary>
    /// Отец и его номер телефона через ?
    /// </summary>
    public string Father { get; set; }

    /// <summary>
    /// Мама и ее номер телефона через  ?
    /// </summary>
    public string Mather { get; set; }

    /// <summary>
    /// Откуда прибыл номер?область?город
    /// </summary>
    public string FromSchool { get; set; }

    /// <summary>
    /// Куда убыл номер?область?город
    /// </summary>
    public string ToSchool { get; set; }

    /// <summary>
    /// Инвалидность
    /// </summary>
    public bool Invalidity { get; set; }

    /// <summary>
    /// Инклюзив
    /// </summary>
    public bool Inclusive { get; set; }


    /// <summary>
    /// Приказ о зачислении номер?дата
    /// </summary>
    public string EnrollmentDecree { get; set; }

    /// <summary>
    /// Приказ об отчислении номер?дата
    /// </summary>
    public string DismissalDecree { get; set; }


    /// <summary>
    /// Похвальный лист
    /// </summary>
    public string Diplom { get; set; }

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
}
