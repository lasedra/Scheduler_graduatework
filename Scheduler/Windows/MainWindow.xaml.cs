using System.Windows;
using System.Windows.Input;
using Scheduler.Pages;

namespace Scheduler
{
    public partial class MainWindow : Window
    {
        public MainSchedulePage MainSchedulePage = new();
        public AuthorisationPage AuthorisationPage = new();
        public RegistrationPage RegistrationPage = new();
        public UserProfilePage UserProfilePage = new();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PagesFrame.Navigate(AuthorisationPage);
        }


        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            if(PagesFrame.Content != MainSchedulePage)
                PagesFrame.Navigate(MainSchedulePage);
        }

        private void UserNamePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PagesFrame.Navigate(UserProfilePage);
        }

        private void StudentGroupPageBttn_Click(object sender, RoutedEventArgs e)
        {
            PagesFrame.Navigate(new StudentGroupPage());
        }
    }
}
