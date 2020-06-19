using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Word=Microsoft.Office.Interop.Word;
using System.Reflection;
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

    }
}
