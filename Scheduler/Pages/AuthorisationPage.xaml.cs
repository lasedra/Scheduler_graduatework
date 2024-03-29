using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            if (!Regex.IsMatch(login, @"^.{5}"))
                MessageBox.Show("Неверный формат логина", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (!Regex.IsMatch(password, @"^.{7}"))
                MessageBox.Show("Неверный формат пароля", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
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
        private void AccesNoSpaceInput(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void RegistrationLink_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //((DockPanel)Window.GetWindow(this).FindName("MenuPanel")).Visibility = Visibility.Collapsed;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            ///((DockPanel)Window.GetWindow(this).FindName("MenuPanel")).Visibility = Visibility.Visible;
        }
    }
}
