using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scheduler.Pages
{
    public partial class AuthorisationPage : Page
    {
        public AuthorisationPage()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            LoginTextBox.Text = LoginTextBox.Text.Trim();
            string login = LoginTextBox.Text;
            PasswordTextBox.Text = PasswordTextBox.Text.Trim();
            string password = PasswordTextBox.Text;
            if (!String.IsNullOrEmpty(login) && !String.IsNullOrEmpty(password))
            {
                Employee? _loggingEmployee = SchedulerDbContext.dbContext.Employees.FirstOrDefault(c => c.Login == login && c.Password == password);
                if (_loggingEmployee != null)
                {
                    CurrentUser.SetCurrentUser(_loggingEmployee);

                    ((TextBlock)Window.GetWindow(this).FindName("UserNameTextBlock")).Text = CurrentUser.Name;
                    ((DockPanel)Window.GetWindow(this).FindName("MenuPanel")).Visibility = Visibility.Visible;

                    NavigationService.Navigate(new MainSchedulePage());
                }
                else
                    MessageBox.Show("Аккаунт не найден. \nНеверные учётные данные или пользователь не зарегистрирован.", "Ошибка доступа!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegistrationLink_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            ((DockPanel)Window.GetWindow(this).FindName("MenuPanel")).Visibility = Visibility.Collapsed;
        }
    }
}
