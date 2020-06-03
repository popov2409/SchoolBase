using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolBase.Model
{
    class GroupClass
    {
        public int Id { get; set; }

        /// <summary>
        /// Номер класса
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Буква
        /// </summary>
        public string Character
        { get; set; }

        /// <summary>
        /// Категория класса
        /// </summary>
        public string Category { get; set; }


        /// <summary>
        /// Статуc
        /// </summary>
        public string Status { get; set; }


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
        /// Смена, в которую учится класс
        /// </summary>
        public int Smena { get; set; }

        /// <summary>
        /// Школьники класса
        /// </summary>
        public List<Student> Students
        {
            get { return Proxy.StudentList.Where(st => st.Group == this).ToList(); }
        }
    }
}
