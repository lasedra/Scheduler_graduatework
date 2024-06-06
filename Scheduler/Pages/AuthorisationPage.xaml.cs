using Scheduler.Models;
using System;
using System.Linq;
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
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoginTextBox.Text = "admin";
            PasswordTextBox.Text = "admin";
            CurrentMainWindow = (MainWindow)Application.Current.MainWindow;
            ((DockPanel)CurrentMainWindow.FindName("MenuPanel")).Visibility = Visibility.Collapsed;
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            ((DockPanel)CurrentMainWindow.FindName("MenuPanel")).Visibility = Visibility.Visible;
        }


        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginTextBox.Text = LoginTextBox.Text.Trim();
                PasswordTextBox.Text = PasswordTextBox.Text.Trim();
                string login = LoginTextBox.Text;
                string password = PasswordTextBox.Text;

                if (string.IsNullOrEmpty(login))
                    throw new Exception("Введите логин");
                else if (string.IsNullOrEmpty(password))
                    throw new Exception("Введите пароль"); // TODO: Хеширование
                else
                {
                    Employee? loggingEmployee = SchedulerDbContext.DbContext.Employees.FirstOrDefault(c => c.Login == login && c.Password == password);
                    if (loggingEmployee != null)
                    {
                        SchedulerDbContext.CurrentUser = loggingEmployee;

                        ((TextBlock)CurrentMainWindow.FindName("UserNameTextBlock")).Text = SchedulerDbContext.CurrentUser.Name;
                        ((DockPanel)CurrentMainWindow.FindName("MenuPanel")).Visibility = Visibility.Visible;
                        NavigationService.Navigate(new MainSchedulePage());
                    }
                    else
                        throw new Exception("Аккаунт не найден. \nНеверные учётные данные или пользователь не зарегистрирован.");
                }
            } 
            catch(Exception ex)
            { 
                MessageBox.Show(ex.Message, "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
        }

        private void AccesNoSpaceInput(object sender, KeyEventArgs e)
        { 
            if (e.Key == Key.Space) e.Handled = true; 
        }
    }
}