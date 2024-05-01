using Scheduler.Models;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Scheduler.Pages
{
    public partial class RegistrationPage : Page
    {
        //TODO: Решить проблему Ctrl-C + Ctrl-V для недопустимых символов
        //TODO: Добавить hint для каждого поля, с форматами строк
        //TODO: Уточнить требования к логинам и паролям

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void CreateAccountBttn_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = NameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(NameTextBox.Text))
                MessageBox.Show("Поле \"Имя\" не должно быть пустым", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (!Regex.IsMatch(PhoneTextBox.Text, @"^(\+)?\d{11}$"))
                MessageBox.Show("Неверный формат тел. номера", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (!Regex.IsMatch(TelegramIDTextBox.Text, @"^.{5}"))
                MessageBox.Show("Неверный формат Telegram link", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (!string.IsNullOrEmpty(EmailTextBox.Text) && !Regex.IsMatch(EmailTextBox.Text, @"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9]+$"))
                MessageBox.Show("Неверный формат эл.Почты", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (!Regex.IsMatch(LoginTextBox.Text, @"^.{5}"))
                MessageBox.Show("Неверный формат логина", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (!Regex.IsMatch(PasswordTextBox.Text, @"^.{7}"))
                MessageBox.Show("Неверный формат пароля", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);

            else
            {
                SchedulerDbContext.DbContext.Employees.Add(new Employee
                {
                    EmployeeId = default,
                    WorkingStatus = true,
                    Name = NameTextBox.Text.Trim(),
                    Role = ManagerRadioBttn.IsChecked.GetValueOrDefault(),
                    Login = LoginTextBox.Text.Trim(),
                    Password = PasswordTextBox.Text.Trim(),
                    TelegramId = TelegramIDTextBox.Text.Trim(),
                    PhoneNumber = PhoneTextBox.Text.Trim(),
                    EMail = string.IsNullOrEmpty(EmailTextBox.Text.Trim()) ? null : EmailTextBox.Text.Trim(),
                });
                SchedulerDbContext.DbContext.SaveChanges();
                MessageBox.Show("Новый аккаунт зарегистрирован", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
    }
}
