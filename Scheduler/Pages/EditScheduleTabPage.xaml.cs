using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using static Scheduler.Services.ScheduleController;

namespace Scheduler.Pages
{
    public partial class EditScheduleTabPage : Page
    {
        #region DayOfWeek Converter
        private readonly Dictionary<DayOfWeek, string> DayOfWeekTranslations = new Dictionary<DayOfWeek, string>
        {
            { DayOfWeek.Monday, "Понедельник" },
            { DayOfWeek.Tuesday, "Вторник" },
            { DayOfWeek.Wednesday, "Среда" },
            { DayOfWeek.Thursday, "Четверг" },
            { DayOfWeek.Friday, "Пятница" },
            { DayOfWeek.Saturday, "Суббота" },
            { DayOfWeek.Sunday, "Воскресенье" }
        };
        public string ConvertDayOfWeekToString(DayOfWeek dayOfWeek)
        {
            return DayOfWeekTranslations.ContainsKey(dayOfWeek) ? DayOfWeekTranslations[dayOfWeek] : string.Empty;
        }
        #endregion


        public EditScheduleTabPage(List<DayTab> sourceDayTab)
        {
            InitializeComponent();

            DataContext = new EditGridViewModel(sourceDayTab);
            ClassesTimingComboBox.ItemsSource = SchedulerDbContext.dbContext.ClassesTimingHeaders.ToList();
            DayOfWeekHeader.Header = ConvertDayOfWeekToString(sourceDayTab.First().DayOfWeek);
            EditedGrid.ItemsSource = sourceDayTab;
        }

        public class EditGridViewModel
        {
            public List<DayTab> DayTab { get; set; }
            public List<Employee> Tutors { get; set; } = SchedulerDbContext.dbContext.Employees.Where(c => c.Role == false).ToList();
            public List<Subject> Subjects { get; set; } = SchedulerDbContext.dbContext.Subjects.ToList();

            public EditGridViewModel(List<DayTab> source)
            {
                DayTab = source;
            }
        }



        private void GoBackBttn_Click(object sender, RoutedEventArgs e)
        {
            if(NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //((DockPanel)Window.GetWindow(this).FindName("MenuPanel")).Visibility = Visibility.Collapsed;
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            //((DockPanel)Window.GetWindow(this).FindName("MenuPanel")).Visibility = Visibility.Visible;
        }
    }
}
