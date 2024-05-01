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
        private MainWindow CurrentMainWindow { get; set; } = null!;

        public AuthorisationPage()
        {
            InitializeComponent();

            LoginTextBox.Text = "petrova_irina";
            PasswordTextBox.Text = "579irishka";
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentMainWindow = (MainWindow)Application.Current.MainWindow;
            ((DockPanel)CurrentMainWindow.FindName("MenuPanel")).Visibility = Visibility.Collapsed;
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
            => ((DockPanel)CurrentMainWindow.FindName("MenuPanel")).Visibility = Visibility.Visible;


        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginTextBox.Text = LoginTextBox.Text.Trim();
                PasswordTextBox.Text = PasswordTextBox.Text.Trim();
                string login = LoginTextBox.Text;
                string password = PasswordTextBox.Text;

                if (!Regex.IsMatch(login, @"^.{5}"))
                    MessageBox.Show("Неверный формат логина", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (!Regex.IsMatch(password, @"^.{7}"))
                    MessageBox.Show("Неверный формат пароля", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    Employee? loggingEmployee = SchedulerDbContext.DbContext.Employees.FirstOrDefault(c => c.Login == login && c.Password == password);
                    if (loggingEmployee != null)
                    {
                        CurrentUser.SetCurrentUser(loggingEmployee);
                        ((TextBlock)CurrentMainWindow.FindName("UserNameTextBlock")).Text = CurrentUser.Name;
                        ((DockPanel)CurrentMainWindow.FindName("MenuPanel")).Visibility = Visibility.Visible;
                        NavigationService.Navigate(CurrentMainWindow.MainSchedulePage);
                    }
                    else
                        MessageBox.Show("Аккаунт не найден. \nНеверные учётные данные или пользователь не зарегистрирован.", "Ошибка доступа!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } 
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void AccesNoSpaceInput(object sender, KeyEventArgs e)
            { if (e.Key == Key.Space) e.Handled = true; }
    }
}