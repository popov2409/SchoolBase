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
            //new AddStudentView().ShowDialog();
        }

        void InitializeTreeView()
        {
            MainTreeView.Items.Clear();
            foreach (CategorySchoolClass schoolDbCategorySchoolClass in DbProxy.SchoolDb.CategorySchoolClasses)
            {
                TreeViewItem item=new TreeViewItem(){Header = schoolDbCategorySchoolClass.Value};
                foreach (SchoolClass schoolClass in DbProxy.SchoolDb.SchoolClasses.Where(c=>c.Category==schoolDbCategorySchoolClass.Id).ToList())
                {
                    TreeViewItem inItem=new TreeViewItem(){Header = schoolClass.FullValue};
                    inItem.PreviewMouseDoubleClick += InItem_PreviewMouseDoubleClick;
                }
            }
        }

        private void InItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DbProxy.SaveData();
        }
    }
}
