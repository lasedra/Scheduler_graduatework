using Microsoft.Extensions.Configuration;
using Scheduler.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Scheduler.Windows
{
    public partial class ConnectionPickWindow : Window
    {
        #region Disallow_closing
        // Prep stuff needed to remove close button on window
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        #endregion

        public string ReturnString { get; private set; }

        public ConnectionPickWindow()
        {
            InitializeComponent();
            ConnectionsComboBox.ItemsSource = SchedulerDbContext.AppConfig.GetRequiredSection("ConnectionStrings").GetChildren().ToList();
        }

        private void ConnectionsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => ReturnString = ((IConfigurationSection)ConnectionsComboBox.SelectedItem).Value;

        private void SubmitBttn_Click(object sender, RoutedEventArgs e)
            => this.Close();
    }
}
