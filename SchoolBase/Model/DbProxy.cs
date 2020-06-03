using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SchoolBase.Model
{
    public class DbProxy
    {
        /// <summary>
        /// База школы
        /// </summary>
        public static SchoolDb SchoolDb;


        public static void LoadData()
        {
            SchoolDb=new SchoolDb();
            XmlSerializer formatter = new XmlSerializer(typeof(SchoolDb));
            using (FileStream fs = new FileStream("data.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    SchoolDb = (SchoolDb)formatter.Deserialize(fs);

                }
                catch
                {
                    // ignored
                }
            }
        }

        public static void SaveData()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(SchoolDb));
            using (FileStream fs = new FileStream("data.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs,SchoolDb);
            }
        }
    }
}
