﻿using System;
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
    /// Логика взаимодействия для ClassSchoolListView.xaml
    /// </summary>
    public partial class ClassSchoolListView : Window
    {
        public ClassSchoolListView()
        {
            InitializeComponent();
            InitializeComboBoxes();
            InitializeListBox();
        }

        void InitializeComboBoxes()
        {
            //CategoryComboBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses;
            //StatusComboBox.ItemsSource = DbProxy.SchoolDb.StatusSchoolClasses.OrderBy(c=>c.Value);

            CategoryComboBox.Items.Add("Все");
            StatusComboBox.Items.Add("Все");
            CategoryComboBox.SelectedIndex = 0;
            StatusComboBox.SelectedIndex = 0;
            foreach (string val in DbProxy.SchoolDb.CategorySchoolClasses.OrderBy(c=>c.Number).Select(c => c.Value))
            {
                CategoryComboBox.Items.Add($"   {val}");
            }

            foreach (string val in DbProxy.SchoolDb.StatusSchoolClasses.Select(c => c.Value).OrderBy(c=>c))
            {
                StatusComboBox.Items.Add($"   {val}");
            }

        }

        private Model.CategorySchoolClass cat;
        private StatusSchoolClass st;
        void InitializeListBox()
        {
            MainListBox.ItemsSource = null;
            if ((cat == null) && (st == null))
            {
                MainListBox.ItemsSource =
                    DbProxy.SchoolDb.SchoolClasses.OrderBy(c => c.Character).OrderBy(c => c.Number);
            }

            if ((cat == null) && (st != null))
            {
                MainListBox.ItemsSource = DbProxy.SchoolDb.SchoolClasses.OrderBy(c => c.Character)
                    .OrderBy(c => c.Number).Where(c => c.Status == st.Id);
            }
            if ((cat != null) && (st == null))
            {
                MainListBox.ItemsSource = DbProxy.SchoolDb.SchoolClasses.OrderBy(c => c.Character)
                    .OrderBy(c => c.Number).Where(c => c.Category == cat.Id);
            }
            if ((cat != null) && (st != null))
            {
                MainListBox.ItemsSource = DbProxy.SchoolDb.SchoolClasses.OrderBy(c => c.Character)
                    .OrderBy(c => c.Number).Where(c => c.Status == st.Id && c.Category == cat.Id);
            }
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryComboBox.SelectedIndex == 0)
            {
                cat = null;
            }
            else
            {
                cat =
                    DbProxy.SchoolDb.CategorySchoolClasses.FirstOrDefault(c => c.Value.Contains(CategoryComboBox.SelectedValue.ToString().TrimStart()));
            }
            
            InitializeListBox();
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            st = StatusComboBox.SelectedIndex == 0 ? null : DbProxy.SchoolDb.StatusSchoolClasses.FirstOrDefault(c => c.Value.Contains(StatusComboBox.SelectedValue.ToString().TrimStart()));
            InitializeListBox();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            SchoolClass sc= new ClassSchoolAdd(Guid.NewGuid()).AddClass();
            if (sc != null)
            {
                InitializeListBox();
            }
        }

        private void EditClassItemMenu_OnClick(object sender, RoutedEventArgs e)
        {
            new ClassSchoolAdd(((SchoolClass) MainListBox.SelectedItem).Id).ShowDialog();
            InitializeListBox();
        }
    }
}
