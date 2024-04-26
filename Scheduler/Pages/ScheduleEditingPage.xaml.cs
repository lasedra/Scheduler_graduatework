using Scheduler.Models;
using Scheduler.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Scheduler.Pages
{
    public partial class ScheduleEditingPage : Page
    {
        //TODO: Каникулы
        //TODO: Зависимость ПРЕДМЕТ-ПРЕПОДАВАТЕЛЬ-ГРУППА
        //TODO: Большой рефакторинг

        public ScheduleEditingPage()
        {
            InitializeComponent();

            TutorComboBox.ItemsSource = SchedulerDbContext.dbContext.Employees.Where(c => c.Role == false).ToList();
            StudentGroupComboBox.ItemsSource = SchedulerDbContext.dbContext.StudentGroups.ToList();
            CabinetComboBox.ItemsSource = SchedulerDbContext.dbContext.Cabinets.ToList();

            ScheduleController scheduleController = new ScheduleController();

            ScheduleEditing.Monday_1.DataContext = scheduleController.MondayTab.First(c => c.ClassNumber == 1);
            ScheduleEditing.Monday_2.DataContext = scheduleController.MondayTab.First(c => c.ClassNumber == 2);
            ScheduleEditing.Monday_3.DataContext = scheduleController.MondayTab.First(c => c.ClassNumber == 3);
            ScheduleEditing.Monday_4.DataContext = scheduleController.MondayTab.First(c => c.ClassNumber == 4);
        }

        private void TutorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubjectComboBox.IsEnabled = true;
            SubjectComboBox.ItemsSource = SchedulerDbContext.dbContext.Tutions
                                                .Where(tution => tution.EmployeeId == ((Employee)TutorComboBox.SelectedItem).EmployeeId && tution.EndDate == null)
                                                .Select(tution => tution.Subject)
                                                .Distinct()
                                                .ToList();
        }

        private void SaveChangesBttn_Click(object sender, RoutedEventArgs e)
        {
            ScheduleController controller = new ScheduleController();

            if(StudentGroupComboBox.SelectedItem != null &&
                TutorComboBox.SelectedItem != null &&
                SubjectComboBox.SelectedItem != null &&
                CabinetComboBox.SelectedItem != null)
            {
                List<DailyScheduleBody> weekToEdit = SchedulerDbContext.dbContext.DailyScheduleBodies.Where(c =>
                    c.OfDate >= controller.CurrentWeek.WeekStart &&
                    c.OfDate <= controller.CurrentWeek.WeekEnd &&
                    c.StudentGroupCode == ((StudentGroup)StudentGroupComboBox.SelectedItem).StudentGroupCode).ToList();
                List<ToggleButton> checkedClasses = new List<ToggleButton>
            {
                FirstClass,
                SecondClass,
                ThirdClass,
                FourthClass
            }.Where(c => c.IsChecked == true).ToList();
                List<ToggleButton> checkedDays = new List<ToggleButton>
            {
                Monday,
                Tuesday,
                Wednesday,
                Thursday,
                Friday
            }.Where(c => c.IsChecked == true).ToList();

                foreach (var day in checkedDays)
                {
                    foreach (var _class in checkedClasses)
                    {
                        DailyScheduleBody toEdit = weekToEdit.First(c =>
                        c.OfDate.DayOfWeek.ToString() == day.Name &&
                        c.ClassNumber.ToString() == _class.Content.ToString());

                        if (toEdit.Employee == null &&
                            toEdit.Subject == null &&
                            toEdit.CabinetNumber == null)
                        {
                            toEdit.Employee = TutorComboBox.SelectedItem as Employee;
                            toEdit.Subject = SubjectComboBox.SelectedItem as Subject;
                            toEdit.CabinetNumber = ((Cabinet)CabinetComboBox.SelectedItem).Number;

                            SchedulerDbContext.dbContext.SaveChanges();
                            MessageBox.Show("Расписание успешно изменено!");
                        }
                        else
                            MessageBox.Show("Хотите перезаписать имеющиеся ячейки?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    }
                }
            }
            
        }
    }
}
