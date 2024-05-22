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
            EmailTextBox.Text = CurrentUser.EMail;
        }


        public void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        public void EmailTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!InputRegExps.EmailLocaleRegEx().IsMatch(e.Text))
                e.Handled = true;
        }

        public void EditLoginBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginTextBox.IsReadOnly = false;
            EditLoginBttn.Visibility = Visibility.Collapsed;
            LoginDecision.Visibility = Visibility.Visible;
        }
        public void LoginAcceptBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text))
                MessageBox.Show("Введите логин", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
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
        public void LoginDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginTextBox.IsReadOnly = true;
            EditLoginBttn.Visibility = Visibility.Visible;
            LoginDecision.Visibility = Visibility.Collapsed;
            LoginTextBox.Text = CurrentUser.Login;
        }

        public void EditPhoneBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhoneTextBox.IsReadOnly = false;
            EditPhoneBttn.Visibility = Visibility.Collapsed;
            PhoneDecision.Visibility = Visibility.Visible;
        }
        public void PhoneAcceptBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!!InputRegExps.PhoneRegEx().IsMatch(PhoneTextBox.Text))
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
        public void PhoneDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhoneTextBox.IsReadOnly = true;
            EditPhoneBttn.Visibility = Visibility.Visible;
            PhoneDecision.Visibility = Visibility.Collapsed;
            PhoneTextBox.Text = CurrentUser.PhoneNumber;
        }

        public void EditEmailBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EmailTextBox.IsReadOnly = false;
            EditEmailBttn.Visibility = Visibility.Collapsed;
            EmailDecision.Visibility = Visibility.Visible;
        }
        public void EmailAcceptBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
        public void EmailDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EmailTextBox.IsReadOnly = true;
            EditEmailBttn.Visibility = Visibility.Visible;
            EmailDecision.Visibility = Visibility.Collapsed;
            EmailTextBox.Text = CurrentUser.EMail;
        }

        public void LogOutBttn_Click(object sender, RoutedEventArgs e)
            => NavigationService.Navigate(((MainWindow)Application.Current.MainWindow).AuthorisationPage);
        private void AccesNoSpaceInput(object sender, KeyEventArgs e)
        { if (e.Key == Key.Space) e.Handled = true; }
    }
}