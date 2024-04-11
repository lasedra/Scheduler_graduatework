using Scheduler.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using static Scheduler.Services.ScheduleController;

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
            StackPanel senderStackPanel = (StackPanel)((Button)sender).Parent;
            UIElement[] grids = new[] { MondayGrid, TuesdayGrid, WednesdayGrid, ThursdayGrid, FridayGrid };
            List<DayTab> sourceDayTab = new List<DayTab>();

            foreach (UIElement grid in grids)
            {
                if (senderStackPanel.Children.Contains(grid))
                {
                    sourceDayTab = (List<DayTab>)((DataGrid)grid).ItemsSource;
                    break;
                }
            }
            NavigationService.GetNavigationService(this).Navigate(new EditScheduleTabPage(sourceDayTab));
        }

        private void ScheduleEditingBttn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new ScheduleEditingPage());
        }
    }
}
