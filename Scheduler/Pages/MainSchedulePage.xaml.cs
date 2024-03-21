using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Scheduler.Pages
{
    public partial class MainSchedulePage : Page
    {
        public MainSchedulePage()
        {
            InitializeComponent();
            /* TODO: Подумать над TimeSpan */
            /* TODO: Сделать .gitignore */
            /* TODO: Функционал для учителя */
            /* TODO: Сделать миграции */
            /* TODO: Дизайн норм сделать */
            /* TODO: ScheduleController */
            /* TODO: Использовать регулярки в регистрации и т.п.*/
            /* TODO: Функционал:
                        1) Оповещения МУП-а о внесении изменений в расписание
                        2) Логгирование*/

            MondayGrid.ItemsSource = GetWeekTabsQuery(new DateOnly(2020, 01, 01), new DateOnly(2020, 01, 01), "В4212");
            MondayGridHeader.Header = GetWeekTabsQuery(new DateOnly(2020, 01, 01), new DateOnly(2020, 01, 01), "В4212").First().AtDay;

            //TuesdayGrid.ItemsSource = GetWeekTabsQuery(new DateOnly(2020, 01, 02), new DateOnly(2020, 01, 02), "В4212");
            //TuesdayGridHeader.Header = GetWeekTabsQuery(new DateOnly(2020, 01, 02), new DateOnly(2020, 01, 02), "В4212").First().AtDay;

            //WednesdayGrid.ItemsSource = GetWeekTabsQuery(new DateOnly(2020, 01, 03), new DateOnly(2020, 01, 03), "В4212");
            //WednesdayGridHeader.Header = GetWeekTabsQuery(new DateOnly(2020, 01, 03), new DateOnly(2020, 01, 03), "В4212").First().AtDay;

            //ThursdayGrid.ItemsSource = GetWeekTabsQuery(new DateOnly(2020, 01, 01), new DateOnly(2020, 01, 01), "В4212");
            //ThursdayGridHeader.Header = GetWeekTabsQuery(new DateOnly(2020, 01, 01), new DateOnly(2020, 01, 01), "В4212").First().AtDay;

            //FridayGrid.ItemsSource = GetWeekTabsQuery(new DateOnly(2020, 01, 01), new DateOnly(2020, 01, 01), "В4212");
            //FridayGridHeader.Header = GetWeekTabsQuery(new DateOnly(2020, 01, 01), new DateOnly(2020, 01, 01), "В4212").First().AtDay;
        }

        public class DayTab
        {
            public string AtDay { get; set; }
            public string StudentGroup { get; set; }
            public string ClassesTimings { get; set; }
            public DateOnly OfDate { get; set; }
            public string TimeSlot { get; set; }
            public string? Subject { get; set; }
            public int? AtCabinet { get; set; }
            public string? Tutor { get; set; }
        }

        private List<DayTab> GetWeekTabsQuery(DateOnly fromDate, DateOnly toDate, string studentGroupCode)
        {
            var queryResult = from dailyScheduleBody in SchedulerDbContext.dbContext.DailyScheduleBodies
                              join dailyScheduleHeader in SchedulerDbContext.dbContext.DailyScheduleHeaders
                                  on dailyScheduleBody.DailyScheduleHeaderId equals dailyScheduleHeader.DailyScheduleHeaderId into dsHeaderGroup
                              from dailyScheduleHeader in dsHeaderGroup.DefaultIfEmpty()
                              join dayOfTheWeek in SchedulerDbContext.dbContext.DayOfTheWeeks
                                  on dailyScheduleHeader.DayOfTheWeekId equals dayOfTheWeek.DayOfTheWeekId into dowGroup
                              from dayOfTheWeek in dowGroup.DefaultIfEmpty()
                              join studentGroup in SchedulerDbContext.dbContext.StudentGroups
                                  on dailyScheduleHeader.StudentGroupId equals studentGroup.StudentGroupId into sgGroup
                              from studentGroup in sgGroup.DefaultIfEmpty()
                              join classesTimingHeader in SchedulerDbContext.dbContext.ClassesTimingHeaders
                                  on dailyScheduleHeader.ClassesTimingHeaderId equals classesTimingHeader.ClassesTimingHeaderId into cthGroup
                              from classesTimingHeader in cthGroup.DefaultIfEmpty()
                              join classesTimingBody in SchedulerDbContext.dbContext.ClassesTimingBodies
                                  on dailyScheduleBody.TimeSlotId equals classesTimingBody.TimeSlotId into ctbGroup
                              from classesTimingBody in ctbGroup.DefaultIfEmpty()
                              join subject in SchedulerDbContext.dbContext.Subjects
                                  on dailyScheduleBody.SubjectId equals subject.SubjectId into subjGroup
                              from subject in subjGroup.DefaultIfEmpty()
                              join cabinet in SchedulerDbContext.dbContext.Cabinets
                                  on dailyScheduleBody.CabinetId equals cabinet.CabinetId into cabGroup
                              from cabinet in cabGroup.DefaultIfEmpty()
                              join employee in SchedulerDbContext.dbContext.Employees
                                  on dailyScheduleBody.EmployeeId equals employee.EmployeeId into empGroup
                              from employee in empGroup.DefaultIfEmpty()
                              where dailyScheduleHeader.OfDate >= fromDate &&
                                    dailyScheduleHeader.OfDate <= toDate &&
                                    studentGroup.Code == studentGroupCode
                              select new DayTab
                              {
                                  AtDay = dayOfTheWeek.Name,
                                  StudentGroup = studentGroup.Code,
                                  ClassesTimings = classesTimingHeader.Name,
                                  OfDate = dailyScheduleHeader.OfDate,
                                  TimeSlot = $"{classesTimingBody.StartTime} - {classesTimingBody.EndTime}",
                                  Subject = subject.Name,
                                  AtCabinet = cabinet.Number,
                                  Tutor = employee.Name
                              };

            return queryResult.ToList();
        }


        private void AddRowIntoSchedule()
        {
            SchedulerDbContext.dbContext.DailyScheduleBodies.Add(new DailyScheduleBody()
            {
                DailyScheduleHeader = SchedulerDbContext.dbContext.DailyScheduleHeaders.First(c => c.OfDate == new DateOnly(2020, 01, 01)),
                LessonId = default,
                TimeSlot = SchedulerDbContext.dbContext.ClassesTimingBodies.First
                    (c => c.ClassesTimingHeaderId == SchedulerDbContext.dbContext.DailyScheduleHeaders.First(c => c.OfDate == new DateOnly(2020, 01, 01)).ClassesTimingHeaderId),
                Subject = null,
                Cabinet = null,
                Employee = null
            });
            SchedulerDbContext.dbContext.SaveChanges();
        } // Справочный метод
    }
}
