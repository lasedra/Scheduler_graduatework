using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Scheduler.Pages
{
    public partial class RegistrationPage : Page
    {
        //TODO: Решить проблему Ctrl-C + Ctrl-V для недопустимых символов

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void CreateAccountBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NameTextBox.Text = NameTextBox.Text.Trim();

                if (string.IsNullOrEmpty(NameTextBox.Text))
                    throw new Exception("Введите имя");

                else if (!InputRegExps.PhoneRegEx().IsMatch(PhoneTextBox.Text))
                    throw new Exception("Неверный формат тел. номера");

                else if (!string.IsNullOrEmpty(EmailTextBox.Text) && !InputRegExps.EmailRegEx().IsMatch(EmailTextBox.Text))
                    throw new Exception("Неверный формат эл.Почты");

                else if (string.IsNullOrEmpty(LoginTextBox.Text))
                    throw new Exception("Введите логин");

                else if (string.IsNullOrEmpty(PasswordTextBox.Text))
                    throw new Exception("Введите пароль");

                else
                {
                    SchedulerDbContext.DbContext.Employees.Add(new Employee
                    {
                        EmployeeId = default,
                        WorkingStatus = true,
                        IsTelegramConfirmed = false,
                        Role = ManagerRadioBttn.IsChecked.GetValueOrDefault(),
                        Name = NameTextBox.Text.Trim(),
                        Login = LoginTextBox.Text.Trim(),
                        Password = PasswordTextBox.Text.Trim(),
                        PhoneNumber = PhoneTextBox.Text.Trim(),
                        EMail = string.IsNullOrEmpty(EmailTextBox.Text.Trim()) ? null : EmailTextBox.Text.Trim(),
                    });

                    SchedulerDbContext.DbContext.SaveChanges();
                    NavigationService.Navigate(new TutorAndSubjectPage());
                    MessageBox.Show("Новый пользователь зарегистрирован", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        private void EmailTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (InputRegExps.EmailLocaleRegEx().IsMatch(e.Text))
                e.Handled = true;
        }
    }
}
