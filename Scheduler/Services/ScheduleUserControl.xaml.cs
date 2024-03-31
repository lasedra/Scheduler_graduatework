using Scheduler.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Scheduler.Services
{
    public partial class ScheduleUserControl : UserControl
    {
        public ScheduleUserControl()
        {
            InitializeComponent();
        }

        private void EditTabBttn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(((MainWindow)Window.GetWindow(this)).EditScheduleTabPage);
        }
    }
}
