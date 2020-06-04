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
    /// Логика взаимодействия для StatusSchoolClassListView.xaml
    /// </summary>
    public partial class StatusSchoolClassListView : Window
    {
        public StatusSchoolClassListView()
        {
            InitializeComponent();
            InitializeListBox();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CategoryNameTextBlock.Text.Replace(" ", "").Length == 0) return;
            DbProxy.SchoolDb.StatusSchoolClasses.Add(new Model.StatusSchoolClass() { Id = Guid.NewGuid(), Value = CategoryNameTextBlock.Text });
            InitializeListBox();

            CategoryNameTextBlock.Text = "";
        }

        private void CategorySchoolClassListView_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete) return;
            DbProxy.SchoolDb.StatusSchoolClasses.Remove(MainListBox.SelectedItem as Model.StatusSchoolClass);
            InitializeListBox();
        }

        void InitializeListBox()
        {
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = DbProxy.SchoolDb.StatusSchoolClasses.OrderBy(c => c.Value);
        }
    }
}
