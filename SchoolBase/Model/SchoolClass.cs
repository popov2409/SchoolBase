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
        /// Буква
        /// </summary>
        public string Character { get; set; }

        /// <summary>
        /// Категория класса
        /// </summary>
        public CategorySchoolClass Category { get; set; }


        /// <summary>
        /// Статуc класса
        /// </summary>
        public StatusSchoolClass Status { get; set; }


        /// <summary>
        /// Целиковое значение класса
        /// </summary>
        public string FullValue
        {
            get
            {
                return String.Format("{0}{1}", Number.ToString(),
                    Character.ToString().ToUpper());
            }
        }

        /// <summary>
        /// Целиковое значение класса и руководитель
        /// </summary>
        public string FullValueTeacher
        {
            get
            {
                return !string.IsNullOrEmpty(Teacher)
                    ? String.Format("{0} К/Р - {1}", FullValue, Teacher)
                    : FullValue;
            }
        }

        /// <summary>
        /// Классный руководитель Id
        /// </summary>
        public string Teacher { get; set; }

        

        /// <summary>
        /// Школьники класса //Сделать нормальный метод выборки из коллекции школьников
        /// </summary>
        public List<Student> Students
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Категория класса(начальная школа/средняя школа/старшая школа)
    /// </summary>
    public class CategorySchoolClass
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// Статус класса(ОВЗ/общая и тд)
    /// </summary>
    public class StatusSchoolClass
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
