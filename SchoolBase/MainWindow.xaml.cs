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
        }

        void InitializeTreeView()
        {
            MainTreeView.Items.Clear();
            foreach (CategorySchoolClass schoolDbCategorySchoolClass in DbProxy.SchoolDb.CategorySchoolClasses)
            {
                TreeViewItem item=new TreeViewItem(){Header = schoolDbCategorySchoolClass.Value};
                item.PreviewMouseUp += Item_PreviewMouseUp;
                foreach (SchoolClass schoolClass in DbProxy.SchoolDb.SchoolClasses.Where(c=>c.Category==schoolDbCategorySchoolClass.Id).ToList())
                {
                    TreeViewItem inItem=new TreeViewItem(){Header = schoolClass.FullValue};
                    inItem.PreviewMouseDoubleClick += InItem_PreviewMouseDoubleClick;
                }

                MainTreeView.Items.Add(item);
            }
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

        /// <summary>
        /// Клик по классу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
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
    }
}
