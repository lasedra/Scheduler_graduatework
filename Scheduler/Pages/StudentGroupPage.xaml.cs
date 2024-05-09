using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            else
                StudentsGroupsListView.ItemsSource = SchedulerDbContext.DbContext.StudentGroups.ToList();
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

                StudentGroupCodeTxtBox.Text = "";
                SpecializationTxtBox.Text = "";
                DoneWithGroupsBttn.Visibility = Visibility.Visible;
                StudentsGroupsListView.ItemsSource = SchedulerDbContext.DbContext.StudentGroups.ToList();
            }
        }

        private void DoneWithGroupsBttn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(((MainWindow)Application.Current.MainWindow).MainSchedulePage);
            ((DockPanel)((MainWindow)Application.Current.MainWindow).FindName("MenuPanel")).Visibility = Visibility.Visible;
        }
    }
}
