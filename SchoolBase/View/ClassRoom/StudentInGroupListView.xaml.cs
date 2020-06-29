using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SchoolBase.Model;

namespace SchoolBase.View.ClassRoom
{
    /// <summary>
    /// Логика взаимодействия для StudentInGroupListView.xaml
    /// </summary>
    public partial class StudentInGroupListView : Window
    {
        private Guid GroupId;
        private SchoolClass SchoolClass;

        private List<StudList> StudLists;
        public StudentInGroupListView(Guid groupId)
        {
            InitializeComponent();
            GroupId = groupId;
            GroupSchoolClass gsc = DbProxy.SchoolDb.GroupSchoolClasses.First(c => c.Id == GroupId);
            SchoolClass = DbProxy.SchoolDb.SchoolClasses.First(c => c.Id == gsc.SchoolClass);
            this.Title = $"{SchoolClass.FullValue}({gsc.FullValue})";
            InitializeList();
            
        }

        void InitializeList()
        {
            StudLists=new List<StudList>();
            foreach (Model.Student student in DbProxy.SchoolDb.Students.Where(c=>c.ClassId==SchoolClass.Id).OrderBy(c=>c.FullName))
            {
                StudLists.Add(new StudList() {Student = student, IsCheck = student.GroupId.Contains(GroupId)});
            }
            MainListBox.ItemsSource = StudLists;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (StudList studList in StudLists)
            {
                if (studList.IsCheck && !studList.Student.GroupId.Contains(GroupId))
                {
                    DbProxy.SchoolDb.Students.First(c => c.Id == studList.Student.Id).GroupId.Add(GroupId);
                }

                if (!studList.IsCheck && studList.Student.GroupId.Contains(GroupId))
                {
                    DbProxy.SchoolDb.Students.First(c => c.Id == studList.Student.Id).GroupId.Remove(GroupId);
                }
            }

            this.Close();
        }
    }

    public class StudList
    {
        public Model.Student Student { get; set; }
        public bool IsCheck { get; set; }
    }
}
