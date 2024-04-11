using Scheduler.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scheduler.Pages
{
    public partial class StudentGroupPage : Page
    {
        public StudentGroupPage(bool hideMenuBar = false)
        {
            InitializeComponent();
            StudentsGroupsListView.ItemsSource = SchedulerDbContext.dbContext.StudentGroups.ToList();
            if (hideMenuBar)
            {
                MessageBox.Show("Для начала работы, внестие данные о группах студентов","Необходимые данные!", MessageBoxButton.OK, MessageBoxImage.Warning);
                ((DockPanel)((MainWindow)Application.Current.MainWindow).FindName("MenuPanel")).Visibility = Visibility.Collapsed;
            }
        }

        private void AddNewGroupBttn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(StudentGroupCodeTxtBox.Text))
            {
                SchedulerDbContext.dbContext.StudentGroups.Add(new StudentGroup
                {
                    StudentGroupCode = StudentGroupCodeTxtBox.Text.Trim(),
                    Specialization = !String.IsNullOrEmpty(SpecializationTxtBox.Text.Trim()) ? SpecializationTxtBox.Text.Trim() : null
                });
                SchedulerDbContext.dbContext.SaveChanges();

                StudentGroupCodeTxtBox.Text = "";
                SpecializationTxtBox.Text = "";
                DoneWithGroupsBttn.Visibility = Visibility.Visible;
                StudentsGroupsListView.ItemsSource = SchedulerDbContext.dbContext.StudentGroups.ToList();
            }
        }

        private void DoneWithGroupsBttn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(((MainWindow)Application.Current.MainWindow).MainSchedulePage);
            ((DockPanel)((MainWindow)Application.Current.MainWindow).FindName("MenuPanel")).Visibility = Visibility.Visible;
        }
    }
}
