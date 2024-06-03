using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Scheduler.Pages
{
    public partial class StudentGroupPage : Page
    {
        private bool IsGettingStarted { get; set; } = false;
        private MainWindow CurrentMainWindow { get; set; } = null!;

        public StudentGroupPage(bool isGettingStarted = false)
        {
            InitializeComponent();
            IsGettingStarted = isGettingStarted;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentMainWindow = (MainWindow)Application.Current.MainWindow;
            if (IsGettingStarted)
            {
                MessageBox.Show("Для начала работы, внестие данные о группах студентов", "Минуточку!", MessageBoxButton.OK, MessageBoxImage.Warning);
                ((DockPanel)CurrentMainWindow.FindName("MenuPanel")).Visibility = Visibility.Collapsed;
            }
            else {    }

            StudentsGroupsListView.ItemsSource = SchedulerDbContext.DbContext.StudentGroups.ToList();
            StudentsGroupsListView.SelectedIndex = 0;
            UpdateStudyingListView();

            AddStudyingRowComboBox.ItemsSource = SchedulerDbContext.DbContext.Subjects.ToList();
        }

        private void UpdateStudyingListView()
        {
            List<Studying> studyingList = SchedulerDbContext.DbContext.Studyings.Include(c => c.Subject).Where(c => c.StudentGroupCode == ((StudentGroup)StudentsGroupsListView.SelectedItem).StudentGroupCode).ToList();
            if (studyingList.Count <= 0)
                studyingList = new List<Studying>() { new() { Subject = new() { Name = "(Отсутствуют)" } } };
            StudyingListView.ItemsSource = studyingList;
        }

        private void StudentsGroupsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStudyingListView();
        }

        private void AddNewGroupBttn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(StudentGroupCodeTxtBox.Text))
            {
                SchedulerDbContext.DbContext.StudentGroups.Add(new StudentGroup
                {
                    StudentGroupCode = StudentGroupCodeTxtBox.Text.Trim(),
                    Specialization = !String.IsNullOrEmpty(SpecializationTxtBox.Text.Trim()) ? SpecializationTxtBox.Text.Trim() : null
                });
                SchedulerDbContext.DbContext.SaveChanges();

                ScheduleController scheduleController = new();
                scheduleController.CreatePivotSchedule(scheduleController.CurrentWeek.WeekStart, StudentGroupCodeTxtBox.Text.Trim());
                SchedulerDbContext.DbContext.SaveChanges();

                StudentGroupCodeTxtBox.Text = string.Empty;
                SpecializationTxtBox.Text = string.Empty;
                StudentsGroupsListView.ItemsSource = SchedulerDbContext.DbContext.StudentGroups.ToList();
            }
        }

        private void AddStudyingRowBttn_Click(object sender, RoutedEventArgs e)
        {
            if(AddStudyingRowComboBox.SelectedItem != null &&
                StudentsGroupsListView.SelectedItem != null)
            {
                SchedulerDbContext.DbContext.Studyings.Add(new Studying()
                {
                    StudentGroupCode = ((StudentGroup)StudentsGroupsListView.SelectedItem).StudentGroupCode,
                    SubjectId = ((Subject)AddStudyingRowComboBox.SelectedItem).SubjectId
                });
                SchedulerDbContext.DbContext.SaveChanges();

                AddStudyingRowComboBox.SelectedItem = null;
                UpdateStudyingListView();
            }
        }

        private void DeleteGroupBttn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditGroupBttn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelEditRowBttn_Click(object sender, RoutedEventArgs e)
        {
            StudentGroupCodeTxtBox.Text = string.Empty;
            SpecializationTxtBox.Text = string.Empty;

            AddNewGroupBttn.Visibility = Visibility.Visible;
            EditGroupRowStackPanel.Visibility = Visibility.Collapsed;
        }

        private void EditGroupRow_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem listViewItem = FindParent<ListViewItem>((Button)sender);
            listViewItem.IsSelected = true;

            StudentGroup groupToEdit = (StudentGroup)StudentsGroupsListView.SelectedItem;
            StudentGroupCodeTxtBox.Text = groupToEdit.StudentGroupCode;
            SpecializationTxtBox.Text = groupToEdit?.Specialization;

            AddNewGroupBttn.Visibility = Visibility.Collapsed;
            EditGroupRowStackPanel.Visibility = Visibility.Visible;
        }
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) 
                return null;

            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }
    }
}
