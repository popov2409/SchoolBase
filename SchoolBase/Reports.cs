using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word=Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Windows;
using SchoolBase.Model;

namespace SchoolBase
{
    public class Reports
    {

        public static void PrintClassReport(Guid classId)
        {
            SchoolClass sc = DbProxy.SchoolDb.SchoolClasses.First(c => c.Id == classId);
            Teacher tt = DbProxy.SchoolDb.Teachers.FirstOrDefault(c => c.Id == sc.Teacher);

            Word._Document doc = null;
            try
            {
                Word._Application app = new Word.Application();
                string source =System.IO.Directory.GetCurrentDirectory()+"\\dot\\ClassRoom.dotx";
                doc = app.Documents.Add(source);
                doc.Activate();
                Word.Find find = app.Selection.Find;
                find.Text = "#h1";
                find.Replacement.Text = sc.FullValue;
                Object replace = Word.WdReplace.wdReplaceOne;
                Object wrap = Word.WdFindWrap.wdFindContinue;
                Object missing = Type.Missing;
                find.Execute(FindText: Type.Missing,
                    MatchCase: false,
                    MatchWholeWord: false,
                    MatchWildcards: false,
                    MatchSoundsLike: missing,
                    MatchAllWordForms: false,
                    Forward: true,
                    Wrap: wrap,
                    Format: false,
                    ReplaceWith: missing, Replace: replace);
                find.Text = "#h2";
                find.Replacement.Text = tt?.FullName;
                find.Execute(FindText: Type.Missing,
                    MatchCase: false,
                    MatchWholeWord: false,
                    MatchWildcards: false,
                    MatchSoundsLike: missing,
                    MatchAllWordForms: false,
                    Forward: true,
                    Wrap: wrap,
                    Format: false,
                    ReplaceWith: missing, Replace: replace);
                

                Word.Table tlb = doc.Tables[1];
                int i = 2;
                foreach (Student student in DbProxy.SchoolDb.Students.Where(c => c.ClassId == classId).OrderBy(c => c.FullName))
                {
                    tlb.Rows[i].Cells[2].Range.InsertAfter(student.FullName);
                    tlb.Rows[i].Cells[3].Range.InsertAfter(student.Sex.Length>0?student.Sex:"");
                    tlb.Rows[i].Cells[4].Range.InsertAfter(student.Birthdate.Length > 2 ? student.Birthdate : "");
                    GroupSchoolClass gr =
                        DbProxy.SchoolDb.GroupSchoolClasses.FirstOrDefault(c => c.Id == student.GroupId);
                    tlb.Rows[i].Cells[5].Range.InsertAfter(gr!=null ? gr.Number.ToString() : "");
                    tlb.Rows[i].Cells[6].Range.InsertAfter(student.AvailableDate.Length>2 ? student.AvailableDate : "");
                    tlb.Rows[i].Cells[7].Range.InsertAfter(student.DismissalDate.Length > 2 ? student.DismissalDate : "");
                    i++;
                    tlb.Rows.Add();
                }
                tlb.Rows[i].Delete();
                app.Visible = true;

            }
            catch (Exception e)
            {
                doc.Close();
                doc = null;
                Console.WriteLine("Error!");
            }
        }

