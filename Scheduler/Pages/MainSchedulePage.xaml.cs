using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scheduler.Pages
{
    /* 
      TODO: Миграции
            Базовые справочные данные:
            - Студ. группа типа "Не указана"
            - аккаунт админа
            - основное и сокращённое расписания
    */
    /* TODO: Использовать хеширование */
    /* TODO: Пофиксить скрытие верхней панели */
    /* TODO: Сделать "Запомнить меня" */
    /* TODO: Функционал:
                1) Оповещения МУП-а о внесении изменений в расписание
                2) Логгирование*/

    public partial class MainSchedulePage : Page
    {
        ScheduleController schController = new ScheduleController();

        public MainSchedulePage()
        {
            InitializeComponent();
            UpdateScheduleSource();
            
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
                /* TODO: Предоставить выбор */
                schController.AddSchedule("Не указано", "Основное");
                UpdateScheduleSource();

            }
            else
            {
                schController.CurrentWeek = new ScheduleController.TimePeriod(schController.CurrentWeek.WeekStart.AddDays(7));
                UpdateScheduleSource();
            }
        }

        public void UpdateScheduleSource()
        {
            StudentGroupComboBox.ItemsSource = SchedulerDbContext.dbContext.StudentGroups.ToList();
            StudentGroupComboBox.SelectedItem = SchedulerDbContext.dbContext.StudentGroups.First(c => c.StudentGroupCode == schController.CurrentGroupCode);

            ScheduleWeekSpanTB.Text = schController.CurrentWeek.GetWeekSpan();

            DateOnly firstEverSked = SchedulerDbContext.dbContext.DailyScheduleHeaders.Min(c => c.OfDate);
            DateOnly lastEverSked = SchedulerDbContext.dbContext.DailyScheduleHeaders.Max(c => c.OfDate);
            BackOnTimelineBttn.Visibility = (schController.CurrentWeek.WeekStart == firstEverSked) ? Visibility.Hidden : Visibility.Visible;
            ForwardOnTimelineBttn.Content = (schController.CurrentWeek.WeekStart.AddDays(4) == lastEverSked) ? "+" : "Вперёд";

            Schedule.MondayGrid.ItemsSource = schController.MondayTab;
            Schedule.TuesdayGrid.ItemsSource = schController.TuesdayTab;
            Schedule.WednesdayGrid.ItemsSource = schController.WednesdayTab;
            Schedule.ThursdayGrid.ItemsSource = schController.ThursdayTab;
            Schedule.FridayGrid.ItemsSource = schController.FridayTab;
        }
    }
}
