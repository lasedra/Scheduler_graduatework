using Scheduler.Models;
using System.Linq;
using System.Windows.Controls;

namespace Scheduler.Pages
{
    public partial class ScheduleEditingPage : Page
    {
        public ScheduleEditingPage()
        {
            InitializeComponent();

            //TODO: Зависимость ПРЕДМЕТ-ПРЕПОДАВАТЕЛЬ-ГРУППА
            TutorComboBox.ItemsSource = SchedulerDbContext.dbContext.Employees.Where(c => c.Role == false).ToList();
            StudentGroupComboBox.ItemsSource = SchedulerDbContext.dbContext.StudentGroups.ToList();
            CabinetComboBox.ItemsSource = SchedulerDbContext.dbContext.Cabinets.ToList();
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
    }
}
