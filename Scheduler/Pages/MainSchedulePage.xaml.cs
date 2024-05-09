using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scheduler.Pages
{
    // TODO: Таблица Studying при GettingStarted
    /* TODO: Миграции */
    /* TODO: Использовать хеширование */
    /* TODO: Функционал:
                1) Оповещения МУП-а о внесении изменений в расписание
                2) Логгирование*/

    public partial class MainSchedulePage : Page
    {
        ScheduleController ScheduleController { get; set; } = null!;

        public MainSchedulePage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Getting started check
            if (!SchedulerDbContext.DbContext.StudentGroups.Any()) 
                NavigationService.Navigate(new StudentGroupPage(true));
            else
            {
                ScheduleController = new();
                StudentGroupComboBox.ItemsSource = SchedulerDbContext.DbContext.StudentGroups.ToList();
                StudentGroupComboBox.SelectedItem = SchedulerDbContext.DbContext.StudentGroups.First(c => c.StudentGroupCode == ScheduleController.CurrentGroupCode);
                UpdateScheduleSource();
            }
        }

        public void UpdateScheduleSource()
        {
            ScheduleController.SetDayTabs();
            var selectedGroupCode = ((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode;
            DateOnly selectedGroupFirstSchedule = SchedulerDbContext.DbContext.DailyScheduleHeaders
                .Where(c => c.StudentGroupCode == selectedGroupCode)
                .Min(c => c.OfDate);
            DateOnly selectedGroupLastSchedule = SchedulerDbContext.DbContext.DailyScheduleHeaders
                .Where(c => c.StudentGroupCode == selectedGroupCode)
                .Max(c => c.OfDate);

            ScheduleWeekSpanTB.Text = ScheduleController.CurrentWeek.GetWeekSpan();

            BackOnTimelineBttn.Visibility = (ScheduleController.CurrentWeek.WeekStart == selectedGroupFirstSchedule) ? Visibility.Hidden : Visibility.Visible;
            if (CurrentUser.Role == true)
                ForwardOnTimelineBttn.Content = (ScheduleController.CurrentWeek.WeekEnd.AddDays(-2) == selectedGroupLastSchedule) ? "➕" : "▶️";
            else
                ForwardOnTimelineBttn.Visibility = (ScheduleController.CurrentWeek.WeekEnd.AddDays(-2) == selectedGroupLastSchedule) ? Visibility.Hidden : Visibility.Visible;


            Schedule.MondayGrid.ItemsSource = ScheduleController.MondayTab.OrderBy(c => c.TimeSlot);
            Schedule.TuesdayGrid.ItemsSource = ScheduleController.TuesdayTab.OrderBy(c => c.TimeSlot);
            Schedule.WednesdayGrid.ItemsSource = ScheduleController.WednesdayTab.OrderBy(c => c.TimeSlot);
            Schedule.ThursdayGrid.ItemsSource = ScheduleController.ThursdayTab.OrderBy(c => c.TimeSlot);
            Schedule.FridayGrid.ItemsSource = ScheduleController.FridayTab.OrderBy(c => c.TimeSlot);
        }

        private void BackOnTimelineBttn_Click(object sender, RoutedEventArgs e)
        {
            ScheduleController.SetCurrentWeek(new TimePeriod(ScheduleController.CurrentWeek.WeekStart.AddDays(-7)));
            UpdateScheduleSource();
        }

        private void ForwardOnTimelineBttn_Click(object sender, RoutedEventArgs e)
        {
            if(ForwardOnTimelineBttn.Content.ToString() == "➕")
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите создать новое расписание для группы {((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode}", 
                    "Минуточку",
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Warning);

                if(result == MessageBoxResult.Yes)
                {
                    ScheduleController.AddSchedule(((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode);
                    ScheduleController.SetCurrentWeek(new TimePeriod(ScheduleController.CurrentWeek.WeekStart.AddDays(7)));
                    UpdateScheduleSource();
                }
            }
            else if(ForwardOnTimelineBttn.Content.ToString() == "▶️")
            {
                ScheduleController.SetCurrentWeek(new TimePeriod(ScheduleController.CurrentWeek.WeekStart.AddDays(7)));
                UpdateScheduleSource();
            }
        }

        private void StudentGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScheduleController.SetCurrentGroupCode(((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode);
            ScheduleController.SetCurrentWeek(new TimePeriod(SchedulerDbContext.DbContext.DailyScheduleHeaders
                .Where(c => c.StudentGroupCode == ScheduleController.CurrentGroupCode)
                .Max(c => c.OfDate)));
            UpdateScheduleSource();
        }
    }
}
