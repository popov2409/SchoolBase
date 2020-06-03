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

namespace SchoolBase.View.CategorySchoolClass
{
    /// <summary>
    /// Логика взаимодействия для CategorySchoolClassListView.xaml
    /// </summary>
    public partial class CategorySchoolClassListView : Window
    {
        private Model.CategorySchoolClass selectedCategory;
        public CategorySchoolClassListView()
        {
            InitializeComponent();
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses;
        }

        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory=MainListBox.SelectedItem as Model.CategorySchoolClass;
        }

        private void AddCategoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CategoryNameTextBlock.Text.Replace(" ","").Length==0) return;
            DbProxy.SchoolDb.CategorySchoolClasses.Add(new Model.CategorySchoolClass(){Id = Guid.NewGuid(),Value = CategoryNameTextBlock.Text});
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses;
            
            CategoryNameTextBlock.Text = "";
        }

        private void CategorySchoolClassListView_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete) return;
            DbProxy.SchoolDb.CategorySchoolClasses.Remove(MainListBox.SelectedItem as Model.CategorySchoolClass);
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses;
        }
    }
}
