using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        List<DayTab> tempDayTab = null!;

        public EditScheduleTabPage(List<DayTab> sourceDayTab)
        {
            InitializeComponent();
            ClassesTimingComboBox.ItemsSource = SchedulerDbContext.dbContext.ClassesTimingHeaders.ToList();
            DayOfWeekHeader.Header = ConvertDayOfWeekToString(sourceDayTab.First().DayOfWeek);

            tempDayTab = sourceDayTab;
            EditGridViewModel viewModel = new(source: tempDayTab);
            DataContext = viewModel;
            EditedGrid.ItemsSource = viewModel.DayTab;
        }

        public class EditGridViewModel
        {
            // Для одной строки DayTab - три коллекции. Сортировки влияют на коллекции всех строк
            public List<DayTab> DayTab { get; set; }
            public List<Employee> Tutors { get; set; } = SchedulerDbContext.dbContext.Employees.Where(c => c.Role == false).ToList();
            public List<Subject> Subjects { get; set; } = SchedulerDbContext.dbContext.Subjects.ToList();
            public List<Cabinet> Cabinets { get; set; } = SchedulerDbContext.dbContext.Cabinets.ToList();

            public EditGridViewModel(List<DayTab> source, Subject? sortBySubj = null, Employee? sortByTutor = null)
            {
                DayTab = source;
            }

            public void SortBySubject(Subject sortBySubj)
            {
                Tutors = SchedulerDbContext.dbContext.Tutions
                                                .Where(tution => tution.SubjectId == sortBySubj.SubjectId && tution.EndDate == null)
                                                .Select(tution => tution.Employee)
                                                .Distinct()
                                                .ToList();
            }

            public void SortByTutors(Employee sortByTutor)
            {
                Subjects = SchedulerDbContext.dbContext.Tutions
                                                .Where(tution => tution.EmployeeId == sortByTutor.EmployeeId && tution.EndDate == null)
                                                .Select(tution => tution.Subject)
                                                .Distinct()
                                                .ToList();
            }
        }

        private void SubjectCBcell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((EditGridViewModel)DataContext).SortBySubject(((ComboBox)sender).SelectedItem as Subject);
        }

        private void TutorCBcell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((EditGridViewModel)DataContext).SortByTutors(((ComboBox)sender).SelectedItem as Employee);
        }



        private void GoBackBttn_Click(object sender, RoutedEventArgs e)
        {
            if(NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ((DockPanel)((MainWindow)Application.Current.MainWindow).FindName("MenuPanel")).Visibility = Visibility.Collapsed;
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            ((DockPanel)((MainWindow)Application.Current.MainWindow).FindName("MenuPanel")).Visibility = Visibility.Visible;
        }

        private void ComboBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataContext = new EditGridViewModel(tempDayTab);
        }
    }
}
