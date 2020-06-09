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

namespace SchoolBase.View.Teacher
{
    /// <summary>
    /// Логика взаимодействия для TeacherListView.xaml
    /// </summary>
    public partial class TeacherListView : Window
    {
        private bool editMode = false;
        public TeacherListView()
        {
            InitializeComponent();
            MainListBox.ItemsSource = DbProxy.SchoolDb.Teachers.OrderBy(c => c.FullName);
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (NameTextBlock.Text.Replace(" ", "").Length == 0) return;
            if (!editMode)
            {
                DbProxy.SchoolDb.Teachers.Add(new Model.Teacher()
                {
                    Id = Guid.NewGuid(),
                    FullName = NameTextBlock.Text
                });
            }
            else
            {
                selTeacher.FullName = NameTextBlock.Text;
                AddButton.Content = "+";
                editMode = false;
            }
            MainListBox.ItemsSource = DbProxy.SchoolDb.Teachers.OrderBy(c => c.FullName);
            NameTextBlock.Text = "";
        }

        private void FullNameTextName_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    DeleteTeacher(MainListBox.SelectedItem as Model.Teacher);
                    break;
                case Key.Escape when editMode:
                    NameTextBlock.Text = "";
                    editMode = false;
                    AddButton.Content = "+";
                    break;
            }

        }

        private Model.Teacher selTeacher;
        private void EditItem_OnClick(object sender, RoutedEventArgs e)
        {
            editMode = true;
            selTeacher = MainListBox.SelectedItem as Model.Teacher;
            AddButton.Content = "V";
            NameTextBlock.Text = selTeacher.FullName;
        }


        void DeleteTeacher(Model.Teacher teacher)
        {
            List<SchoolClass> classes = DbProxy.SchoolDb.SchoolClasses.Where(c => c.Teacher == teacher.Id).ToList();
            foreach (SchoolClass schoolClass in classes)
            {
                schoolClass.Teacher=new Guid();
            }
            DbProxy.SchoolDb.Teachers.Remove(teacher);
            MainListBox.ItemsSource = DbProxy.SchoolDb.Teachers.OrderBy(c => c.FullName);
        }

        private void DelItem_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteTeacher(MainListBox.SelectedItem as Model.Teacher);
        }
    }
}
