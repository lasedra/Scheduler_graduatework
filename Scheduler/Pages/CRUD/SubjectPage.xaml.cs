using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace Scheduler.Pages
{
    public partial class SubjectPage : Page
    {
        public SubjectPage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SubjectsListView.ItemsSource = SchedulerDbContext.DbContext.Subjects.ToList();
        }

        // Динамическая привязка ListView.Height к RowDefinition.Height 
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
            => SubjectsListView.Height = FirstRow.ActualHeight - 50;

        private void SubjectsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddSubjectBttn.Visibility = Visibility.Visible;
            EditSubjectStackPanel.Visibility = Visibility.Collapsed;
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


        private void AddSubjectBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(NameTxtBox.Text))
                {
                    if (SchedulerDbContext.DbContext.Subjects.Any(c => c.Name == NameTxtBox.Text.Trim()))
                        throw new Exception("Такой предмет уже существует!");
                    else
                    {
                        SchedulerDbContext.DbContext.Subjects.Add(new Subject()
                        {
                            SubjectId = default,
                            Name = NameTxtBox.Text.Trim()
                        });
                        SchedulerDbContext.DbContext.SaveChanges(SchedulerDbContext.ChangeLogLevel.Primary,"Subject added");

                        NameTxtBox.Text = string.Empty;
                        SubjectsListView.ItemsSource = SchedulerDbContext.DbContext.Subjects.ToList();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DeleteSubjectBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SubjectsListView.SelectedItem != null)
                {
                    Subject subjectToRemove = SchedulerDbContext.DbContext.Subjects
                        .First(c => c.SubjectId == ((Subject)SubjectsListView.SelectedItem).SubjectId);

                    var result = MessageBox.Show(
                        $"Вы уверены, что хотите удалить из базы предмет {subjectToRemove.Name} ?" +
                        $"\nЭто приведёт к удалению всей зависимой информации.",
                        "Минуточку",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        List<DailyScheduleBody> bodiesToUnbind = SchedulerDbContext.DbContext.DailyScheduleBodies.Where(c => c.SubjectId == subjectToRemove.SubjectId).ToList();
                        bodiesToUnbind.ForEach(c => {
                            c.Subject = null;
                            c.Employee = null;
                            c.CabinetNumber = null;
                        });
                        SchedulerDbContext.DbContext.SaveChanges(SchedulerDbContext.ChangeLogLevel.Primary, "Subject deleted");

                        // Работает только при наличии ограничения у связанных таблиц - "ON DELETE CASCADE"
                        SchedulerDbContext.DbContext.Subjects
                            .Where(c => c.SubjectId == subjectToRemove.SubjectId)
                            .ExecuteDelete();

                        SubjectsListView.ItemsSource = SchedulerDbContext.DbContext.Subjects.ToList();
                        AddSubjectBttn.Visibility = Visibility.Visible;
                        EditSubjectStackPanel.Visibility = Visibility.Collapsed;
                        NameTxtBox.Text = string.Empty;
                        MessageBox.Show("Предмет успешно удалён!");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        private void EditSubjectBttn_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem listViewItem = FindParent<ListViewItem>((Button)sender);
            listViewItem.IsSelected = true;

            Subject subjectToEdit = (Subject)SubjectsListView.SelectedItem;
            NameTxtBox.Text = subjectToEdit.Name;

            AddSubjectBttn.Visibility = Visibility.Collapsed;
            EditSubjectStackPanel.Visibility = Visibility.Visible;
        }

        private void CommitEditSubjectBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SubjectsListView.SelectedItem != null)
                {
                    Subject subjectToEdit = ((Subject)SubjectsListView.SelectedItem);
                    string newSubjectName = NameTxtBox.Text.Trim();

                    if (subjectToEdit.Name != newSubjectName)
                    {
                        var result = MessageBox.Show(
                            $"Вы уверены, что хотите изменить наименование предмета {subjectToEdit.Name} ?" +
                            $"\nЭто приведёт к изменению всей зависимой информации.",
                            "Минуточку",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            SchedulerDbContext.DbContext.Subjects
                                .Where(c => c.SubjectId == subjectToEdit.SubjectId)
                                .ExecuteUpdate(c =>
                                    c.SetProperty(c => c.Name, newSubjectName));

                            // Сохранение и подгрузка изменений не ключевых полей
                            SchedulerDbContext.DbContext.ChangeTracker.Clear();

                            SubjectsListView.ItemsSource = SchedulerDbContext.DbContext.Subjects.ToList();
                            AddSubjectBttn.Visibility = Visibility.Visible;
                            EditSubjectStackPanel.Visibility = Visibility.Collapsed;
                            NameTxtBox.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CancelEditSubjectBttn_Click(object sender, RoutedEventArgs e)
        {
            AddSubjectBttn.Visibility = Visibility.Visible;
            EditSubjectStackPanel.Visibility = Visibility.Collapsed;
            NameTxtBox.Text = string.Empty;
        }
    }
}
