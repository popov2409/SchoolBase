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
            SchoolDb = new SchoolDb();
            XmlSerializer formatter = new XmlSerializer(typeof(SchoolDb));
            {
                try
                {
                    using (StreamReader fs = new StreamReader("data.xml", Encoding.Default))
                    {

                        SchoolDb = (SchoolDb) formatter.Deserialize(fs);
                    }
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
        }

        public static void SaveData()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(SchoolDb));
            using (StreamWriter fs = new StreamWriter("data.xml",false,Encoding.Default))
            {
                formatter.Serialize(fs,SchoolDb);
            }
        }
    }
}