        public static void PrintGroupReport(Guid groupId)
        {
            GroupSchoolClass gsc = DbProxy.SchoolDb.GroupSchoolClasses.First(c => c.Id == groupId);
            SchoolClass sc = DbProxy.SchoolDb.SchoolClasses.First(c => c.Id == gsc.SchoolClass);
            Word._Document doc = null;
            try
            {
                Word._Application app = new Word.Application();
                string source = System.IO.Directory.GetCurrentDirectory() + "\\dot\\Group.dotx";
                doc = app.Documents.Add(source);
                doc.Activate();
                Word.Find find = app.Selection.Find;
                find.Text = "#h1";
                find.Replacement.Text = sc.FullValue;
                Object replace = Word.WdReplace.wdReplaceOne;
                Object wrap = Word.WdFindWrap.wdFindContinue;
                Object missing = Type.Missing;
                find.Execute(FindText: Type.Missing,
                    MatchCase: false,
                    MatchWholeWord: false,
                    MatchWildcards: false,
                    MatchSoundsLike: missing,
                    MatchAllWordForms: false,
                    Forward: true,
                    Wrap: wrap,
                    Format: false,
                    ReplaceWith: missing, Replace: replace);
                find.Text = "#h2";
                find.Replacement.Text = gsc?.FullValue;
                find.Execute(FindText: Type.Missing,
                    MatchCase: false,
                    MatchWholeWord: false,
                    MatchWildcards: false,
                    MatchSoundsLike: missing,
                    MatchAllWordForms: false,
                    Forward: true,
                    Wrap: wrap,
                    Format: false,
                    ReplaceWith: missing, Replace: replace);
                Word.Table tlb = doc.Tables[1];
                int i = 2;
                foreach (Student student in DbProxy.SchoolDb.Students.Where(c => c.GroupId == groupId).OrderBy(c => c.FullName))
                {
                    tlb.Rows[i].Cells[2].Range.InsertAfter(student.FullName);
                    i++;
                    tlb.Rows.Add();
                }
                tlb.Rows[i].Delete();
                app.Visible = true;

            }
            catch (Exception e)
            {
                doc.Close();
                doc = null;
                Console.WriteLine("Error!");
            }
        }

        public static void PrintContingentReport()
        {
            Word._Document doc = null;
            try
            {
                Word._Application app = new Word.Application();
                string source = System.IO.Directory.GetCurrentDirectory() + "\\dot\\Contingent.dotx";
                doc = app.Documents.Add(source);
                doc.Activate();
                Word.Table tlb = doc.Tables[1];
                int i = 2;
                int[] res = new int[8];
                foreach (SchoolClass schoolClass in DbProxy.SchoolDb.SchoolClasses.OrderBy(c=>c.Number).ThenBy(c=>c.Character))
                {
                    tlb.Rows[i].Cells[1].Range.InsertAfter(schoolClass.FullValue);
                    res[0]++;
                    List<Student> students = DbProxy.SchoolDb.Students.Where(c => c.ClassId == schoolClass.Id).ToList();
                    if (!students.Any())
                    {
                        i++;
                        tlb.Rows.Add();
                        continue;
                    }
                    tlb.Rows[i].Cells[2].Range.InsertAfter(students.Count().ToString());
                    res[1] += students.Count();

                    int count = students.Count(c => c.Sex.ToLower().Equals("ж"));
                    res[2] += count;
                    tlb.Rows[i].Cells[3].Range.InsertAfter(count>0?count.ToString():"");

                    count = students.Count(c => c.Sex.ToLower().Equals("м"));
                    res[3] += count;
                    tlb.Rows[i].Cells[4].Range.InsertAfter(count > 0 ? count.ToString() : "");
                    
                    count = students.Count(c => c.HomeSchooling);
                    res[4] += count;
                    tlb.Rows[i].Cells[5].Range.InsertAfter(count > 0 ? count.ToString() : "");

                    count = students.Count(c => c.Inclusive);
                    res[5] += count;
                    tlb.Rows[i].Cells[6].Range.InsertAfter(count > 0 ? count.ToString() : "");

                    count = students.Count(c => c.Invalidity);
                    res[6] += count;
                    tlb.Rows[i].Cells[7].Range.InsertAfter(count > 0 ? count.ToString() : "");
                    
                    count = students.Count(c => c.ProbationTransferred);
                    res[7] += count;
                    tlb.Rows[i].Cells[8].Range.InsertAfter(count > 0 ? count.ToString() : "");
                    
                    i++;
                    tlb.Rows.Add();

                }

                for (int j = 0; j < 8; j++)
                {
                    tlb.Rows[i].Cells[j + 1].Range.Bold = 1;
                    tlb.Rows[i].Cells[j + 1].Range.Font.Color = Word.WdColor.wdColorRed;
                    tlb.Rows[i].Cells[j+1].Range.InsertAfter(res[j] > 0 ? res[j].ToString() : "");
                }

               

                app.Visible = true;

            }
            catch (Exception e)
            {
                doc.Close();
                doc = null;
                Console.WriteLine("Error!");
            }
        }

