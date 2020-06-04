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
    /// Логика взаимодействия для LanguageListView.xaml
    /// </summary>
    public partial class LanguageListView : Window
    {
        public LanguageListView()
        {
            InitializeComponent();
            SetSourceMainListBox();
        }

        private void LanguageListView_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete) return;
            DbProxy.SchoolDb.Languages.Remove(MainListBox.SelectedItem as Model.Language);
            SetSourceMainListBox();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (NameTextBlock.Text.Replace(" ", "").Length == 0) return;
            DbProxy.SchoolDb.Languages.Add(new Model.Language() { Id = Guid.NewGuid(), Value = NameTextBlock.Text });
            SetSourceMainListBox();
            NameTextBlock.Text = "";
        }

        void SetSourceMainListBox()
        {
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = DbProxy.SchoolDb.Languages.OrderBy(c => c.Value); ;
        }
    }
}
