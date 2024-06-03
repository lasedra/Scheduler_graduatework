using Scheduler.Models;
using Scheduler.Services;
using Scheduler.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Scheduler.Pages
{
    public partial class EditingPage : Page
    {
        //TODO: Каникулы и звонки

        ScheduleController scheduleController {  get; set; } = new ScheduleController();

        public EditingPage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StudentGroupComboBox.ItemsSource = SchedulerDbContext.DbContext.StudentGroups.ToList();
            StudentGroupComboBox.SelectedItem = SchedulerDbContext.DbContext.StudentGroups.First(c => c.StudentGroupCode == scheduleController.CurrentGroupCode);

            TutorComboBox.ItemsSource = SchedulerDbContext.DbContext.Employees.Where(c => c.Role == false).ToList();
            CabinetComboBox.ItemsSource = SchedulerDbContext.DbContext.Cabinets.ToList();
            UpdateScheduleEditingView();
        }

        private List<Subject> SelectAllowedSubjects()
        {
            List<Subject> tutionSubjects = SchedulerDbContext.DbContext.Tutions
                                                .Where(tution => tution.EmployeeId == ((Employee)TutorComboBox.SelectedItem).EmployeeId && tution.EndDate == null)
                                                .Select(tution => tution.Subject)
                                                .Distinct()
                                                .ToList();
            List<Subject> studyingSubjects = SchedulerDbContext.DbContext.Studyings
                                                .Where(studying => studying.StudentGroupCode == ((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode)
                                                .Select(studying => studying.Subject)
                                                .Distinct()
                                                .ToList();
            List<Subject> allowedSubjects = studyingSubjects.Intersect(tutionSubjects).ToList();
            return allowedSubjects;
        }

        private void StudentGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            scheduleController.SetCurrentGroupCode(((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode);
            scheduleController.SetCurrentWeek(new TimePeriod(SchedulerDbContext.DbContext.DailyScheduleHeaders
                .Where(c => c.StudentGroupCode == scheduleController.CurrentGroupCode)
                .Max(c => c.OfDate)));

            if (TutorComboBox.SelectedItem != null)
                SubjectComboBox.ItemsSource = SelectAllowedSubjects();

            UpdateScheduleEditingView();
        }

        private void TutorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubjectComboBox.IsEnabled = true;
            SubjectComboBox.ItemsSource = SelectAllowedSubjects();
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
                    UpdateScheduleEditingView();
                }
                else if (anyToRewrite && rewriteDecision)
                {
                    EditScheduleCell(checkedClasses, checkedDays, weekToEdit);
                    UpdateScheduleEditingView();
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

        private void UpdateScheduleEditingView()
        {
            scheduleController.SetDayTabs();
            var selectedGroupCode = ((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode;
            DateOnly selectedGroupFirstSchedule = SchedulerDbContext.DbContext.DailyScheduleHeaders
                .Where(c => c.StudentGroupCode == selectedGroupCode)
                .Min(c => c.OfDate);
            DateOnly selectedGroupLastSchedule = SchedulerDbContext.DbContext.DailyScheduleHeaders
                .Where(c => c.StudentGroupCode == selectedGroupCode)
                .Max(c => c.OfDate);

            ScheduleWeekSpanTB.Text = scheduleController.CurrentWeek.GetWeekSpan();

            BackOnTimelineBttn.Visibility = (scheduleController.CurrentWeek.WeekStart == selectedGroupFirstSchedule) ? Visibility.Hidden : Visibility.Visible;
            if (CurrentUser.Role == true)
                ForwardOnTimelineBttn.Content = (scheduleController.CurrentWeek.WeekEnd.AddDays(-2) == selectedGroupLastSchedule) ? "➕" : "▶️";
            else
                ForwardOnTimelineBttn.Visibility = (scheduleController.CurrentWeek.WeekEnd.AddDays(-2) == selectedGroupLastSchedule) ? Visibility.Hidden : Visibility.Visible;


            Monday1.DataContext = scheduleController.MondayTab.First(c => c.ClassNumber == 1);
            Monday2.DataContext = scheduleController.MondayTab.First(c => c.ClassNumber == 2);
            Monday3.DataContext = scheduleController.MondayTab.First(c => c.ClassNumber == 3);
            Monday4.DataContext = scheduleController.MondayTab.First(c => c.ClassNumber == 4);

            Tuesday1.DataContext = scheduleController.TuesdayTab.First(c => c.ClassNumber == 1);
            Tuesday2.DataContext = scheduleController.TuesdayTab.First(c => c.ClassNumber == 2);
            Tuesday3.DataContext = scheduleController.TuesdayTab.First(c => c.ClassNumber == 3);
            Tuesday4.DataContext = scheduleController.TuesdayTab.First(c => c.ClassNumber == 4);

            Wednesday1.DataContext = scheduleController.WednesdayTab.First(c => c.ClassNumber == 1);
            Wednesday2.DataContext = scheduleController.WednesdayTab.First(c => c.ClassNumber == 2);
            Wednesday3.DataContext = scheduleController.WednesdayTab.First(c => c.ClassNumber == 3);
            Wednesday4.DataContext = scheduleController.WednesdayTab.First(c => c.ClassNumber == 4);

            Thursday1.DataContext = scheduleController.ThursdayTab.First(c => c.ClassNumber == 1);
            Thursday2.DataContext = scheduleController.ThursdayTab.First(c => c.ClassNumber == 2);
            Thursday3.DataContext = scheduleController.ThursdayTab.First(c => c.ClassNumber == 3);
            Thursday4.DataContext = scheduleController.ThursdayTab.First(c => c.ClassNumber == 4);

            Friday1.DataContext = scheduleController.FridayTab.First(c => c.ClassNumber == 1);
            Friday2.DataContext = scheduleController.FridayTab.First(c => c.ClassNumber == 2);
            Friday3.DataContext = scheduleController.FridayTab.First(c => c.ClassNumber == 3);
            Friday4.DataContext = scheduleController.FridayTab.First(c => c.ClassNumber == 4);
        }

        private void BackOnTimelineBttn_Click(object sender, RoutedEventArgs e)
        {
            scheduleController.SetCurrentWeek(new TimePeriod(scheduleController.CurrentWeek.WeekStart.AddDays(-7)));
            UpdateScheduleEditingView();
        }

        private void ForwardOnTimelineBttn_Click(object sender, RoutedEventArgs e)
        {
            if (ForwardOnTimelineBttn.Content.ToString() == "➕")
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите создать новое расписание для группы {((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode}",
                    "Минуточку",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    scheduleController.AddSchedule(((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode);
                    UpdateScheduleEditingView();
                }
            }
            else if (ForwardOnTimelineBttn.Content.ToString() == "▶️")
            {
                scheduleController.SetCurrentWeek(new TimePeriod(scheduleController.CurrentWeek.WeekStart.AddDays(7)));
                UpdateScheduleEditingView();
            }
        }

        private void DeleteCell(object sender, RoutedEventArgs e)
        {
            List<DailyScheduleBody> weekToEdit = SchedulerDbContext.DbContext.DailyScheduleBodies
                    .Where(c => c.OfDate >= scheduleController.CurrentWeek.WeekStart &&
                           c.OfDate <= scheduleController.CurrentWeek.WeekEnd &&
                           c.StudentGroupCode == ((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode).ToList();

            string cellName = ((LessonCell)sender).Name;
            DayOfWeek dayToEdit = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), cellName.Remove(cellName.Length - 1), true);
            int classToEdit = Convert.ToInt32(cellName.ElementAt(cellName.Length - 1).ToString());

            DailyScheduleBody cellToRemove = weekToEdit.First(c => c.OfDate.DayOfWeek == dayToEdit && c.ClassNumber == classToEdit);
            if(cellToRemove.Employee != null || cellToRemove.Subject != null || cellToRemove.CabinetNumber != null)
            {
                cellToRemove.Employee = null;
                cellToRemove.Subject = null;
                cellToRemove.CabinetNumber = null;

                SchedulerDbContext.DbContext.SaveChanges();
                scheduleController = new();
                UpdateScheduleEditingView();
            }
        }
    }
}
