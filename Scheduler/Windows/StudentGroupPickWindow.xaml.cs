using Scheduler.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scheduler.Windows
{
    public partial class StudentGroupPickWindow : Window
    {
        public string ReturnString { get; private set; }

        public StudentGroupPickWindow()
        {
            InitializeComponent();
            StudentGroupsComboBox.ItemsSource = SchedulerDbContext.dbContext.StudentGroups.Where(c => c.StudentGroupCode != "Не указано").ToList();
        }

        private void StudentGroupsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => ReturnString = ((StudentGroup)StudentGroupsComboBox.SelectedItem).StudentGroupCode;

        private void SubmitBttn_Click(object sender, RoutedEventArgs e)
            => this.Close();
    }
}
