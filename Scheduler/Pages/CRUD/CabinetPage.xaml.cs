using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
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
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e) 
            => CabinetListView.Height = FirstRow.ActualHeight - 50;

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
                        SchedulerDbContext.DbContext.SaveChanges(SchedulerDbContext.ChangeLogLevel.Secondary, "Cabinet added");

                        NumberTxtBox.Text = string.Empty;
                        NameTxtBox.Text = string.Empty;
                        CabinetListView.ItemsSource = SchedulerDbContext.DbContext.Cabinets.ToList();
                    }   
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DeleteCabinetBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CabinetListView.SelectedItem != null)
                {
                    Cabinet cabinetToRemove = SchedulerDbContext.DbContext.Cabinets
                        .First(c => c.Number == ((Cabinet)CabinetListView.SelectedItem).Number);

                    var result = MessageBox.Show(
                        $"Вы уверены, что хотите удалить из базы кабинет {cabinetToRemove.Number} - {cabinetToRemove.Name} ?" +
                        $"\nЭто приведёт к удалению всей зависимой информации.",
                        "Минуточку",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        List<DailyScheduleBody> bodiesToUnbind = SchedulerDbContext.DbContext.DailyScheduleBodies.Where(c => c.CabinetNumber == cabinetToRemove.Number).ToList();
                        bodiesToUnbind.ForEach(c => {
                            c.Subject = null;
                            c.Employee = null;
                            c.CabinetNumber = null;
                        });
                        SchedulerDbContext.DbContext.SaveChanges(SchedulerDbContext.ChangeLogLevel.Secondary, "Cabinet deleted");

                        SchedulerDbContext.DbContext.Cabinets
                            .Where(c => c.Number == cabinetToRemove.Number)
                            .ExecuteDelete();

                        CabinetListView.ItemsSource = SchedulerDbContext.DbContext.Cabinets.ToList();
                        AddCabinetBttn.Visibility = Visibility.Visible;
                        EditCabinetStackPanel.Visibility = Visibility.Collapsed;
                        NumberTxtBox.Text = string.Empty;
                        NameTxtBox.Text = string.Empty;
                        MessageBox.Show("Кабинет успешно удалён!");
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
            try
            {
                if (CabinetListView.SelectedItem != null)
                {
                    Cabinet cabinetToEdit = ((Cabinet)CabinetListView.SelectedItem);
                    string newCabinetNumber = NumberTxtBox.Text.Trim();
                    string newCabinetName = NameTxtBox.Text.Trim();

                    if (cabinetToEdit.Number != newCabinetNumber)
                    {
                        var result = MessageBox.Show(
                            $"Вы уверены, что хотите изменить номер группы {cabinetToEdit.Number} - {cabinetToEdit.Name} ?" +
                            $"\nЭто приведёт к изменению всей зависимой информации.",
                            "Минуточку",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            // Работает только при наличии ограничения у связанных таблиц - "ON UPDATE CASCADE"
                            SchedulerDbContext.DbContext.Cabinets
                                .Where(c => c.Number == cabinetToEdit.Number)
                                .ExecuteUpdate(c =>
                                    c.SetProperty(c => c.Number, newCabinetNumber));

                            CabinetListView.ItemsSource = SchedulerDbContext.DbContext.Cabinets.ToList();
                            AddCabinetBttn.Visibility = Visibility.Visible;
                            EditCabinetStackPanel.Visibility = Visibility.Collapsed;
                            NumberTxtBox.Text = string.Empty;
                            NameTxtBox.Text = string.Empty;
                        }
                    }
                    if (cabinetToEdit.Name != newCabinetName)
                    {
                        SchedulerDbContext.DbContext.Cabinets
                            .Where(c => c.Number == cabinetToEdit.Number)
                            .ExecuteUpdate(c =>
                                c.SetProperty(c => c.Name, newCabinetName));

                        // Сохранение и подгрузка изменений не ключевых полей
                        SchedulerDbContext.DbContext.ChangeTracker.Clear();

                        CabinetListView.ItemsSource = SchedulerDbContext.DbContext.Cabinets.ToList();
                        AddCabinetBttn.Visibility = Visibility.Visible;
                        EditCabinetStackPanel.Visibility = Visibility.Collapsed;
                        NumberTxtBox.Text = string.Empty;
                        NameTxtBox.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CancelEditCabinetBttn_Click(object sender, RoutedEventArgs e)
        {
            AddCabinetBttn.Visibility = Visibility.Visible;
            EditCabinetStackPanel.Visibility = Visibility.Collapsed;
            NumberTxtBox.Text = string.Empty;
            NameTxtBox.Text = string.Empty;
        }
    }
}
