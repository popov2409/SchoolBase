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
            MainListBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses.OrderBy(c=>c.Number);
        }

        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategory=MainListBox.SelectedItem as Model.CategorySchoolClass;
            //if (MainListBox.SelectedIndex == 0)
            //{
            //    ((MainListBox.SelectedItem as TextBlock).ContextMenu.Items[0] as MenuItem).Visibility =
            //        Visibility.Hidden;
            //}
            //else
            //{
            //    ((MainListBox.SelectedItem as TextBlock).ContextMenu.Items[0] as MenuItem).Visibility =
            //        Visibility.Visible;
            //}

            //if (MainListBox.SelectedIndex == MainListBox.Items.Count-1)
            //{
            //    ((MainListBox.SelectedItem as TextBlock).ContextMenu.Items[MainListBox.Items.Count - 1] as MenuItem).Visibility =
            //        Visibility.Hidden;
            //}
            //else
            //{
            //    ((MainListBox.SelectedItem as TextBlock).ContextMenu.Items[MainListBox.Items.Count - 1] as MenuItem).Visibility =
            //        Visibility.Visible;
            //}
        }

        private void AddCategoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CategoryNameTextBlock.Text.Replace(" ","").Length==0) return;
            DbProxy.SchoolDb.CategorySchoolClasses.Add(new Model.CategorySchoolClass()
            {
                Id = Guid.NewGuid(), Value = CategoryNameTextBlock.Text,
                Number = DbProxy.SchoolDb.CategorySchoolClasses.Select(c=>c.Number).Max()+1
            });
            MainListBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses.OrderBy(c=>c.Number);
            
            CategoryNameTextBlock.Text = "";
        }

        private void CategorySchoolClassListView_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete) return;
            DbProxy.SchoolDb.CategorySchoolClasses.Remove(MainListBox.SelectedItem as Model.CategorySchoolClass);
            MainListBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses.OrderBy(c=>c.Number);
        }

        private void UpMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Model.CategorySchoolClass csc=MainListBox.SelectedItem as Model.CategorySchoolClass;
            if(csc.Number==1) return;
            DbProxy.SchoolDb.CategorySchoolClasses.First(c=>c.Number==csc.Number-1).Number++;
            csc.Number--;
            MainListBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses.OrderBy(c => c.Number);
        }

        private void DeleteMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            DbProxy.SchoolDb.CategorySchoolClasses.Remove(MainListBox.SelectedItem as Model.CategorySchoolClass);
            MainListBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses.OrderBy(c => c.Number);
        }

        private void DownMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Model.CategorySchoolClass csc = MainListBox.SelectedItem as Model.CategorySchoolClass;
            if (csc.Number == DbProxy.SchoolDb.CategorySchoolClasses.Count) return;
            DbProxy.SchoolDb.CategorySchoolClasses.First(c => c.Number == csc.Number + 1).Number--;
            csc.Number++;
            MainListBox.ItemsSource = DbProxy.SchoolDb.CategorySchoolClasses.OrderBy(c => c.Number);
        }
    }
}
