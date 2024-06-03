using Scheduler.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scheduler.Pages
{
    public partial class CabinetPage : Page
    {
        public CabinetPage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CabinetListView.ItemsSource = SchedulerDbContext.DbContext.Cabinets.ToList();
        }

        private void AddCabinetBttn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NumberTxtBox.Text))
            {
                SchedulerDbContext.DbContext.Cabinets.Add(new Cabinet()
                {
                    Number = NumberTxtBox.Text.Trim(),
                    Name = NameTxtBox.Text.Trim(),
                });
                SchedulerDbContext.DbContext.SaveChanges();

                NumberTxtBox.Text = string.Empty;
                NameTxtBox.Text = string.Empty;
                CabinetListView.ItemsSource = SchedulerDbContext.DbContext.Cabinets.ToList();
            }
        }
    }
}
