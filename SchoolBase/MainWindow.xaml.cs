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
using SchoolBase.View.Report;
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
            UpdateSourceMainGrid();
            //MainGrid.ItemsSource = DbProxy.SchoolDb.Students.OrderBy(c=>c.FullName);
           // new TeacherListView().ShowDialog();
        }

        /// <summary>
        /// Собираем дерево классов
        /// </summary>
        void InitializeTreeView()
        {
            MainTreeView.Items.Clear();
            foreach (CategorySchoolClass schoolDbCategorySchoolClass in DbProxy.SchoolDb.CategorySchoolClasses.OrderBy(c=>c.Number))
            {
                TreeViewItem categoryTreeViewItem=new TreeViewItem(){Header = schoolDbCategorySchoolClass.Value,Uid = schoolDbCategorySchoolClass.Id.ToString()};
                categoryTreeViewItem.PreviewMouseUp += CategoryTreeViewItem_PreviewMouseUp; ;
                foreach (SchoolClass schoolClass in DbProxy.SchoolDb.SchoolClasses.Where(c=>c.Category==schoolDbCategorySchoolClass.Id).OrderBy(c=>c.Character).OrderBy(c=>c.Number).ToList())
                {
                    TreeViewItem classTreeViewItem=new TreeViewItem(){Header = schoolClass.FullValue,Uid = schoolClass.Id.ToString()};
                    classTreeViewItem.PreviewMouseUp += ClassTreeViewItem_PreviewMouseUp; ;
                    ContextMenu classContextMenu=new ContextMenu();


                    MenuItem reportClassMenuItem=new MenuItem(){ Header = "Отчет", Uid = Uid = schoolClass.Id.ToString()};
                    reportClassMenuItem.Click += ReportClassMenuItem_Click;
                    classContextMenu.Items.Add(reportClassMenuItem);
                    classContextMenu.Items.Add(new Separator());

                    MenuItem editClassMenuItem=new MenuItem(){ Header = "Свойства", Uid = Uid = schoolClass.Id.ToString() };
                    editClassMenuItem.Click += EditClassMenuItem_Click;
                    classContextMenu.Items.Add(editClassMenuItem);

                    classTreeViewItem.ContextMenu = classContextMenu;

                    List<GroupSchoolClass> groupSchoolClasses =
                        DbProxy.SchoolDb.GroupSchoolClasses.Where(c => c.SchoolClass == schoolClass.Id).ToList();
                    foreach (GroupSchoolClass groupSchoolClass in groupSchoolClasses)
                    {
                        TreeViewItem grourTreeViewItem = new TreeViewItem() { Header = groupSchoolClass.FullValue,Uid = groupSchoolClass.Id.ToString()};
                        grourTreeViewItem.PreviewMouseUp += GrourTreeViewItem_PreviewMouseUp;
                        ContextMenu groupContextMenu=new ContextMenu();
                        MenuItem reportGroupMenuItem=new MenuItem(){Header = "Отчет", Uid = groupSchoolClass.Id.ToString()};
                        reportGroupMenuItem.Click += reportGroupMenuItem_Click;
                        groupContextMenu.Items.Add(reportGroupMenuItem);
                        grourTreeViewItem.ContextMenu = groupContextMenu;
                        classTreeViewItem.Items.Add(grourTreeViewItem);
                    }

                    categoryTreeViewItem.Items.Add(classTreeViewItem);
                }

                MainTreeView.Items.Add(categoryTreeViewItem);
            }
        }

        private void EditClassMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new ClassSchoolAdd(Guid.Parse(((MenuItem) sender).Uid)).ShowDialog();
        }

        private void ReportClassMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Reports.PrintClassReport(Guid.Parse(((MenuItem)sender).Uid));
        }

        private void GrourTreeViewItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            MainGrid.ItemsSource = DbProxy.SchoolDb.Students
                .Where(c => c.GroupId == Guid.Parse(((TreeViewItem)sender).Uid) && c.IsArhive == IsArhiveSearchCheckBox.IsChecked).OrderBy(c => c.FullName);
        }

        private void ClassTreeViewItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            MainGrid.ItemsSource = DbProxy.SchoolDb.Students
                .Where(c => c.ClassId == Guid.Parse(((TreeViewItem)sender).Uid) && c.IsArhive == IsArhiveSearchCheckBox.IsChecked).OrderBy(c => c.FullName);
        }

        private void CategoryTreeViewItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            MainGrid.ItemsSource = DbProxy.SchoolDb.Students
                .Where(c => c.CategoryId == Guid.Parse(((TreeViewItem)sender).Uid)&&c.IsArhive==IsArhiveSearchCheckBox.IsChecked).OrderBy(c => c.FullName);
        }

        private void reportGroupMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Reports.PrintGroupReport(Guid.Parse(((MenuItem) sender).Uid));
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
            MainGrid.ItemsSource = DbProxy.SchoolDb.Students.Where(c =>
                c.FullName.ToLower().Contains(SearchTextBox.Text.ToLower()) &&
                c.IsArhive == IsArhiveSearchCheckBox.IsChecked).OrderBy(c=>c.FullName);
        }

        private void EditStudentButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (MainGrid.SelectedItem != null)
            {
                new AddStudentView(MainGrid.SelectedItem as Student).ShowDialog();
                UpdateSourceMainGrid();

            }
            else
            {
                MessageBox.Show("Выберите школьника!");
            }
        }

        private void AddStudentMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new AddStudentView(null).ShowDialog();
            UpdateSourceMainGrid();
        }

        private void DeleteStudentButton_OnClick(object sender, RoutedEventArgs e)
        {
            var student = MainGrid.SelectedItem as Student;
            var resultMessage = MessageBox.Show("Вы действительно хотите удалить школьника? \n" +
                                                student.FullName, "", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (resultMessage == MessageBoxResult.Yes)
            {
                DbProxy.SchoolDb.Students.Remove(student);
                UpdateSourceMainGrid();
            }
        }

        private void ClassReportMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new ClassReportView().ShowDialog();
        }

        private void GroupReportMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            new GroupReportView().ShowDialog();

        }

        private void ToArhivStudentButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (MainGrid.SelectedItem != null)
            {
                var student = MainGrid.SelectedItem as Student;
                var resultMessage = MessageBox.Show("Вы действительно хотите пометить школьника архив? \n" +
                                                    student.FullName, "", MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (resultMessage == MessageBoxResult.Yes)
                {
                    student.IsArhive = true;
                    student.ArhivGroup = DbProxy.SchoolDb.SchoolClasses.First(c => c.Id == student.ClassId).FullValue;
                    student.ClassId = new Guid();
                    student.GroupId = new Guid();
                    student.CategoryId=new Guid();
                    UpdateSourceMainGrid();
                }
            }
            else
            {
                MessageBox.Show("Не выбран ученик!");
            }
        }

      void UpdateSourceMainGrid()
        {
            MainGrid.ItemsSource = DbProxy.SchoolDb.Students.Where(c => c.IsArhive == IsArhiveSearchCheckBox.IsChecked).OrderBy(c=>c.FullName);
        }


        private void IsArhiveSearchCheckBox_Click(object sender, RoutedEventArgs e)
        {
            UpdateSourceMainGrid();
        }

        private void MainGrid_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()+1).ToString();
        }

        private void ContingentReportMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Reports.PrintContingentReport();
        }
    }
}
