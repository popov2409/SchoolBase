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
        public TeacherListView()
        {
            InitializeComponent();
            MainGrid.ItemsSource = null;
            MainGrid.ItemsSource = DbProxy.SchoolDb.Teachers;
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (FirstNameTextBox.Text.Replace(" ", "").Length == 0 ||
                LastNameTextBox.Text.Replace(" ", "").Length == 0 ||
                MiddleNameTextBox.Text.Replace(" ", "").Length == 0) return;
            DbProxy.SchoolDb.Teachers.Add(new Model.Teacher()
            {
                Id = Guid.NewGuid(), LastName = LastNameTextBox.Text, FirstName = FirstNameTextBox.Text,
                MiddleName = MiddleNameTextBox.Text
            });
            MainGrid.ItemsSource = null;
            MainGrid.ItemsSource = DbProxy.SchoolDb.Teachers;

        }
    }
}
