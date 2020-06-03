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
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = DbProxy.SchoolDb.Teachers;
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (FullNameTextBlock.Text.Replace(" ", "").Length == 0) return;
            DbProxy.SchoolDb.Teachers.Add(new Model.Teacher()
            {
                Id = Guid.NewGuid(), FullName = FullNameTextBlock.Text
            });
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = DbProxy.SchoolDb.Teachers;

        }

        private void FullNameTextName_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete) return;
            DbProxy.SchoolDb.Teachers.Remove(MainListBox.SelectedItem as Model.Teacher);
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = DbProxy.SchoolDb.Teachers;

        }
    }
}
