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
    /// Логика взаимодействия для AvailableReportView.xaml
    /// </summary>
    public partial class AvailableReportView : Window
    {
        public AvailableReportView()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        void InitializeComboBoxes()
        {
            foreach (int i in DbProxy.SchoolDb.Quarters.Select(c=>c.Year).Distinct().OrderByDescending(c=>c))
            {
                YearComboBox.Items.Add(i);
            }
        }

        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            QuarterComboBox.Items.Clear();
            foreach (int i in DbProxy.SchoolDb.Quarters.Where(c=>c.Year==int.Parse(YearComboBox.SelectedItem.ToString())).Select(c=>c.Number).OrderBy(c=>c))
            {
                QuarterComboBox.Items.Add(i);
            }
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateReportButton_OnClick(object sender, RoutedEventArgs e)
        {
            Reports.PrintAvailableReport(
                QuarterComboBox.SelectedIndex < 0 ? 0 : int.Parse(QuarterComboBox.SelectedItem.ToString()),
                int.Parse(YearComboBox.SelectedItem.ToString()));
        }
    }
}
