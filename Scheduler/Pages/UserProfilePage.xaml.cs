using Scheduler.Services;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Scheduler.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Scheduler.Pages
{
    public partial class UserProfilePage : Page
    {
        private Employee User { get; set; } = null!;

        public UserProfilePage(Employee user)
        {
            this.User = user;
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProfileNameTextBlock.Text = User.Name;
            NameTextBox.Text = User.Name;
            LoginTextBox.Text = User.Login;
            PasswordTextBox.Text = User.Password;
            EditRolePanel.Visibility = (SchedulerDbContext.CurrentUser.EmployeeId == User.EmployeeId) ? Visibility.Collapsed : Visibility.Visible;
            PhoneTextBox.Text = User.PhoneNumber;
            EmailTextBox.Text = User.EMail;

            DeleteUserBttn.Visibility = (SchedulerDbContext.CurrentUser.EmployeeId == User.EmployeeId) ? Visibility.Collapsed : Visibility.Visible;
            LogOutBttn.Visibility = (SchedulerDbContext.CurrentUser.EmployeeId == User.EmployeeId) ? Visibility.Visible : Visibility.Collapsed;
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
        private void AccesNoSpaceInput(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }


        private void EditNameBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NameTextBox.IsReadOnly = false;
            EditNameBttn.Visibility = Visibility.Collapsed;
            NameDecision.Visibility = Visibility.Visible;
        }
        private void NameAcceptBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
                MessageBox.Show("Введите имя", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                NameTextBox.IsReadOnly = true;
                EditNameBttn.Visibility = Visibility.Visible;
                NameDecision.Visibility = Visibility.Collapsed;

                SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == User.EmployeeId).Name = NameTextBox.Text.Trim();
                SchedulerDbContext.DbContext.SaveChanges();
            }
        }
        private void NameDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NameTextBox.IsReadOnly = true;
            EditNameBttn.Visibility = Visibility.Visible;
            NameDecision.Visibility = Visibility.Collapsed;
            NameTextBox.Text = User.Name;
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

                SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == User.EmployeeId).Login = LoginTextBox.Text.Trim();
                SchedulerDbContext.DbContext.SaveChanges();
            }
        }
        public void LoginDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginTextBox.IsReadOnly = true;
            EditLoginBttn.Visibility = Visibility.Visible;
            LoginDecision.Visibility = Visibility.Collapsed;
            LoginTextBox.Text = User.Login;
        }


        private void EditPasswordBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PasswordTextBox.IsReadOnly = false;
            EditPasswordBttn.Visibility = Visibility.Collapsed;
            PasswordDecision.Visibility = Visibility.Visible;
        }
        private void PasswordAcceptBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
                MessageBox.Show("Введите пароль", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                PasswordTextBox.IsReadOnly = true;
                EditPasswordBttn.Visibility = Visibility.Visible;
                PasswordDecision.Visibility = Visibility.Collapsed;

                SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == User.EmployeeId).Password = PasswordTextBox.Text.Trim();
                SchedulerDbContext.DbContext.SaveChanges();
            }
        }
        private void PasswordDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PasswordTextBox.IsReadOnly = true;
            EditPasswordBttn.Visibility = Visibility.Visible;
            PasswordDecision.Visibility = Visibility.Collapsed;
            PasswordTextBox.Text = User.Password;
        }


        public void EditPhoneBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhoneTextBox.IsReadOnly = false;
            EditPhoneBttn.Visibility = Visibility.Collapsed;
            PhoneDecision.Visibility = Visibility.Visible;
        }
        public void PhoneAcceptBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!InputRegExps.PhoneRegEx().IsMatch(PhoneTextBox.Text))
                MessageBox.Show("Неверный формат тел. номера", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                PhoneTextBox.IsReadOnly = true;
                EditPhoneBttn.Visibility = Visibility.Visible;
                PhoneDecision.Visibility = Visibility.Collapsed;

                SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == User.EmployeeId).PhoneNumber = PhoneTextBox.Text.Trim();
                SchedulerDbContext.DbContext.SaveChanges();
                SchedulerDbContext.CurrentUser.PhoneNumber = PhoneTextBox.Text;
            }
        }
        public void PhoneDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhoneTextBox.IsReadOnly = true;
            EditPhoneBttn.Visibility = Visibility.Visible;
            PhoneDecision.Visibility = Visibility.Collapsed;
            PhoneTextBox.Text = SchedulerDbContext.CurrentUser.PhoneNumber;
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

                SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == User.EmployeeId).EMail = EmailTextBox.Text.Trim();
                SchedulerDbContext.DbContext.SaveChanges();
                SchedulerDbContext.CurrentUser.EMail = EmailTextBox.Text;
            }
        }
        public void EmailDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EmailTextBox.IsReadOnly = true;
            EditEmailBttn.Visibility = Visibility.Visible;
            EmailDecision.Visibility = Visibility.Collapsed;
            EmailTextBox.Text = SchedulerDbContext.CurrentUser.EMail;
        }


        private void ManagerRadioBttn_Checked(object sender, RoutedEventArgs e)
        {
            User.Role = true;
            SchedulerDbContext.DbContext.SaveChanges();
        }
        private void TutorRadioBttn_Checked(object sender, RoutedEventArgs e)
        {
            User.Role = false;
            SchedulerDbContext.DbContext.SaveChanges();
        }


        public void LogOutBttn_Click(object sender, RoutedEventArgs e)
            => NavigationService.Navigate(((MainWindow)Application.Current.MainWindow).AuthorisationPage);
        private void DeleteUserBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить пользователя {User.Name} ?" +
                    $"\nЭто приведёт к удалению всей зависимой информации.",
                    "Минуточку",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    List<DailyScheduleBody> bodiesToUnbind = SchedulerDbContext.DbContext.DailyScheduleBodies.Where(c => c.EmployeeId == User.EmployeeId).ToList();
                    bodiesToUnbind.ForEach(c => {
                        c.Subject = null;
                        c.Employee = null;
                        c.CabinetNumber = null;
                    });
                    SchedulerDbContext.DbContext.SaveChanges();

                    // Работает только при наличии ограничения у связанных таблиц - "ON DELETE CASCADE"
                    SchedulerDbContext.DbContext.Employees
                        .Where(c => c.EmployeeId == User.EmployeeId)
                        .ExecuteDelete();

                    NavigationService.Navigate(new TutorAndSubjectPage());
                    MessageBox.Show("Пользователь успешно удалён!");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}