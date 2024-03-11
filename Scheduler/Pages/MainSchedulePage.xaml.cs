using Scheduler.Models;
using System;
using System.Linq;
using System.Windows.Controls;

namespace Scheduler.Pages
{
    public partial class MainSchedulePage : Page
    {
        public MainSchedulePage()
        {
            InitializeComponent();
            MondayGrid.ItemsSource = SchedulerDbContext.dbContext.ScheduleViews.ToList();
        }

        private void AddViewRow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SchedulerDbContext.dbContext.ScheduleViews.Add(new ScheduleView
            {
                AtDay = "Вторник",
                StudentGroup = "1312",
                ClassesTimings = "Основное",
                OfDate = new DateOnly(2020, 01, 02),
                StartTime = new TimeOnly(9, 00),
                EndTime = new TimeOnly(10, 30),
                Subject = "Англ. Яз",
                AtCabinet = 705,
                Tutor = "Смелянцев И. И."
            });
            SchedulerDbContext.dbContext.SaveChanges();
        }
    }
}
