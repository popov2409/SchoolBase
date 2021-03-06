﻿using System;
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
using Microsoft.Win32;
using SchoolBase.Model;

namespace SchoolBase.View.Student
{
    /// <summary>
    /// Логика взаимодействия для AddStudentView.xaml
    /// </summary>
    public partial class AddStudentView : Window
    {
        private Model.Student student;

        public AddStudentView(Model.Student student)
        {
            InitializeComponent();
            this.student = student;
            InitializeBoxes();
            if (student!=null)
            {
                SetFields(student);
            }


        }

        void SetFields(Model.Student l_student)
        {
            //ФИО
            FullNameTextBox.Text = l_student.FullName;
            //Дата рождения
            if (l_student.Birthdate.Length > 0) Birthdate.SelectedDate = DateTime.Parse(l_student.Birthdate);
            //Пол
            SexComboBox.Text = l_student.Sex;
            //Личное дело
            PersonalFileNumberTextBox.Text = l_student.PersonalFileNumber;
            //инвалид
             InvalidityCheckBox.IsChecked = l_student.Invalidity;
            //Инклюзив
            InclusiveCheckBox.IsChecked = l_student.Inclusive;
            //надомное обучение
            HomeSchoolingCheckBox.IsChecked = l_student.HomeSchooling;
            //Другие данные
            OtherTextBox.Text = l_student.OtherText;
            //дата поступления в школу
            if (l_student.AvailableDate.Length > 0)
                AvailableDate.SelectedDate = DateTime.Parse(l_student.AvailableDate);
            //Дата убытия
            if (l_student.DismissalDate.Length > 0)  DismissalDate.SelectedDate = DateTime.Parse(l_student.DismissalDate);
            //Откуда прибыл
            FromSchoolNumberTextBox.Text= l_student.FromSchool.Split('#')[0];
            FromRegionComboBox.Text= l_student.FromSchool.Split('#')[1];
            FromSityComboBox.Text= l_student.FromSchool.Split('#')[2];
            //Приказ о зачислении
            if (l_student.EnrollmentDecree.Length > 0)
                EnrollmentDecreeDate.SelectedDate = DateTime.Parse(l_student.EnrollmentDecree);
            //Приказ об отчислении
            if (l_student.DismissalDecree.Length > 0)
                DismissalDecreeDate.SelectedDate = DateTime.Parse(l_student.DismissalDecree);
            //Переведен условно
            ConditionallyCheckBox.IsChecked = l_student.ProbationTransferred;
            //Основной язык
            Language fLang = DbProxy.SchoolDb.Languages.FirstOrDefault(c => c.Id == l_student.FirstLanguage);
            if (fLang != null) FirstLanguageComboBox.SelectedItem = fLang;
            //Второй язык
            fLang = DbProxy.SchoolDb.Languages.FirstOrDefault(c => c.Id == l_student.SecondLanguage);
            if (fLang != null) SecondLanguageComboBox.SelectedItem = fLang;
            //Класс и группа
            ClassComboBox.SelectedItem = DbProxy.SchoolDb.SchoolClasses.FirstOrDefault(c => c.Id == l_student.ClassId);
            //Файл
            FileNameTextBLock.Text = l_student.File;
            DelFilePathButton.Visibility = string.IsNullOrEmpty(l_student.File)
                ? Visibility.Collapsed
                : Visibility.Visible;


            //GroupComboBox.SelectedItem = DbProxy.SchoolDb.GroupSchoolClasses.FirstOrDefault(c => c.Id == l_student.GroupId);
            InitializeGroupsListBox();

        }

        void InitializeBoxes()
        {
            ClassComboBox.ItemsSource = DbProxy.SchoolDb.SchoolClasses.OrderBy(c=>c.Number).ThenBy(c=>c.Character);
            FirstLanguageComboBox.ItemsSource = DbProxy.SchoolDb.Languages;
            FromSityComboBox.ItemsSource = DbProxy.SchoolDb.Students.Select(c => c.FromSchool.Split('#')[2]).Distinct();
            FromRegionComboBox.ItemsSource= DbProxy.SchoolDb.Students.Select(c => c.FromSchool.Split('#')[1]).Distinct();
        }

        private void ClassComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GroupComboBox.ItemsSource = null;
            GroupComboBox.ItemsSource = ((SchoolClass) ClassComboBox.SelectedItem).GroupSchoolClasses;
            CategoryGroupTextBlock.Text =
                DbProxy.SchoolDb.CategorySchoolClasses.FirstOrDefault(c =>
                    c.Id == ((SchoolClass) ClassComboBox.SelectedItem).Category)
                    ?.Value;
            StatusGroupTextBlock.Text= DbProxy.SchoolDb.StatusSchoolClasses.FirstOrDefault(c =>
                c.Id == ((SchoolClass)ClassComboBox.SelectedItem).Status)
                ?.Value;

            TeacherTextBlock.Text= DbProxy.SchoolDb.Teachers.FirstOrDefault(c =>
                    c.Id == ((SchoolClass)ClassComboBox.SelectedItem).Teacher)
                ?.FullName;
        }

