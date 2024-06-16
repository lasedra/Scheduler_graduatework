using Scheduler.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Xceed.Words.NET;
using Xceed.Document.NET;
using System.Linq;
using Scheduler.Models;
using System.Collections.Generic;
using Microsoft.Win32;
using Scheduler.Services;
using System.Reflection;

// TODO: Неймспейсы проверить
namespace Scheduler.UserControls
{
    public partial class ScheduleUserControl : UserControl
    {
        public ScheduleUserControl()
        {
            InitializeComponent();
        }

        private void ScheduleEditingBttn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new EditingPage());
        }


        private void CreateDocumentBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScheduleController scheduleController = ((MainSchedulePage)((Grid)this.Parent).Parent).ScheduleController;

                MessageBoxResult result = MessageBoxResult.None;
                if(scheduleController.HasEmptyCells())
                {
                    result = MessageBox.Show(
                        $"Вы уверены, что хотите создать документ?" +
                        $"\nРасписание не заполнено полностью.",
                        "Минуточку",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);
                }

                if(result == MessageBoxResult.None || result == MessageBoxResult.Yes)
                {
                    using (var templateDoc = DocX.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Scheduler.Resources.schedule.doc-template.docx")))
                    {
                        ReplaceKeywordWithValue(templateDoc, "[studentGroupCode]", scheduleController.CurrentGroupCode);
                        ReplaceKeywordWithValue(templateDoc, "[weekSpan]", scheduleController.CurrentWeek.GetWeekSpan());
                        ReplaceKeywordWithValue(templateDoc, "[createdAtDate]", DateTime.Now.ToString("dd.MM.yyyy"));

                        var dayTabs = scheduleController.GetDayTabs();
                        StringReplaceTextOptions options = new();

                        int tableCounter = 0;
                        foreach (List<ScheduleController.DayTab> dayTab in dayTabs)
                        {
                            int lessonCounter = 1;
                            for (int i = 1; i <= 4; i += 1) // Строки в таблицах документа
                            {
                                Subject? subject = dayTab.First(c => c.ClassNumber == lessonCounter).Subject;
                                Employee? tutor = dayTab.First(c => c.ClassNumber == lessonCounter).Tutor;
                                Cabinet? cabinet = dayTab.First(c => c.ClassNumber == lessonCounter).AtCabinet;

                                options.SearchValue = "[Subject]";
                                options.NewValue = subject != null ? subject.Name : string.Empty;
                                templateDoc.Tables[tableCounter].Rows[i].Cells[1].ReplaceText(options);

                                options.SearchValue = "[Tutor]";
                                options.NewValue = tutor != null ? tutor.Name : string.Empty;
                                templateDoc.Tables[tableCounter].Rows[i].Cells[1].ReplaceText(options);

                                options.SearchValue = "[Cabinet]";
                                options.NewValue = cabinet != null ? cabinet.Number : string.Empty;
                                templateDoc.Tables[tableCounter].Rows[i].Cells[1].ReplaceText(options);
                                lessonCounter++;
                            }
                            tableCounter++;
                        }

                        SaveFileDialog sfDialog = new() { DefaultExt = ".docx", Filter = "Word Documents|*.docx" };
                        if (sfDialog.ShowDialog() == true)
                        {
                            templateDoc.SaveAs(sfDialog.FileName);
                            MessageBox.Show("Документ успешно сохранён!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка при создании документа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ReplaceKeywordWithValue(DocX document, string keyword, string value)
        {
            foreach (var paragraph in document.Paragraphs)
            {
                if (paragraph.Text.Contains(keyword))
                {
                    paragraph.ReplaceText(keyword, value);
                }
            }
        }

        // Динамическое выравнивание строк по высоте внутри DataGrid
        private void MondayGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateRowHeight(sender as DataGrid);
        }
        private void UpdateRowHeight(DataGrid dataGrid)
        {
            if (dataGrid != null && dataGrid.Items.Count > 0)
            {
                double rowHeight = dataGrid.ActualHeight / dataGrid.Items.Count;

                foreach (var item in dataGrid.Items)
                {
                    var row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem(item);
                    if (row != null)
                    {
                        row.Height = rowHeight - 7.9;
                    }
                }
            }
        }
    }
}
