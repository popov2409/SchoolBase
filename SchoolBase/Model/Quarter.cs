using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolBase.Model
{
    /// <summary>
    /// Четверти
    /// </summary>
    public class Quarter
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер четверти
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// начало четверти
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Конец четверти
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// Год
        /// </summary>
        public int Year { get; set; }
    }
}