        private void FirstLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SecondLanguageComboBox.ItemsSource = DbProxy.SchoolDb.Languages
                .Where(c => c.Id != ((Language) FirstLanguageComboBox.SelectedItem).Id).OrderBy(c => c.Value);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FullNameTextBox.Text.Length < 3)
            {
                MessageBox.Show("Введите ФИО школьника!");
                return;
            }

            if (student == null)
            {
                student=new Model.Student(){Id = Guid.NewGuid(),GroupId = new List<Guid>()};
                SetDataStudent(student);
                DbProxy.SchoolDb.Students.Add(student);
            }
            else
            {
                SetDataStudent(DbProxy.SchoolDb.Students.First(c => c.Id == student.Id));
            }
            DbProxy.SaveData();
            this.Close();

        }



        /// <summary>
        /// Создает нового ученика
        /// </summary>
        /// <returns></returns>
        private Model.Student SetDataStudent(Model.Student l_student)
        {
            //ФИО
            l_student.FullName = FullNameTextBox.Text;
            //Дата рождения
            l_student.Birthdate =
                Birthdate.SelectedDate != null ? Birthdate.SelectedDate.Value.ToShortDateString() : "";
            //Класс
            SchoolClass sc = ClassComboBox.SelectedIndex < 0 ? null : (SchoolClass) ClassComboBox.SelectedItem;
            l_student.ClassId = sc?.Id ?? new Guid();
            //Категория
            l_student.CategoryId = sc?.Category ?? new Guid(); 
            //Группа в классе
            //l_student.GroupId = GroupComboBox.SelectedIndex<0?new Guid(): ((GroupSchoolClass)GroupComboBox.SelectedItem).Id;
            //Пол
            l_student.Sex = SexComboBox.Text;
            //дата поступления в школу
            l_student.AvailableDate = AvailableDate.SelectedDate != null
                ? AvailableDate.SelectedDate.Value.ToShortDateString()
                : "";
            //Личное дело
            l_student.PersonalFileNumber = PersonalFileNumberTextBox.Text;
            //Откуда прибыл
            l_student.FromSchool = $"{FromSchoolNumberTextBox.Text}#{FromRegionComboBox.Text}#{FromSityComboBox.Text}";
            //Основной язык
            l_student.FirstLanguage = ((Language) FirstLanguageComboBox.SelectedItem)?.Id ?? new Guid();
            //Второй язык
            l_student.SecondLanguage = ((Language)SecondLanguageComboBox.SelectedItem)?.Id ?? new Guid();
            //инвалид
            l_student.Invalidity = InvalidityCheckBox.IsChecked == true;
            //Инклюзив
            l_student.Inclusive = InclusiveCheckBox.IsChecked == true;
            //надомное обучение
            l_student.HomeSchooling = HomeSchoolingCheckBox.IsChecked == true;
            //Приказ о зачислении
            l_student.EnrollmentDecree = EnrollmentDecreeDate.SelectedDate != null
                ? EnrollmentDecreeDate.SelectedDate.Value.ToShortDateString()
                : "";
            //Приказ об отчислении
            l_student.DismissalDecree = DismissalDecreeDate.SelectedDate != null
                ? DismissalDecreeDate.SelectedDate.Value.ToShortDateString()
                : "";
            //Переведен условно
            l_student.ProbationTransferred = ConditionallyCheckBox.IsChecked == true;
            //Дата убытия
            l_student.DismissalDate = DismissalDate.SelectedDate != null
                ? DismissalDate.SelectedDate.Value.ToShortDateString()
                : "";
            //Путь к личному делу
            l_student.File = FileNameTextBLock.Text;
            //Другие данные
            l_student.OtherText = OtherTextBox.Text;

            return l_student;
        }

        private void AddInGroupButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (GroupComboBox.SelectedItem==null)
            {
                MessageBox.Show("Не выбрана группа!");
                return;
            }
            GroupSchoolClass gsc= GroupComboBox.SelectedItem as GroupSchoolClass;

            if (student.GroupId.Contains(gsc.Id))
            {
                MessageBox.Show("Группа уже имеется в списке!");
                return;
            }

            student.GroupId.Add(gsc.Id);
            InitializeGroupsListBox();
        }

        void InitializeGroupsListBox()
        {
            GroupsListBox.ItemsSource = student.GroupId.Select(guid => DbProxy.SchoolDb.GroupSchoolClasses.First(c => c.Id == guid)).OrderBy(c=>c.Number).ToList();
        }

        private void RemoveInGroupMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            student.GroupId.Remove(((GroupSchoolClass) GroupsListBox.SelectedItem).Id);
            InitializeGroupsListBox();
        }

        private void FileButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = System.IO.Directory.GetCurrentDirectory()+"\\files";
            if (ofd.ShowDialog() == true)
            {
                FileNameTextBLock.Text = ofd.SafeFileName;
                DelFilePathButton.Visibility = Visibility.Visible;

            }
        }

        private void DelFilePathButton_OnClick(object sender, RoutedEventArgs e)
        {
            FileNameTextBLock.Text = "";
            DelFilePathButton.Visibility = Visibility.Collapsed;
        }
    }
}
