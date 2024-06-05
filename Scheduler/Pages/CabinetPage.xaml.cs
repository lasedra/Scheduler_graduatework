using Scheduler.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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


        // Динамическая привязка ListView.Height к RowDefinition.Height 
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e) => CabinetListView.Height = FirstRow.ActualHeight - 50;

        private void CabinetListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddCabinetBttn.Visibility = Visibility.Visible;
            EditCabinetStackPanel.Visibility = Visibility.Collapsed;
            NumberTxtBox.Text = string.Empty;
            NameTxtBox.Text = string.Empty;
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;

            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }


        private void AddCabinetBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(NumberTxtBox.Text))
                {
                    if (SchedulerDbContext.DbContext.Cabinets.Any(c => c.Number == NumberTxtBox.Text.Trim()))
                        throw new Exception("Такой кабинет уже существует!");
                    else
                    {
                        SchedulerDbContext.DbContext.Cabinets.Add(new Cabinet()
                        {
                            Number = NumberTxtBox.Text.Trim(),
                            Name = !string.IsNullOrEmpty(NameTxtBox.Text) ? NameTxtBox.Text.Trim() : null
                        });
                        SchedulerDbContext.DbContext.SaveChanges();

                        NumberTxtBox.Text = string.Empty;
                        NameTxtBox.Text = string.Empty;
                        CabinetListView.ItemsSource = SchedulerDbContext.DbContext.Cabinets.ToList();
                    }   
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void EditCabinetBttn_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem listViewItem = FindParent<ListViewItem>((Button)sender);
            listViewItem.IsSelected = true;

            Cabinet cabinetToEdit = (Cabinet)CabinetListView.SelectedItem;
            NumberTxtBox.Text = cabinetToEdit.Number;
            NameTxtBox.Text = cabinetToEdit?.Name;

            AddCabinetBttn.Visibility = Visibility.Collapsed;
            EditCabinetStackPanel.Visibility = Visibility.Visible;
        }

        private void CommitEditCabinetBttn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteCabinetBttn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelEditCabinetBttn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
