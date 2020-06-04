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
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = DbProxy.SchoolDb.Languages;
        }

        private void LanguageListView_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete) return;
            DbProxy.SchoolDb.Languages.Remove(MainListBox.SelectedItem as Model.Language);
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = DbProxy.SchoolDb.Languages;
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (NameTextBlock.Text.Replace(" ", "").Length == 0) return;
            DbProxy.SchoolDb.Languages.Add(new Model.Language() { Id = Guid.NewGuid(), Value = NameTextBlock.Text });
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = DbProxy.SchoolDb.Languages;
            NameTextBlock.Text = "";
        }
    }
}
