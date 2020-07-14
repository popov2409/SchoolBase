using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows;

namespace SchoolBase.Model
{
    public class DbProxy
    {
        /// <summary>
        /// База школы
        /// </summary>
        public static SchoolDb SchoolDb;

        static DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\files");

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
            
            CreateFileFolders();
        }

        /// <summary>
        /// Создание структуры каталогов
        /// </summary>
        static void CreateFileFolders()
        {
            foreach (CategorySchoolClass categorySchoolClass in SchoolDb.CategorySchoolClasses)
            {
                List<string> categoryFolders = dir.GetDirectories().Select(c => c.Name).ToList();
                string categoryDirName = $"{categorySchoolClass.Value}#{categorySchoolClass.Id}";
                if (!categoryFolders.Contains(categoryDirName))
                {
                    if (categoryFolders.Select(c => Guid.Parse(c.Split('#')[1])).ToList().Contains(categorySchoolClass.Id))
                    {
                        MessageBox.Show("Найдена папка!");
                    }
                    else
                    {
                        dir.CreateSubdirectory(categoryDirName);
                    }
                }
                DirectoryInfo categoryDir = new DirectoryInfo(dir.FullName + "\\" + categoryDirName);
                foreach (SchoolClass schoolClass in DbProxy.SchoolDb.SchoolClasses.Where(c=>c.Category==categorySchoolClass.Id))
                {
                    string classDirName = $"{schoolClass.FullValue}#{schoolClass.Id}";
                    List<string> classFolders = categoryDir.GetDirectories().Select(c => c.Name).ToList();
                    if (!classFolders.Contains(classDirName))
                    {
                        if (classFolders.Select(c => Guid.Parse(c.Split('#')[1])).ToList().Contains(schoolClass.Id))
                        {
                            string sd = classFolders.First(c => Guid.Parse(c.Split('#')[1]) == schoolClass.Id);
                            DirectoryInfo di=new DirectoryInfo(categoryDir.FullName+"\\"+sd);
                            di.MoveTo(categoryDir.FullName + "\\" + classDirName);
                        }
                        else
                        {
                            categoryDir.CreateSubdirectory(classDirName);
                        }
                    }
                }

            }
        }
    }
}
