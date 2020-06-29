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
    /// Логика взаимодействия для GroupEditView.xaml
    /// </summary>
    public partial class GroupEditView : Window
    {
        private Guid GroupId;
        public GroupEditView(Guid groupId)
        {
            InitializeComponent();
            GroupId = groupId;
            NumGroupTextBox.Text = DbProxy.SchoolDb.GroupSchoolClasses.First(c => c.Id == GroupId).Number.ToString();
            ValGroupTextBox.Text = DbProxy.SchoolDb.GroupSchoolClasses.First(c => c.Id == GroupId).Value.ToString();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            DbProxy.SchoolDb.GroupSchoolClasses.First(c => c.Id == GroupId).Number = int.Parse(NumGroupTextBox.Text);
            DbProxy.SchoolDb.GroupSchoolClasses.First(c => c.Id == GroupId).Value = ValGroupTextBox.Text;
            this.Close();
        }
    }
}
