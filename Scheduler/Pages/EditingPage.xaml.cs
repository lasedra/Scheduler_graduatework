using Scheduler.Models;
using Scheduler.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Scheduler.Pages
{
    public partial class EditingPage : Page
    {
        //TODO: Каникулы
        //TODO: Зависимость ПРЕДМЕТ-ПРЕПОДАВАТЕЛЬ-ГРУППА

        ScheduleController scheduleController {  get; set; } = new ScheduleController();

        public EditingPage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TutorComboBox.ItemsSource = SchedulerDbContext.DbContext.Employees.Where(c => c.Role == false).ToList();
            StudentGroupComboBox.ItemsSource = SchedulerDbContext.DbContext.StudentGroups.ToList();
            CabinetComboBox.ItemsSource = SchedulerDbContext.DbContext.Cabinets.ToList();
            UpdateScheduleEditingView(((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode);
        }

        private void TutorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubjectComboBox.IsEnabled = true;
            SubjectComboBox.ItemsSource = SchedulerDbContext.DbContext.Tutions
                                                .Where(tution => tution.EmployeeId == ((Employee)TutorComboBox.SelectedItem).EmployeeId && tution.EndDate == null)
                                                .Select(tution => tution.Subject)
                                                .Distinct()
                                                .ToList();
        }

        private void SaveChangesBttn_Click(object sender, RoutedEventArgs e)
        {
            List<ToggleButton> checkedClasses = new List<ToggleButton>{
                    FirstClass,
                    SecondClass,
                    ThirdClass,
                    FourthClass
                }.Where(c => c.IsChecked == true).ToList();

            List<ToggleButton> checkedDays = new List<ToggleButton>{
                    Monday,
                    Tuesday,
                    Wednesday,
                    Thursday,
                    Friday
                }.Where(c => c.IsChecked == true).ToList();

            #region Input handling
            if (checkedClasses.Count == 0)
                MessageBox.Show("Выберите назначаемые пары!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

            else if(checkedDays.Count == 0)
                MessageBox.Show("Выберите назначаемые дни!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (StudentGroupComboBox.SelectedItem == null)
                MessageBox.Show("Выберите группу обучающихся!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (TutorComboBox.SelectedItem == null)
                MessageBox.Show("Выберите преподавателя!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (SubjectComboBox.SelectedItem == null)
                MessageBox.Show("Выберите предмет!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (CabinetComboBox.SelectedItem == null) 
                MessageBox.Show("Выберите аудиторию!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            #endregion

            else
            {
                List<DailyScheduleBody> weekToEdit = SchedulerDbContext.DbContext.DailyScheduleBodies
                    .Where(c => c.OfDate >= scheduleController.CurrentWeek.WeekStart &&
                           c.OfDate <= scheduleController.CurrentWeek.WeekEnd &&
                           c.StudentGroupCode == ((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode).ToList();

                List<DailyScheduleBody> daysToEdit = new();

                foreach (var day in checkedDays)
                    foreach (var _class in checkedClasses)
                        daysToEdit.Add(weekToEdit.First(c => c.OfDate.DayOfWeek.ToString() == day.Name && c.ClassNumber.ToString() == _class.Content.ToString()));

                bool rewriteDecision = false;
                bool anyToRewrite = daysToEdit.Any(c => c.Employee != null || c.Subject != null || c.CabinetNumber != null);

                if (anyToRewrite)
                {
                    var result = MessageBox.Show("Хотите перезаписать имеющиеся ячейки?", "Минуточку", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                        rewriteDecision = true;
                    else
                        rewriteDecision = false;
                }
                if (!anyToRewrite)
                {
                    EditScheduleCell(checkedClasses, checkedDays, weekToEdit);
                    MessageBox.Show("Расписание успешно изменено!");
                    UpdateScheduleEditingView(((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode);
                }
                else if (anyToRewrite && rewriteDecision)
                {
                    EditScheduleCell(checkedClasses, checkedDays, weekToEdit);
                    MessageBox.Show("Расписание успешно изменено!");
                    UpdateScheduleEditingView(((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode);
                }
            }
        }

        private void EditScheduleCell(List<ToggleButton> checkedClasses, List<ToggleButton> checkedDays, List<DailyScheduleBody> weekToEdit)
        {
            foreach (var day in checkedDays)
            {
                foreach (var _class in checkedClasses)
                {
                    DailyScheduleBody dayToEdit = weekToEdit
                        .First(c => c.OfDate.DayOfWeek.ToString() == day.Name &&
                                    c.ClassNumber.ToString() == _class.Content.ToString());

                    dayToEdit.Employee = TutorComboBox.SelectedItem as Employee;
                    dayToEdit.Subject = SubjectComboBox.SelectedItem as Subject;
                    dayToEdit.CabinetNumber = ((Cabinet)CabinetComboBox.SelectedItem).Number;
                }
                SchedulerDbContext.DbContext.SaveChanges();
            }
        }

        private void UpdateScheduleEditingView(string studentGroupCode)
        {
            scheduleController = new ScheduleController();

            ScheduleEditingView.Monday_1.DataContext = scheduleController.MondayTab.First(c => c.ClassNumber == 1);
            ScheduleEditingView.Monday_2.DataContext = scheduleController.MondayTab.First(c => c.ClassNumber == 2);
            ScheduleEditingView.Monday_3.DataContext = scheduleController.MondayTab.First(c => c.ClassNumber == 3);
            ScheduleEditingView.Monday_4.DataContext = scheduleController.MondayTab.First(c => c.ClassNumber == 4);

            ScheduleEditingView.Tuesday_1.DataContext = scheduleController.TuesdayTab.First(c => c.ClassNumber == 1);
            ScheduleEditingView.Tuesday_2.DataContext = scheduleController.TuesdayTab.First(c => c.ClassNumber == 2);
            ScheduleEditingView.Tuesday_3.DataContext = scheduleController.TuesdayTab.First(c => c.ClassNumber == 3);
            ScheduleEditingView.Tuesday_4.DataContext = scheduleController.TuesdayTab.First(c => c.ClassNumber == 4);

            ScheduleEditingView.Wednesday_1.DataContext = scheduleController.WednesdayTab.First(c => c.ClassNumber == 1);
            ScheduleEditingView.Wednesday_2.DataContext = scheduleController.WednesdayTab.First(c => c.ClassNumber == 2);
            ScheduleEditingView.Wednesday_3.DataContext = scheduleController.WednesdayTab.First(c => c.ClassNumber == 3);
            ScheduleEditingView.Wednesday_4.DataContext = scheduleController.WednesdayTab.First(c => c.ClassNumber == 4);

            ScheduleEditingView.Thursday_1.DataContext = scheduleController.ThursdayTab.First(c => c.ClassNumber == 1);
            ScheduleEditingView.Thursday_2.DataContext = scheduleController.ThursdayTab.First(c => c.ClassNumber == 2);
            ScheduleEditingView.Thursday_3.DataContext = scheduleController.ThursdayTab.First(c => c.ClassNumber == 3);
            ScheduleEditingView.Thursday_4.DataContext = scheduleController.ThursdayTab.First(c => c.ClassNumber == 4);

            ScheduleEditingView.Friday_1.DataContext = scheduleController.FridayTab.First(c => c.ClassNumber == 1);
            ScheduleEditingView.Friday_2.DataContext = scheduleController.FridayTab.First(c => c.ClassNumber == 2);
            ScheduleEditingView.Friday_3.DataContext = scheduleController.FridayTab.First(c => c.ClassNumber == 3);
            ScheduleEditingView.Friday_4.DataContext = scheduleController.FridayTab.First(c => c.ClassNumber == 4);
        }

        private void StudentGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => UpdateScheduleEditingView(((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode);
    }
}
