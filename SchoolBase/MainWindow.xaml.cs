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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SchoolBase.Model;
using SchoolBase.View.CategorySchoolClass;
using SchoolBase.View.ClassRoom;
using SchoolBase.View.Quarter;
using SchoolBase.View.Student;
using SchoolBase.View.Teacher;

namespace SchoolBase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DbProxy.LoadData();
            InitializeTreeView();
            MainGrid.ItemsSource = DbProxy.SchoolDb.Students.OrderBy(c=>c.FullName);
           // new TeacherListView().ShowDialog();
        }

        void InitializeTreeView()
        {
            MainTreeView.Items.Clear();
            foreach (CategorySchoolClass schoolDbCategorySchoolClass in DbProxy.SchoolDb.CategorySchoolClasses)
            {
                TreeViewItem item=new TreeViewItem(){Header = schoolDbCategorySchoolClass.Value};
                item.PreviewMouseUp += Item_PreviewMouseUp;
                foreach (SchoolClass schoolClass in DbProxy.SchoolDb.SchoolClasses.Where(c=>c.Category==schoolDbCategorySchoolClass.Id).OrderBy(c=>c.Character).OrderBy(c=>c.Number).ToList())
                {
                    TreeViewItem inItem=new TreeViewItem(){Header = schoolClass.FullValue};
                    inItem.PreviewMouseUp += InItem_PreviewMouseUp;

                    List<GroupSchoolClass> groupSchoolClasses =
                        DbProxy.SchoolDb.GroupSchoolClasses.Where(c => c.SchoolClass == schoolClass.Id).ToList();
                    foreach (GroupSchoolClass groupSchoolClass in groupSchoolClasses)
                    {
                        TreeViewItem ininItem = new TreeViewItem() { Header = groupSchoolClass.FullValue };
                        ininItem.PreviewMouseUp += IninItem_PreviewMouseUp;
                        inItem.Items.Add(ininItem);
                    }

                    item.Items.Add(inItem);
                }

                MainTreeView.Items.Add(item);
            }
        }

        private void InItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            SchoolClass sc= DbProxy.SchoolDb.SchoolClasses.FirstOrDefault(c =>
                c.FullValue == ((TreeViewItem)sender).Header.ToString());
            MainGrid.ItemsSource = sc.Students.OrderBy(c => c.FullName);
        }

        private void IninItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            SchoolClass sc = DbProxy.SchoolDb.SchoolClasses.First(c =>
                c.FullValue == ((TreeViewItem) ((TreeViewItem) sender).Parent).Header.ToString());
            GroupSchoolClass gsc =
                DbProxy.SchoolDb.GroupSchoolClasses.FirstOrDefault(c =>
                    c.FullValue == ((TreeViewItem) sender).Header.ToString() && c.SchoolClass == sc.Id);

            MainGrid.ItemsSource = gsc != null
                ? DbProxy.SchoolDb.Students.Where(c => c.GroupGuid == gsc.Id&&c.ClassRoom==sc.Id).OrderBy(c => c.FullName)
                : null;
        }

        /// <summary>
        /// Клик по категории классов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
        }

       

        private void Window_Closed(object sender, EventArgs e)
        {
            DbProxy.SaveData();
        }

        private void CategoryClassSchoolMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new CategorySchoolClassListView().ShowDialog();
            InitializeTreeView();
        }

        private void StatusClassSchoolMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new StatusSchoolClassListView().ShowDialog();
        }

        private void LanguageMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new LanguageListView().ShowDialog();
        }

        private void QuarterMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new QuarterListView().ShowDialog();
        }

        private void ClassSchoolMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new ClassSchoolListView().ShowDialog();
            InitializeTreeView();
        }

        private void TeacherMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new TeacherListView().ShowDialog();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainGrid.ItemsSource = DbProxy.SchoolDb.Students.Where(c=>c.FullName.ToLower().Contains(SearchTextBox.Text.ToLower()));
        }

        private void EditStudentButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (MainGrid.SelectedItem != null)
            {
                new AddStudentView(MainGrid.SelectedItem as Student).ShowDialog();
                MainGrid.ItemsSource = DbProxy.SchoolDb.Students.OrderBy(c => c.FullName);

            }
            else
            {
                MessageBox.Show("Выберите школьника!");
            }
        }

        private void AddStudentMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new AddStudentView(null).ShowDialog();
            MainGrid.ItemsSource = DbProxy.SchoolDb.Students.OrderBy(c => c.FullName);
        }
    }
}