        public static void PrintAvailableReport(int quater, int year)
        {
            List<Quarter> quarters = quater == 0
                ? DbProxy.SchoolDb.Quarters.Where(c => c.Year == year).ToList()
                : DbProxy.SchoolDb.Quarters.Where(c => c.Year == year && c.Number == quater).ToList();
            DateTime sd = quarters.Select(c => DateTime.Parse(c.StartDate)).Min();
            DateTime ed = quarters.Select(c => DateTime.Parse(c.EndDate)).Max();
            List<Student> students= DbProxy.SchoolDb.Students.Where(student => student.AvailableDate.Length > 2).Where(student => DateTime.Parse(student.AvailableDate) >= sd && DateTime.Parse(student.AvailableDate) <= ed).ToList();
            if (students.Count == 0)
            {
                MessageBox.Show("Поиск не дал результата!");
                return;
            }

            Word._Document doc = null;
            try
            {
                Word._Application app = new Word.Application();
                string source = System.IO.Directory.GetCurrentDirectory() + "\\dot\\Available.dotx";
                doc = app.Documents.Add(source);
                doc.Activate();
                Word.Find find = app.Selection.Find;
                find.Text = "#h1";
                find.Replacement.Text = quater==0?$"{year} учебный год":$"{quater} четверть {year} учебного года";
                Object replace = Word.WdReplace.wdReplaceOne;
                Object wrap = Word.WdFindWrap.wdFindContinue;
                Object missing = Type.Missing;
                find.Execute(FindText: Type.Missing,
                    MatchCase: false,
                    MatchWholeWord: false,
                    MatchWildcards: false,
                    MatchSoundsLike: missing,
                    MatchAllWordForms: false,
                    Forward: true,
                    Wrap: wrap,
                    Format: false,
                    ReplaceWith: missing, Replace: replace);
                Word.Table tlb = doc.Tables[1];
                int i = 3;
                foreach (Student student in students)
                {
                    tlb.Rows[i].Cells[2].Range.InsertAfter(DbProxy.SchoolDb.SchoolClasses.FirstOrDefault(c => c.Id == student.ClassId)?.FullValue);
                    tlb.Rows[i].Cells[3].Range.InsertAfter(student.FullName);
                    tlb.Rows[i].Cells[4].Range.InsertAfter(student.AvailableDate);
                    tlb.Rows[i].Cells[5].Range.InsertAfter(student.EnrollmentDecree);
                    tlb.Rows[i].Cells[6].Range.InsertAfter(student.FromSchool.Length > 3 ? student.FromSchool.Split('#')[0] : "");
                    tlb.Rows[i].Cells[7].Range.InsertAfter(student.FromSchool.Length > 3 ? student.FromSchool.Split('#')[2] : "");
                    i++;
                    tlb.Rows.Add();
                }

                tlb.Rows[i].Delete();

                app.Visible = true;

            }
            catch (Exception e)
            {
                doc.Close();
                doc = null;
                Console.WriteLine("Error!");
            }
        }

        public static void PrintDismissalReport()
        {
            Word._Document doc = null;
            try
            {
                Word._Application app = new Word.Application();
                string source = System.IO.Directory.GetCurrentDirectory() + "\\dot\\Dismissal.dotx";
                doc = app.Documents.Add(source);
                doc.Activate();
                Word.Table tlb = doc.Tables[1];
                int i = 2;

                foreach (Student student in DbProxy.SchoolDb.Students.Where(c => c.DismissalDate.Length > 2)
                    .OrderBy(c => c.Class.Number).ThenBy(c => c.Class.Character).ThenBy(c => c.FullName))
                {
                    tlb.Rows[i].Cells[2].Range.InsertAfter(student.Class.FullValue);
                    tlb.Rows[i].Cells[3].Range.InsertAfter(student.FullName);
                    tlb.Rows[i].Cells[4].Range.InsertAfter(student.DismissalDate);
                    i++;
                    tlb.Rows.Add();
                }
                tlb.Rows[i].Delete();

                app.Visible = true;
            }
            catch (Exception e)
            {
                doc.Close();
                doc = null;
                Console.WriteLine("Error!");
            }
        }

    }
}
