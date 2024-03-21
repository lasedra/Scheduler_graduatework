using System.Windows;
using System.Windows.Navigation;
//using Scheduler.Models;
using Scheduler.Pages;

namespace Scheduler
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PagesFrame.Navigate(new MainSchedulePage());
        }

        private void UserImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PagesFrame.Navigate(new AuthorisationPage());
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            PagesFrame.Navigate(new MainSchedulePage());
        }
    }
}
