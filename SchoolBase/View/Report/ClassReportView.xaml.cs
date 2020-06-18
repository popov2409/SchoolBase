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

namespace SchoolBase.View.Report
{
    /// <summary>
    /// Логика взаимодействия для ClassReportView.xaml
    /// </summary>
    public partial class ClassReportView : Window
    {
        public ClassReportView()
        {
            InitializeComponent();
            CategoryComboBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses.OrderBy(c => c.Number);
        }

        private void CategoryComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassComboBox.ItemsSource = DbProxy.SchoolDb.SchoolClasses.Where(c =>
                    c.Category == ((Model.CategorySchoolClass) CategoryComboBox.SelectedItem).Id)
                .OrderBy(c => c.Character)
                .ThenBy(c => c.Number);
        }

        private void CreateReportButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ClassComboBox.SelectedItem == null)
            {
                MessageBox.Show("Не выбран класс!");
                return;
            }

            Reports.PrintClassReport(((SchoolClass) ClassComboBox.SelectedItem).Id);
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
