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

namespace SchoolBase.View.ClassRoom
{
    /// <summary>
    /// Логика взаимодействия для ClassSchoolAdd.xaml
    /// </summary>
    public partial class ClassSchoolAdd : Window
    {
        private Model.CategorySchoolClass categoryGuid;
        private StatusSchoolClass statusGuid;
        private Guid Id;

        private List<GroupSchoolClass> GroupSchoolClasses;
        public ClassSchoolAdd(Model.CategorySchoolClass categoryGuid, StatusSchoolClass statusGuid)
        {
            this.categoryGuid = categoryGuid;
            this.statusGuid = statusGuid;
            InitializeComponent();
            CategoryTextBlock.Text = categoryGuid.Value;
            StatusTextBlock.Text = statusGuid.Value;
            Id=Guid.NewGuid();
            GroupSchoolClasses = new List<GroupSchoolClass>();
            NumGroupTextBox.Text = (GroupSchoolClasses.Count + 1).ToString();
            InitializeCombobox();
        }

        void InitializeCombobox()
        {
           

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            GroupSchoolClass gr = new GroupSchoolClass()
            {
                Id = Guid.NewGuid(),
                SchoolClass = Id,
                Number = int.Parse(NumGroupTextBox.Text),
                Value = ValGroupTextBox.Text
            };
            GroupSchoolClasses.Add(gr);
            GroupListBox.ItemsSource = null;
            GroupListBox.ItemsSource = GroupSchoolClasses;
            NumGroupTextBox.Text = (GroupSchoolClasses.Count + 1).ToString();
            ValGroupTextBox.Text = "";
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if(GroupSchoolClasses.Count==0) return;
            GroupSchoolClasses.RemoveAt(GroupListBox.Items.Count-1);
            GroupListBox.ItemsSource = null;
            GroupListBox.ItemsSource = GroupSchoolClasses;
            NumGroupTextBox.Text = (GroupSchoolClasses.Count + 1).ToString();
        }
    }
}
