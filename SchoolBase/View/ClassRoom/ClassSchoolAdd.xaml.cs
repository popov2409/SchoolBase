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
    /// Логика взаимодействия для ClassSchoolAdd.xaml
    /// </summary>
    public partial class ClassSchoolAdd : Window
    {
        private Guid Id;
        private SchoolClass SchoolClass;
        private bool EditMode = false;

        private List<GroupSchoolClass> GroupSchoolClasses;
        public ClassSchoolAdd(Guid classId)
        {
            InitializeComponent();
            Id = classId;
            GroupSchoolClasses = new List<GroupSchoolClass>();
            InitializeCombobox();
            SchoolClass = DbProxy.SchoolDb.SchoolClasses.FirstOrDefault(c=>c.Id==classId);
            EditMode = SchoolClass != null;
            if(EditMode)SetFields();
        }

        void SetFields()
        {
            NumComboBox.Text = SchoolClass.Number.ToString();
            CharTextBlock.Text = SchoolClass.Character;
            CategoryComboBox.SelectedItem =
                DbProxy.SchoolDb.CategorySchoolClasses.FirstOrDefault(c => c.Id == SchoolClass.Category);
            StatusComboBox.SelectedItem =
                DbProxy.SchoolDb.StatusSchoolClasses.FirstOrDefault(c => c.Id == SchoolClass.Status);
            TeacherComboBox.Text = DbProxy.SchoolDb.Teachers.FirstOrDefault(c => c.Id == SchoolClass.Teacher)?.FullName;
            GroupSchoolClasses = DbProxy.SchoolDb.GroupSchoolClasses.Where(c => c.SchoolClass == SchoolClass.Id)
                .ToList();
            GroupListBox.ItemsSource = GroupSchoolClasses;
        }

        void InitializeCombobox()
        {
            CategoryComboBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses.OrderBy(c => c.Number);
            StatusComboBox.ItemsSource = DbProxy.SchoolDb.StatusSchoolClasses.OrderBy(c => c.Value);
          
            for (int i = 0; i < 11; i++)
            {
                NumComboBox.Items.Add(i + 1);
            }

            foreach (string teacher in DbProxy.SchoolDb.Teachers.OrderBy(c=>c.FullName).Select(c=>c.FullName))
            {
                TeacherComboBox.Items.Add(teacher);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NumGroupTextBox.Text.Length == 0)
            {
                MessageBox.Show("Укажите номер группы!");
            }

            GroupSchoolClass gr = new GroupSchoolClass()
            {
                Id = Guid.NewGuid(),
                SchoolClass = Id,
                Number = int.Parse(NumGroupTextBox.Text),
                Value = ValGroupTextBox.Text
            };
            GroupSchoolClasses.Add(gr);
            GroupListBox.ItemsSource = null;
            GroupListBox.ItemsSource = GroupSchoolClasses;
            NumGroupTextBox.Text = "";
            ValGroupTextBox.Text = "";
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if(GroupSchoolClasses.Count==0) return;
            GroupSchoolClass gsc = DbProxy.SchoolDb.GroupSchoolClasses.FirstOrDefault(c =>
                c.Id == GroupSchoolClasses[GroupSchoolClasses.Count - 1].Id);
            if (gsc!=null)
            {
                foreach (Model.Student student in DbProxy.SchoolDb.Students.Where(c=>c.GroupId==gsc.Id))
                {
                    student.GroupId = new Guid();
                }
            }
            DbProxy.SchoolDb.GroupSchoolClasses.Remove(gsc);
            GroupSchoolClasses.RemoveAt(GroupListBox.Items.Count-1);
            GroupListBox.ItemsSource = null;
            GroupListBox.ItemsSource = GroupSchoolClasses;
        }

        private void SaveButtom_OnClick(object sender, RoutedEventArgs e)
        {
            if (CategoryComboBox.SelectedIndex < 0 || StatusComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Не указана категория или статус класса!");
                return;
            }

            if (!EditMode)
            {
                SchoolClass=new SchoolClass(){Id = Guid.NewGuid()};
            }

            SchoolClass.Category = ((Model.CategorySchoolClass)CategoryComboBox.SelectedItem)?.Id ?? new Guid();
            SchoolClass.Status = ((StatusSchoolClass)StatusComboBox.SelectedItem)?.Id ?? new Guid();
            SchoolClass.Number = int.Parse(NumComboBox.Text);
            SchoolClass.Character = CharTextBlock.Text;

            if (TeacherComboBox.SelectedIndex < 0 && TeacherComboBox.Text.Length > 2)
            {
                Model.Teacher teacher = new Model.Teacher() { Id = Guid.NewGuid(), FullName = TeacherComboBox.Text };
                DbProxy.SchoolDb.Teachers.Add(teacher);
                SchoolClass.Teacher = teacher.Id;
            }
            else
            {
                SchoolClass.Teacher =
                    DbProxy.SchoolDb.Teachers.First(c => c.FullName.Equals(TeacherComboBox.Text)).Id;
            }

            foreach (GroupSchoolClass groupSchoolClass in GroupSchoolClasses)
            {
                if(DbProxy.SchoolDb.GroupSchoolClasses.Count(c=>c.Id==groupSchoolClass.Id)>0) continue;
                DbProxy.SchoolDb.GroupSchoolClasses.Add(groupSchoolClass);
            }

            this.Close();
        }

        private void CancelButtom_OnClick(object sender, RoutedEventArgs e)
        {
            SchoolClass = null;
            this.Close();
        }

        public Model.SchoolClass AddClass()
        {
            this.ShowDialog();
            return SchoolClass;
        }

    }
}
