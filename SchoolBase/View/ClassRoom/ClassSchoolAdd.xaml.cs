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
            NumGroupTextBox.Text = (GroupSchoolClasses.Count + 1).ToString();
            InitializeCombobox();
            SchoolClass = DbProxy.SchoolDb.SchoolClasses.FirstOrDefault(c=>c.Id==classId);
            EditMode = SchoolClass != null;
            if(EditMode)SetFields();
        }

        void SetFields()
        {

        }

        void InitializeCombobox()
        {
            CategoryComboBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses;
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
            NumGroupTextBox.Text = (GroupSchoolClasses.Count + 1).ToString();
            ValGroupTextBox.Text = "";
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if(GroupSchoolClasses.Count==0) return;
            GroupSchoolClasses.RemoveAt(GroupListBox.Items.Count-1);
            GroupListBox.ItemsSource = null;
            GroupListBox.ItemsSource = GroupSchoolClasses;
            NumGroupTextBox.Text = (GroupSchoolClasses.Count + 1).ToString();
        }

        private void SaveButtom_OnClick(object sender, RoutedEventArgs e)
        {
            if (!EditMode)
            {
                if (GroupSchoolClasses.Count < 1)
                {
                    MessageBox.Show("Нет групп в классе!");
                    return;
                }

                if (CategoryComboBox.SelectedIndex < 0 || StatusComboBox.SelectedIndex < 0)
                {
                    MessageBox.Show("Не указана категория или статус класса!");
                    return;
                }

                DbProxy.SchoolDb.GroupSchoolClasses.AddRange(GroupSchoolClasses);

                SchoolClass = new SchoolClass()
                {
                    Id = Id,
                    Category = ((Model.CategorySchoolClass) CategoryComboBox.SelectedItem).Id,
                    Status = ((StatusSchoolClass) StatusComboBox.SelectedItem).Id,
                    Number = int.Parse(NumComboBox.Text),
                    Character = CharTextBlock.Text,
                };




                if (TeacherComboBox.SelectedIndex < 0)
                {
                    Model.Teacher teacher = new Model.Teacher() {Id = Guid.NewGuid(), FullName = TeacherComboBox.Text};
                    DbProxy.SchoolDb.Teachers.Add(teacher);
                    SchoolClass.Teacher = teacher.Id;
                }
                else
                {
                    SchoolClass.Teacher =
                        DbProxy.SchoolDb.Teachers.First(c => c.FullName.Equals(TeacherComboBox.Text)).Id;
                }
                DbProxy.SchoolDb.SchoolClasses.Add(SchoolClass);
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
