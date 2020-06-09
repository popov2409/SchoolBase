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
        private bool editMode = false;
        public LanguageListView()
        {
            InitializeComponent();
            SetSourceMainListBox();
        }

        private void LanguageListView_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    DeleteLanguage(MainListBox.SelectedItem as Language);
                    SetSourceMainListBox();
                    break;
                case Key.Escape when editMode:
                    NameTextBlock.Text = "";
                    editMode = false;
                    AddButton.Content = "+";
                    break;
            }
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (NameTextBlock.Text.Replace(" ", "").Length == 0) return;
            if (!editMode)
            {
                DbProxy.SchoolDb.Languages.Add(new Model.Language() {Id = Guid.NewGuid(), Value = NameTextBlock.Text});
            }
            else
            {
                selLanguage.Value = NameTextBlock.Text;
                AddButton.Content = "+";
                editMode = false;
            }
            SetSourceMainListBox();
            NameTextBlock.Text = "";
        }

        void SetSourceMainListBox()
        {
            MainListBox.ItemsSource = null;
            MainListBox.ItemsSource = DbProxy.SchoolDb.Languages.OrderBy(c => c.Value); ;
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteLanguage(MainListBox.SelectedItem as Model.Language);
            SetSourceMainListBox();
        }

        private Language selLanguage;

        private void EditItem_OnClick(object sender, RoutedEventArgs e)
        {
            editMode = true;
            selLanguage=MainListBox.SelectedItem as Language;
            AddButton.Content = "V";
            NameTextBlock.Text = selLanguage.Value;
        }

        void DeleteLanguage(Language lang)
        {
            List<Model.Student> st = DbProxy.SchoolDb.Students.Where(c => c.FirstLanguage == lang.Id).ToList();
            foreach (Model.Student student in st)
            {
                student.FirstLanguage=new Guid();
            }
            st = DbProxy.SchoolDb.Students.Where(c => c.SecondLanguage == lang.Id).ToList();
            foreach (Model.Student student in st)
            {
                student.SecondLanguage = new Guid();
            }
            DbProxy.SchoolDb.Languages.Remove(lang);
        }
    }
}
