using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Scheduler.Pages
{
    public partial class StudentGroupPage : Page
    {
        public StudentGroupPage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StudentsGroupsListView.ItemsSource = SchedulerDbContext.DbContext.StudentGroups.ToList();
            AddStudyingComboBox.ItemsSource = SchedulerDbContext.DbContext.Subjects.ToList();
        }
        

        // Динамическая привязка ListView.Height к RowDefinition.Height 
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StudentsGroupsListView.Height = FirstRow.ActualHeight - 50;
            StudyingListView.Height = FirstRow.ActualHeight - 50;
        }

        private void UpdateStudyingListView()
        {
            if(StudentsGroupsListView.SelectedItem != null)
            {
                List<Studying> studyingList = SchedulerDbContext.DbContext.Studyings
                    .Include(c => c.Subject)
                    .Where(c => c.StudentGroupCode == ((StudentGroup)StudentsGroupsListView.SelectedItem).StudentGroupCode)
                    .ToList();

                if (studyingList.Count <= 0)
                    studyingList = new List<Studying>() { 
                        new() { Subject = new() { Name = "(Отсутствуют)" } } 
                    };

                StudyingListView.ItemsSource = studyingList;
            }
        }

        private void StudentsGroupsListView_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            AddNewGroupBttn.Visibility = Visibility.Visible;
            EditGroupRowStackPanel.Visibility = Visibility.Collapsed;
            StudentGroupCodeTxtBox.Text = string.Empty;
            SpecializationTxtBox.Text = string.Empty;
            UpdateStudyingListView();
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


        private void AddNewGroupBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(StudentGroupCodeTxtBox.Text))
                {
                    if (SchedulerDbContext.DbContext.StudentGroups.Any(c => c.StudentGroupCode == StudentGroupCodeTxtBox.Text.Trim()))
                        throw new Exception("Такая группа уже существует!");
                    else
                    {
                        string newGroupCode = SpecializationTxtBox.Text.Trim();
                        string? newGroupSpecialization = !string.IsNullOrEmpty(SpecializationTxtBox.Text.Trim()) ? SpecializationTxtBox.Text.Trim() : null;

                        SchedulerDbContext.DbContext.StudentGroups.Add(new StudentGroup{
                            StudentGroupCode = newGroupCode,
                            Specialization = newGroupSpecialization
                        });
                        SchedulerDbContext.DbContext.SaveChanges(SchedulerDbContext.ChangeLogLevel.Secondary, "StudentGroup added");

                        ScheduleController scheduleController = new();
                        scheduleController.CreatePivotSchedule(scheduleController.CurrentWeek.WeekStart, newGroupCode);

                        StudentGroupCodeTxtBox.Text = string.Empty;
                        SpecializationTxtBox.Text = string.Empty;
                        StudentsGroupsListView.ItemsSource = SchedulerDbContext.DbContext.StudentGroups.ToList();
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DeleteGroupBttn_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                if (StudentsGroupsListView.SelectedItem != null)
                {
                    StudentGroup groupToRemove = SchedulerDbContext.DbContext.StudentGroups
                        .First(c => c.StudentGroupCode == ((StudentGroup)StudentsGroupsListView.SelectedItem).StudentGroupCode);

                    var result = MessageBox.Show(
                        $"Вы уверены, что хотите удалить из базы группу {groupToRemove.StudentGroupCode} ?" +
                        $"\nЭто приведёт к удалению всей зависимой информации.",
                        "Минуточку",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Работает только при наличии ограничения у связанных таблиц - "ON DELETE CASCADE"
                        SchedulerDbContext.DbContext.StudentGroups
                            .Where(c => c.StudentGroupCode == groupToRemove.StudentGroupCode)
                            .ExecuteDelete();

                        StudentsGroupsListView.ItemsSource = SchedulerDbContext.DbContext.StudentGroups.ToList();
                        AddNewGroupBttn.Visibility = Visibility.Visible;
                        EditGroupRowStackPanel.Visibility = Visibility.Collapsed;
                        StudentGroupCodeTxtBox.Text = string.Empty;
                        SpecializationTxtBox.Text = string.Empty;
                        MessageBox.Show("Группа успешно удалена!");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void EditGroupBttn_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem listViewItem = FindParent<ListViewItem>((Button)sender);
            listViewItem.IsSelected = true;

            StudentGroup groupToEdit = (StudentGroup)StudentsGroupsListView.SelectedItem;
            StudentGroupCodeTxtBox.Text = groupToEdit.StudentGroupCode;
            SpecializationTxtBox.Text = groupToEdit?.Specialization;

            AddNewGroupBttn.Visibility = Visibility.Collapsed;
            EditGroupRowStackPanel.Visibility = Visibility.Visible;
        }

        private void CommitEditGroupBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StudentsGroupsListView.SelectedItem != null)
                {
                    StudentGroup groupToEdit = ((StudentGroup)StudentsGroupsListView.SelectedItem);
                    string newGroupCode = StudentGroupCodeTxtBox.Text.Trim();
                    string newGroupSpecialization = SpecializationTxtBox.Text.Trim();

                    if (groupToEdit.StudentGroupCode != newGroupCode)
                    {
                        var result = MessageBox.Show(
                            $"Вы уверены, что хотите изменить номер группы {groupToEdit.StudentGroupCode} ?" +
                            $"\nЭто приведёт к изменению всей зависимой информации.",
                            "Минуточку",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            // Работает только при наличии ограничения у связанных таблиц - "ON UPDATE CASCADE"
                            SchedulerDbContext.DbContext.StudentGroups
                                .Where(c => c.StudentGroupCode == groupToEdit.StudentGroupCode)
                                .ExecuteUpdate(c =>
                                    c.SetProperty(c => c.StudentGroupCode, newGroupCode));

                            StudentsGroupsListView.ItemsSource = SchedulerDbContext.DbContext.StudentGroups.ToList();
                            AddNewGroupBttn.Visibility = Visibility.Visible;
                            EditGroupRowStackPanel.Visibility = Visibility.Collapsed;
                            StudentGroupCodeTxtBox.Text = string.Empty;
                            SpecializationTxtBox.Text = string.Empty;
                        }
                    }
                    if (groupToEdit.Specialization != newGroupSpecialization)
                    {
                        SchedulerDbContext.DbContext.StudentGroups
                            .Where(c => c.StudentGroupCode == groupToEdit.StudentGroupCode)
                            .ExecuteUpdate(c =>
                                c.SetProperty(c => c.Specialization, newGroupSpecialization));

                        // Сохранение и подгрузка изменений не ключевых полей
                        SchedulerDbContext.DbContext.ChangeTracker.Clear();

                        StudentsGroupsListView.ItemsSource = SchedulerDbContext.DbContext.StudentGroups.ToList();
                        AddNewGroupBttn.Visibility = Visibility.Visible;
                        EditGroupRowStackPanel.Visibility = Visibility.Collapsed;
                        StudentGroupCodeTxtBox.Text = string.Empty;
                        SpecializationTxtBox.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CancelEditGroupBttn_Click(object sender, RoutedEventArgs e)
        {
            StudentGroupCodeTxtBox.Text = string.Empty;
            SpecializationTxtBox.Text = string.Empty;
            AddNewGroupBttn.Visibility = Visibility.Visible;
            EditGroupRowStackPanel.Visibility = Visibility.Collapsed;
        }


        private void AddNewStudyingBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddStudyingComboBox.SelectedItem != null && StudentsGroupsListView.SelectedItem != null)
                {
                    Subject selectedSubject = (Subject)AddStudyingComboBox.SelectedItem;
                    StudentGroup selectedGroup = (StudentGroup)StudentsGroupsListView.SelectedItem;

                    if (SchedulerDbContext.DbContext.Studyings.Any(c => 
                            c.SubjectId == selectedSubject.SubjectId && 
                            c.StudentGroupCode == selectedGroup.StudentGroupCode))
                    {
                        AddStudyingComboBox.SelectedItem = null;
                        UpdateStudyingListView();
                        throw new Exception("Дисциплина уже внесена в список!");
                    }
                    else
                    {
                        SchedulerDbContext.DbContext.Studyings.Add(new Studying(){
                            SubjectId = selectedSubject.SubjectId,
                            StudentGroupCode = selectedGroup.StudentGroupCode
                        });

                        SchedulerDbContext.DbContext.SaveChanges(SchedulerDbContext.ChangeLogLevel.Intermediate,"Stydying added");

                        AddStudyingComboBox.SelectedItem = null;
                        UpdateStudyingListView();
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DeleteStudyingBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListViewItem listViewItem = FindParent<ListViewItem>((Button)sender);
                listViewItem.IsSelected = true;

                SchedulerDbContext.DbContext.Studyings
                    .Where(c =>
                        c.StudentGroupCode == ((StudentGroup)StudentsGroupsListView.SelectedItem).StudentGroupCode &&
                        c.Subject.SubjectId == ((Studying)StudyingListView.SelectedItem).SubjectId)
                    .ExecuteDelete();

                SchedulerDbContext.DbContext.SaveChanges(SchedulerDbContext.ChangeLogLevel.Intermediate,"Studying deleted");
                UpdateStudyingListView();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
