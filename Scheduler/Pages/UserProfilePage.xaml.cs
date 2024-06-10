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
        private Employee? GivenUser { get; set; }
        private MainWindow CurrentMainWindow { get; set; } = null!;

        public UserProfilePage(Employee? user = null)
        {
            GivenUser = user;
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentMainWindow = (MainWindow)Application.Current.MainWindow;
            if(GivenUser != null)
            {
                if (SchedulerDbContext.CurrentUser == GivenUser)
                    EditRolePanel.Visibility = Visibility.Collapsed;
                else
                {
                    DeleteUserBttn.Visibility = Visibility.Visible;
                    LogOutBttn.Visibility = Visibility.Collapsed;
                }

                if (GivenUser.Role)
                {
                    ManagerRadioBttn.IsChecked = true;
                    TutorRadioBttn.IsChecked = false;
                }else{
                    ManagerRadioBttn.IsChecked = false;
                    TutorRadioBttn.IsChecked = true;
                }
                ProfileNameTextBlock.Text = GivenUser.Name;
                NameTextBox.Text = GivenUser.Name;
                LoginTextBox.Text = GivenUser.Login;
                PasswordTextBox.Text = GivenUser.Password;
                PhoneTextBox.Text = GivenUser.Phone.ToString();
                EmailTextBox.Text = GivenUser.EMail;
            }
            else
            {
                ProfileNameTextBlock.Text = "Новый пользователь";
                UserProfilePic.Visibility = Visibility.Collapsed;
                RegisterProfilePic.Visibility = Visibility.Visible;

                CreateAccountBttn.Visibility = Visibility.Visible;
                LogOutBttn.Visibility = Visibility.Collapsed;

                EditNameBttn.Visibility = Visibility.Collapsed;
                NameTextBox.IsReadOnly = false;
                EditLoginBttn.Visibility = Visibility.Collapsed;
                LoginTextBox.IsReadOnly = false;
                EditPasswordBttn.Visibility = Visibility.Collapsed;
                PasswordTextBox.IsReadOnly = false;
                TutorRadioBttn.IsChecked = true;
                EditPhoneBttn.Visibility = Visibility.Collapsed;
                PhoneTextBox.IsReadOnly = false;
                EditEmailBttn.Visibility = Visibility.Collapsed;
                EmailTextBox.IsReadOnly = false;
            }
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
            if (InputRegExps.EmailLocaleRegEx().IsMatch(e.Text))
                e.Handled = true;
        }
        private void AccesNoSpaceInput(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }
        public string ReplaceAt(string input, int index, char newChar)
        {
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
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

                SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == GivenUser.EmployeeId).Name = NameTextBox.Text.Trim();
                SchedulerDbContext.DbContext.SaveChanges();

                ProfileNameTextBlock.Text = GivenUser.Name;
                ((TextBlock)CurrentMainWindow.FindName("UserNameTextBlock")).Text = SchedulerDbContext.CurrentUser.Name;
            }
        }
        private void NameDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NameTextBox.IsReadOnly = true;
            EditNameBttn.Visibility = Visibility.Visible;
            NameDecision.Visibility = Visibility.Collapsed;
            NameTextBox.Text = GivenUser.Name;
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
            else if (GivenUser.Login != LoginTextBox.Text && SchedulerDbContext.DbContext.Employees.Any(c => c.Login == LoginTextBox.Text))
                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                LoginTextBox.IsReadOnly = true;
                EditLoginBttn.Visibility = Visibility.Visible;
                LoginDecision.Visibility = Visibility.Collapsed;

                GivenUser.Login = LoginTextBox.Text.Trim();
                SchedulerDbContext.DbContext.SaveChanges();
            }
        }
        public void LoginDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginTextBox.IsReadOnly = true;
            EditLoginBttn.Visibility = Visibility.Visible;
            LoginDecision.Visibility = Visibility.Collapsed;
            LoginTextBox.Text = GivenUser.Login;
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

                GivenUser.Password = PasswordTextBox.Text.Trim();
                SchedulerDbContext.DbContext.SaveChanges();
            }
        }
        private void PasswordDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PasswordTextBox.IsReadOnly = true;
            EditPasswordBttn.Visibility = Visibility.Visible;
            PasswordDecision.Visibility = Visibility.Collapsed;
            PasswordTextBox.Text = GivenUser.Password;
        }


        public void EditPhoneBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhoneTextBox.IsReadOnly = false;
            EditPhoneBttn.Visibility = Visibility.Collapsed;
            PhoneDecision.Visibility = Visibility.Visible;
        }
        public void PhoneAcceptBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Cтрогая валидация тел. номера к формату long(80000000000)
            string thePhone = PhoneTextBox.Text.Trim();
            thePhone = thePhone.Replace("+", "");
            if (thePhone.StartsWith('7'))
                thePhone = ReplaceAt(thePhone, 0, '8');

            long newPhone = long.Parse(thePhone.Trim());

            if (GivenUser.Phone != newPhone)
            {
                if (!InputRegExps.PhoneRegEx().IsMatch(PhoneTextBox.Text))
                    MessageBox.Show("Неверный формат тел. номера", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (SchedulerDbContext.DbContext.Employees.Any(c => c.Phone == newPhone))
                    MessageBox.Show("Пользователь с таким номером телефона уже существует!", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    PhoneTextBox.IsReadOnly = true;
                    EditPhoneBttn.Visibility = Visibility.Visible;
                    PhoneDecision.Visibility = Visibility.Collapsed;

                    GivenUser.Phone = newPhone;
                    SchedulerDbContext.DbContext.SaveChanges();
                }
            }
        }
        public void PhoneDeclineBttn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhoneTextBox.IsReadOnly = true;
            EditPhoneBttn.Visibility = Visibility.Visible;
            PhoneDecision.Visibility = Visibility.Collapsed;
            PhoneTextBox.Text = GivenUser.Phone.ToString();
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

                GivenUser.EMail = EmailTextBox.Text.Trim();
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
            if (GivenUser != null) 
            {
                GivenUser.Role = true;
                SchedulerDbContext.DbContext.SaveChanges();
            }
        }
        private void TutorRadioBttn_Checked(object sender, RoutedEventArgs e)
        {
            if (GivenUser != null) 
            {
                GivenUser.Role = false;
                SchedulerDbContext.DbContext.SaveChanges();
            }
        }


        public void LogOutBttn_Click(object sender, RoutedEventArgs e)
            => NavigationService.Navigate(new AuthorisationPage());
        private void DeleteUserBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить пользователя {GivenUser.Name} ?" +
                    $"\nЭто приведёт к удалению всей зависимой информации.",
                    "Минуточку",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    List<DailyScheduleBody> bodiesToUnbind = SchedulerDbContext.DbContext.DailyScheduleBodies.Where(c => c.EmployeeId == GivenUser.EmployeeId).ToList();
                    bodiesToUnbind.ForEach(c => {
                        c.Subject = null;
                        c.Employee = null;
                        c.CabinetNumber = null;
                    });
                    SchedulerDbContext.DbContext.SaveChanges();

                    // Работает только при наличии ограничения у связанных таблиц - "ON DELETE CASCADE"
                    SchedulerDbContext.DbContext.Employees
                        .Where(c => c.EmployeeId == GivenUser.EmployeeId)
                        .ExecuteDelete();

                    NavigationService.Navigate(new TutorAndSubjectPage());
                    MessageBox.Show("Пользователь успешно удалён!");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
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

                else if (SchedulerDbContext.DbContext.Employees.Any(c => c.Phone == long.Parse(PhoneTextBox.Text.Trim())))
                    throw new Exception("Пользователь с таким номером телефона уже существует!");

                else if (SchedulerDbContext.DbContext.Employees.Any(c => c.Login == LoginTextBox.Text))
                    throw new Exception("Пользователь с таким логином уже существует!");
                else
                {
                    SchedulerDbContext.DbContext.Employees.Add(new Employee
                    {
                        EmployeeId = default,
                        WorkingStatus = true,
                        TgBotChatId = null,
                        Role = ManagerRadioBttn.IsChecked.GetValueOrDefault(),
                        Name = NameTextBox.Text.Trim(),
                        Login = LoginTextBox.Text.Trim(),
                        Password = PasswordTextBox.Text.Trim(),
                        Phone = long.Parse(PhoneTextBox.Text.Trim()),
                        EMail = string.IsNullOrEmpty(EmailTextBox.Text.Trim()) ? null : EmailTextBox.Text.Trim(),
                    });

                    SchedulerDbContext.DbContext.SaveChanges();
                    NavigationService.Navigate(new TutorAndSubjectPage());
                    MessageBox.Show("Новый пользователь зарегистрирован", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}