using Scheduler.Services;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Scheduler.Models;
using System.Linq;

namespace Scheduler.Pages
{
    public partial class UserProfilePage : Page
    {
        public UserProfilePage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProfileNameTextBlock.Text = CurrentUser.Name;
            LoginTextBox.Text = CurrentUser.Login;
            PhoneTextBox.Text = CurrentUser.PhoneNumber;
            //TelegramIDTextBox.Text = CurrentUser.TelegramId.Replace("@", "");
            EmailTextBox.Text = CurrentUser.EMail;
        }

        private void AccesNoSpaceInput(object sender, KeyEventArgs e)
            { if (e.Key == Key.Space) e.Handled = true; }
        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (PhoneTextBox.CaretIndex == 0 &&
                e.Text == "7" &&
                char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                PhoneTextBox.Text = $"+7{PhoneTextBox.Text}";
                PhoneTextBox.SelectionStart = PhoneTextBox.Text.Length;
                PhoneTextBox.Focus();
            }
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }
        private void TelegramIDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^a-zA-Z0-9_]+");
            if (regex.IsMatch(e.Text))
                e.Handled = true;
        }
        private void EmailTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^@a-zA-Z0-9_\-\.]+");
            if (regex.IsMatch(e.Text))
                e.Handled = true;
        }


        private void EditLoginBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginTextBox.IsReadOnly = false;
            EditLoginBttn.Visibility = Visibility.Collapsed;
            LoginDecision.Visibility = Visibility.Visible;
        }
        private void LoginAcceptBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Regex.IsMatch(LoginTextBox.Text, @"^.{5}"))
                MessageBox.Show("Неверный формат логина", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                LoginTextBox.IsReadOnly = true;
                EditLoginBttn.Visibility = Visibility.Visible;
                LoginDecision.Visibility = Visibility.Collapsed;

                SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == CurrentUser.EmployeeId).Login = LoginTextBox.Text.Trim();
                SchedulerDbContext.DbContext.SaveChanges();
                CurrentUser.Login = LoginTextBox.Text;
            }
        }
        private void LoginDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginTextBox.IsReadOnly = true;
            EditLoginBttn.Visibility = Visibility.Visible;
            LoginDecision.Visibility = Visibility.Collapsed;
            LoginTextBox.Text = CurrentUser.Login;
        }

        private void EditPhoneBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhoneTextBox.IsReadOnly = false;
            EditPhoneBttn.Visibility = Visibility.Collapsed;
            PhoneDecision.Visibility = Visibility.Visible;
        }
        private void PhoneAcceptBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Regex.IsMatch(PhoneTextBox.Text, @"^(\+)?\d{11}$"))
                MessageBox.Show("Неверный формат тел. номера", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                PhoneTextBox.IsReadOnly = true;
                EditPhoneBttn.Visibility = Visibility.Visible;
                PhoneDecision.Visibility = Visibility.Collapsed;

                SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == CurrentUser.EmployeeId).PhoneNumber = PhoneTextBox.Text.Trim();
                SchedulerDbContext.DbContext.SaveChanges();
                CurrentUser.PhoneNumber = PhoneTextBox.Text;
            }
        }
        private void PhoneDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhoneTextBox.IsReadOnly = true;
            EditPhoneBttn.Visibility = Visibility.Visible;
            PhoneDecision.Visibility = Visibility.Collapsed;
            PhoneTextBox.Text = CurrentUser.PhoneNumber;
        }

        private void EditTgLinkBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TelegramIDTextBox.IsReadOnly = false;
            EditTgLinkBttn.Visibility = Visibility.Collapsed;
            TgLinkDecision.Visibility = Visibility.Visible;
        }
        private void TgLinkAcceptBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Regex.IsMatch(TelegramIDTextBox.Text, @"^.{5}"))
                MessageBox.Show("Неверный формат Telegram link", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                TelegramIDTextBox.IsReadOnly = true;
                EditTgLinkBttn.Visibility = Visibility.Visible;
                TgLinkDecision.Visibility = Visibility.Collapsed;

                //SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == CurrentUser.EmployeeId).TelegramId = TelegramIDTextBox.Text.Trim();
                SchedulerDbContext.DbContext.SaveChanges();
                //CurrentUser.TelegramId = TelegramIDTextBox.Text;
            }
        }
        private void TgLinkDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TelegramIDTextBox.IsReadOnly = true;
            EditTgLinkBttn.Visibility = Visibility.Visible;
            TgLinkDecision.Visibility = Visibility.Collapsed;
            //TelegramIDTextBox.Text = CurrentUser.TelegramId.Replace("@", "");
        }

        private void EditEmailBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EmailTextBox.IsReadOnly = false;
            EditEmailBttn.Visibility = Visibility.Collapsed;
            EmailDecision.Visibility = Visibility.Visible;
        }
        private void EmailAcceptBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailTextBox.Text) && !Regex.IsMatch(EmailTextBox.Text, @"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9]+$"))
                MessageBox.Show("Неверный формат эл.Почты", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                EmailTextBox.IsReadOnly = true;
                EditEmailBttn.Visibility = Visibility.Visible;
                EmailDecision.Visibility = Visibility.Collapsed;

                SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == CurrentUser.EmployeeId).EMail = EmailTextBox.Text.Trim();
                SchedulerDbContext.DbContext.SaveChanges();
                CurrentUser.EMail = EmailTextBox.Text;
            }
        }
        private void EmailDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EmailTextBox.IsReadOnly = true;
            EditEmailBttn.Visibility = Visibility.Visible;
            EmailDecision.Visibility = Visibility.Collapsed;
            EmailTextBox.Text = CurrentUser.EMail;
        }

        private void LogOutBttn_Click(object sender, RoutedEventArgs e)
            => NavigationService.Navigate(((MainWindow)Application.Current.MainWindow).AuthorisationPage);
    }
}