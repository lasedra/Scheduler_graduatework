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
    public partial class TutorAndSubjectPage : Page
    {
        public TutorAndSubjectPage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TutorsListView.ItemsSource = SchedulerDbContext.DbContext.Employees.ToList();
            AddTutionComboBox.ItemsSource = SchedulerDbContext.DbContext.Subjects.ToList();
        }

        // Динамическая привязка ListView.Height к RowDefinition.Height 
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TutorsListView.Height = FirstRow.ActualHeight - 50;
            TutionListView.Height = FirstRow.ActualHeight - 50;
        }

        private void UpdateTutionListView()
        {
            if (TutorsListView.SelectedItem != null)
            {
                List<Tution> tutionList = SchedulerDbContext.DbContext.Tutions
                    .Include(c => c.Subject)
                    .Where(c => c.EmployeeId == ((Employee)TutorsListView.SelectedItem).EmployeeId)
                    .ToList();

                if (tutionList.Count <= 0)
                    tutionList = new List<Tution>() {
                        new() { Subject = new() { Name = "(Отсутствуют)" } }
                    };

                TutionListView.ItemsSource = tutionList;
            }
        }

        private void TutorsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTutionListView();
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


        private void AddTutorBttn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void EditTutorBttn_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem listViewItem = FindParent<ListViewItem>((Button)sender);
            listViewItem.IsSelected = true;
            NavigationService.Navigate(new UserProfilePage((Employee)TutorsListView.SelectedItem));
        }


        private void AddTutionBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddTutionComboBox.SelectedItem != null && TutorsListView.SelectedItem != null)
                {
                    Subject selectedSubject = (Subject)AddTutionComboBox.SelectedItem;
                    Employee selectedTutor = (Employee)TutorsListView.SelectedItem;

                    if (SchedulerDbContext.DbContext.Tutions.Any(c =>
                            c.SubjectId == selectedSubject.SubjectId &&
                            c.EmployeeId == selectedTutor.EmployeeId))
                    {
                        AddTutionComboBox.SelectedItem = null;
                        UpdateTutionListView();
                        throw new Exception("Дисциплина уже внесена в список!");
                    }
                    else
                    {
                        SchedulerDbContext.DbContext.Tutions.Add(new Tution()
                        {
                            SubjectId = selectedSubject.SubjectId,
                            EmployeeId = selectedTutor.EmployeeId
                        });

                        SchedulerDbContext.DbContext.SaveChanges();

                        AddTutionComboBox.SelectedItem = null;
                        UpdateTutionListView();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DeleteTutionBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListViewItem listViewItem = FindParent<ListViewItem>((Button)sender);
                listViewItem.IsSelected = true;

                SchedulerDbContext.DbContext.Tutions
                    .Where(c =>
                        c.EmployeeId == ((Employee)TutorsListView.SelectedItem).EmployeeId &&
                        c.Subject.SubjectId == ((Tution)TutionListView.SelectedItem).SubjectId)
                    .ExecuteDelete();

                SchedulerDbContext.DbContext.SaveChanges();
                UpdateTutionListView();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
