using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Scheduler.Models;
using Scheduler.Pages;

namespace Scheduler
{
    public partial class MainWindow : Window
    {
        public MainSchedulePage MainSchedulePage = new();
        public AuthorisationPage AuthorisationPage = new();
        public RegistrationPage RegistrationPage = new();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PagesFrame.Navigate(AuthorisationPage);
            StartDbWatcher();
        }


        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            PagesFrame.Navigate(new MainSchedulePage());
        }

        private void UserNamePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PagesFrame.Navigate(new UserProfilePage(SchedulerDbContext.CurrentUser));
        }

        private void StudentGroupPageBttn_Click(object sender, RoutedEventArgs e)
        {
            PagesFrame.Navigate(new StudentGroupPage());
        }

        private void TutionPageBttn_Click(object sender, RoutedEventArgs e)
        {
            PagesFrame.Navigate(new TutorAndSubjectPage());
        }

        private void CabinetPageBttn_Click(object sender, RoutedEventArgs e)
        {
            PagesFrame.Navigate(new CabinetPage());
        }


        public static void StartDbWatcher()
        {
            new Thread(() =>
            {
                SchedulerDbContext dbContext = new SchedulerDbContext();
                List<DailyScheduleBody> beforeUpdateState = dbContext.DailyScheduleBodies.ToList();
                List<DailyScheduleBody> afterUpdateState = null!;

                Parallel.Invoke(new Action(() =>
                {
                    while (true)
                    {
                        while (true)
                        {
                            afterUpdateState = dbContext.DailyScheduleBodies.ToList();
                            if (!beforeUpdateState.SequenceEqual(afterUpdateState))
                                break;
                        }
                        beforeUpdateState = afterUpdateState;
                        MessageBox.Show("Обнаружены изменения в БД");
                    }
                }));

            }).Start();
        }

    }
}
