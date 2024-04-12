using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using Scheduler.Services;
using Scheduler.Windows;
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
        ScheduleController schController = null!;

        public MainSchedulePage()
        {
            InitializeComponent();

            StudentGroupComboBox.ItemsSource = SchedulerDbContext.dbContext.StudentGroups.ToList();
            StudentGroupComboBox.SelectedItem = StudentGroupComboBox.Items.GetItemAt(0);
        }

        private void BackOnTimelineBttn_Click(object sender, RoutedEventArgs e)
        {
            schController.CurrentWeek = new ScheduleController.TimePeriod(schController.CurrentWeek.WeekStart.AddDays(-7));
            UpdateScheduleSource();
        }

        private void ForwardOnTimelineBttn_Click(object sender, RoutedEventArgs e)
        {
            if(ForwardOnTimelineBttn.Content == "+")
            {
                var result = MessageBox.Show("Предупреждение", $"Вы уверены, что хотите создать новое расписание для группы {((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode}", 
                        MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    schController.AddSchedule(((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode);
                    UpdateScheduleSource();
                }
            }
            else if(ForwardOnTimelineBttn.Content == ">>")
            {
                schController.CurrentWeek = new ScheduleController.TimePeriod(schController.CurrentWeek.WeekStart.AddDays(7));
                UpdateScheduleSource();
            }
        }

        public void UpdateScheduleSource()
        {
            ScheduleWeekSpanTB.Text = schController.CurrentWeek.GetWeekSpan();

            DateOnly firstEverSked = SchedulerDbContext.dbContext.DailyScheduleHeaders.Where(c => c.StudentGroupCode == ((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode)
                                                                                      .Min(c => c.OfDate);
            DateOnly lastEverSked = SchedulerDbContext.dbContext.DailyScheduleHeaders.Where(c => c.StudentGroupCode == ((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode)
                                                                                     .Max(c => c.OfDate);
            BackOnTimelineBttn.Visibility = (schController.CurrentWeek.WeekStart == firstEverSked) ? Visibility.Hidden : Visibility.Visible;
            if (schController.CurrentWeek.WeekStart.AddDays(4) == lastEverSked)
            {
                if (CurrentUser.Role == false)
                    ForwardOnTimelineBttn.Content = "...";
                else
                    ForwardOnTimelineBttn.Content = "+";
            }
            else
                ForwardOnTimelineBttn.Content = ">>";

            Schedule.MondayGrid.ItemsSource = schController.MondayTab;
            Schedule.TuesdayGrid.ItemsSource = schController.TuesdayTab;
            Schedule.WednesdayGrid.ItemsSource = schController.WednesdayTab;
            Schedule.ThursdayGrid.ItemsSource = schController.ThursdayTab;
            Schedule.FridayGrid.ItemsSource = schController.FridayTab;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Getting started check
            if (!SchedulerDbContext.dbContext.StudentGroups.Any())
                NavigationService.Navigate(new StudentGroupPage(true));
            else
            {
                schController = new();
                UpdateScheduleSource();
            }
        }

        private void StudentGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(schController != null)
            {
                schController.CurrentGroupCode = ((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode;
                UpdateScheduleSource();
            }
        }
    }
}
