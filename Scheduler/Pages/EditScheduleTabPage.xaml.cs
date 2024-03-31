﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scheduler.Pages
{
    public partial class EditScheduleTabPage : Page
    {
        public EditScheduleTabPage()
        {
            InitializeComponent();
        }

        private void GoBackBttn_Click(object sender, RoutedEventArgs e)
        {
            if(NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ((DockPanel)Window.GetWindow(this).FindName("MenuPanel")).Visibility = Visibility.Collapsed;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            ((DockPanel)Window.GetWindow(this).FindName("MenuPanel")).Visibility = Visibility.Visible;
        }
    }
}
