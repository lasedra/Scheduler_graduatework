using System.Windows;
using Scheduler.Pages;
using Scheduler.Services;

namespace Scheduler
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PagesFrame.Navigate(new RegistrationPage());
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
