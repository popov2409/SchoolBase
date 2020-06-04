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
using static System.Int32;

namespace SchoolBase.View.Quarter
{
    /// <summary>
    /// Логика взаимодействия для QuarterListView.xaml
    /// </summary>
    public partial class QuarterListView : Window
    {
        public QuarterListView()
        {
            InitializeComponent();
            MainGrid.ItemsSource = null;
            MainGrid.ItemsSource = DbProxy.SchoolDb.Quarters.OrderBy(c=>c.Number).OrderBy(c=>c.Year);
            InitializeComboboxes();
        }

        void InitializeComboboxes()
        {
            YearComboBox.Items.Clear();
            QuarterComboBox.Items.Clear();
            YearComboBox.Text = "";
            QuarterComboBox.Text = "";
            List<string> years = DbProxy.SchoolDb.Quarters.Select(c => c.Year).Distinct().OrderBy(c=>c).Select(c => c.ToString())
                .ToList();
            List<string> nums = DbProxy.SchoolDb.Quarters.Select(c => c.Number).Distinct().OrderBy(c=>c).Select(c => c.ToString())
                .ToList();

            foreach (string year in years)
            {
                YearComboBox.Items.Add(year);
            }

            foreach (string num in nums)
            {
                QuarterComboBox.Items.Add(num);
            }
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if((StartDateDatePicker.SelectedDate==null)||(EndDateDatePicker.SelectedDate==null))
            {
                MessageBox.Show("Не выбраны даты!");
                return;
            }

            if (StartDateDatePicker.SelectedDate > EndDateDatePicker.SelectedDate)
            {
                MessageBox.Show("Даты не корректны!");
                return;
            }

            if ((YearComboBox.Text.Trim().Length != 4) || (QuarterComboBox.Text.Trim().Length != 1))
            {
                MessageBox.Show("Не корректны год или четверть!");
            }

            if (DbProxy.SchoolDb.Quarters.Count(c => c.Number == Parse(QuarterComboBox.Text) && c.Year == Parse(YearComboBox.Text)) > 0)
            {
                MessageBox.Show("Данная четверь уже имеется в списке! \n Удалите из списка старое значение!");
                return;
            }

            DbProxy.SchoolDb.Quarters.Add(new Model.Quarter()
            {
                Id = Guid.NewGuid(), Year = Parse(YearComboBox.Text), Number = Parse(QuarterComboBox.Text),
                StartDate = StartDateDatePicker.Text, EndDate = EndDateDatePicker.Text
            });



            MainGrid.ItemsSource = null;
            MainGrid.ItemsSource = DbProxy.SchoolDb.Quarters.OrderBy(c => c.Number).OrderBy(c => c.Year);
            InitializeComboboxes();
            StartDateDatePicker.SelectedDate = null;
            EndDateDatePicker.SelectedDate = null;
        }

        private void QuarterListView_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete) return;
            DbProxy.SchoolDb.Quarters.Remove(MainGrid.SelectedItem as Model.Quarter);
            MainGrid.ItemsSource = null;
            MainGrid.ItemsSource = DbProxy.SchoolDb.Quarters.OrderBy(c => c.Number).OrderBy(c => c.Year);
            InitializeComboboxes();
        }

        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (YearComboBox.SelectedItem == null)
            {
                MainGrid.ItemsSource = null;
                MainGrid.ItemsSource = DbProxy.SchoolDb.Quarters.OrderBy(c => c.Number).OrderBy(c => c.Year);
                return;
            }
            MainGrid.ItemsSource = null;
            MainGrid.ItemsSource =
                DbProxy.SchoolDb.Quarters.Where(c => c.Year == int.Parse(YearComboBox.SelectedItem.ToString()));
        }
    }
}
