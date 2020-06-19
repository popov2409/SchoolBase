using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolBase.Model
{
    /// <summary>
    /// Класс
    /// </summary>
    public class SchoolClass
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Номер класса
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Буква(или символы)
        /// </summary>
        public string Character { get; set; }

        public List<GroupSchoolClass> GroupSchoolClasses =>
            DbProxy.SchoolDb.GroupSchoolClasses.Where(c => c.SchoolClass == this.Id).ToList();

        /// <summary>
        /// Категория класса
        /// </summary>
        public Guid Category { get; set; }

        /// <summary>
        /// Статуc класса
        /// </summary>
        public Guid Status { get; set; }

        /// <summary>
        /// Целиковое значение класса
        /// </summary>
        public string FullValue => $"{Number}-{Character}";

        /// <summary>
        /// Целиковое значение класса и руководитель
        /// </summary>
        public string FullValueTeacher
        {
            get
            {
                Teacher teacher = DbProxy.SchoolDb.Teachers.FirstOrDefault(c => c.Id == Teacher);

                return teacher != null &&teacher.FullName.Length>2 ? $"{FullValue} К/Р - {teacher.FullName}"
                    : FullValue;
            }
        }

        /// <summary>
        /// Классный руководитель
        /// </summary>
        public Guid Teacher { get; set; }

        /// <summary>
        /// Школьники класса //Сделать нормальный метод выборки из коллекции школьников
        /// </summary>
        public List<Student> Students => DbProxy.SchoolDb.Students.Where(c => c.ClassId == this.Id).ToList();

    }


    /// <summary>
    /// Категория класса(начальная школа/средняя школа/старшая школа)
    /// </summary>
    public class CategorySchoolClass
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование категории
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Для корректной организации порядка списка
        /// </summary>
        public int Number { get; set; }
    }

    /// <summary>
    /// Статус класса(ОВЗ/общая и тд)
    /// </summary>
    public class StatusSchoolClass
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование статуса
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// Группа внутри класса
    /// </summary>
    public class GroupSchoolClass
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Номер группы в классе
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Наименование статуса
        /// </summary>
        public string Value { get; set; }

        public string FullValue => Value.Length > 0 ? $"{Number}-{Value}" : Number.ToString();
        

        /// <summary>
        /// Класс
        /// </summary>
        public Guid SchoolClass { get; set; }
    }

    /// <summary>
    /// Изучаемые языки
    /// </summary>
    public class Language
    {
        public Guid Id;
        public string Value { get; set; }
    }
}
