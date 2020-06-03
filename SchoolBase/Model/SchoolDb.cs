using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolBase.Model
{
    /// <summary>
    /// База школы
    /// </summary>
    public class SchoolDb
    {
        public SchoolDb()
        {
            Students=new List<Student>();
            Teachers=new List<Teacher>();
            SchoolClasses=new List<SchoolClass>();
            StatusSchoolClasses=new List<StatusSchoolClass>();
            CategorySchoolClasses=new List<CategorySchoolClass>();
            GroupSchoolClasses=new List<GroupSchoolClass>();
            Languages=new List<Language>();
        }

        /// <summary>
        /// Школьники
        /// </summary>
        public List<Student> Students { get; set; }

        /// <summary>
        /// Учителя
        /// </summary>
        public List<Teacher> Teachers { get; set; }

        /// <summary>
        /// Классы
        /// </summary>
        public List<SchoolClass> SchoolClasses { get; set; }

        /// <summary>
        /// статусы классов
        /// </summary>
        public List<StatusSchoolClass> StatusSchoolClasses { get; set; }

        /// <summary>
        /// Категории классо
        /// </summary>
        public List<CategorySchoolClass> CategorySchoolClasses { get; set; }

        /// <summary>
        /// Список подгрупп в классах
        /// </summary>
        public List<GroupSchoolClass> GroupSchoolClasses { get; set; }

        /// <summary>
        /// Список языков в школе
        /// </summary>
        public List<Language> Languages { get; set; }
    }
}
