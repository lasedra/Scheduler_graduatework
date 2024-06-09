﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using Scheduler.Models;
using Scheduler.Pages;

namespace Scheduler
{
    public partial class MainWindow : Window
    {
        #region SetDarkTheme
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            HwndSource source = (HwndSource)PresentationSource.FromVisual(this);
            UseImmersiveDarkMode(source.Handle, true);
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        private static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
        {
            if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 17763))
            {
                var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (OperatingSystem.IsWindowsVersionAtLeast(10, 0, 18985))
                {
                    attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int useImmersiveDarkMode = enabled ? 1 : 0;
                return DwmSetWindowAttribute(handle, attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }
        #endregion

        public AuthorisationPage AuthorisationPage = new();

        public MainWindow()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += GlobalUnhandledExceptionHandler;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PagesFrame.Navigate(AuthorisationPage);
            StartDbWatcher();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
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

                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            MessageBox.Show("Обнаружены изменения в БД! Страница будет перезагружена");
                            Frame frame = (Frame)Application.Current.MainWindow.FindName("PagesFrame");
                            Page currentPage = frame.Content as Page;
                            if (currentPage != null)
                            {
                                Type type = currentPage.GetType();
                                Page newPage = (Page)Activator.CreateInstance(type);
                                frame.Content = newPage;
                            }

                        }));

                    }
                }));

            }).Start();
        }

        private void GlobalUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;

            string shortException = "Message: " + ex.Message
                             + "\nSource: " + ex.Source;

            string exception = "Message: " + ex.Message
                             + "\nBase exception: " + ex.GetBaseException()
                             + "\nInner exception: " + ex.InnerException
                             + "\nSource: " + ex.Source
                             + "\nStack trace: " + ex.StackTrace;

            MessageBox.Show(shortException, "Возникла непредвиденная ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UserNamePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PagesFrame.Navigate(new UserProfilePage(SchedulerDbContext.CurrentUser));
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            PagesFrame.Navigate(new MainSchedulePage());
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
        private void SubjectPageBttn_Click(object sender, RoutedEventArgs e)
        {
            PagesFrame.Navigate(new SubjectPage());
        }
    }
}
