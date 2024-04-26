using Microsoft.EntityFrameworkCore;
using Scheduler.Models;
using Scheduler.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Scheduler.Services
{
    public class ScheduleController
    {
        public TimePeriod CurrentWeek { get; set; }
        public string CurrentGroupCode { get; set; }

        public List<DayTab> MondayTab { get; set; }
        public List<DayTab> TuesdayTab { get; set; }
        public List<DayTab> WednesdayTab { get; set; }
        public List<DayTab> ThursdayTab { get; set; }
        public List<DayTab> FridayTab { get; set; }
        public List<DayTab>? SaturdayTab { get; set; }

        public ScheduleController()
        {
            #warning В первую очередь текущую неделю
            CurrentWeek = new TimePeriod(DateOnly.FromDateTime(DateTime.Now.Date));

            CreatePivotScheduleIfHasNoAny();
            CurrentGroupCode = SchedulerDbContext.dbContext.DailyScheduleHeaders.First().StudentGroupCode;
            SetDayTabs();
        }

        public List<DayTab> GetDayTabOf(DateOnly fromDate, DateOnly toDate, string studentGroupCode, DayOfWeek dayOfWeek)
        {
            #region SqlRawSelect
            //FormattableString select = $@"
            //        SELECT
            //        ""DailySchedule_body"".""OfDate"" AS ""Of date"",
            //     to_char(""DailySchedule_body"".""OfDate"", 'Day') AS ""Day of week"",
            //        ""DailySchedule_body"".""StudentGroupCode"" AS ""Student group"",
            //     ""ClassesTiming_header"".""Name"" as ""Timing name"",
            //        ""DailySchedule_body"".""ClassNumber"" AS ""Class number"",
            //     ""Employee"".""Name"" as ""Tutor"",
            //     ""Subject"".""Name"" as ""Subject"",
            //     ""DailySchedule_body"".""CabinetNumber"" as ""At cabinet""
            //    FROM ""DailySchedule_body""
            //        LEFT JOIN ""ClassesTiming_header"" ON ""DailySchedule_body"".""ClassesTiming_header_ID"" = ""ClassesTiming_header"".""ClassesTiming_header_ID""
            //     LEFT JOIN ""Employee"" ON ""DailySchedule_body"".""Employee_ID"" = ""Employee"".""Employee_ID""
            //        LEFT JOIN ""Subject"" ON ""DailySchedule_body"".""Subject_ID"" = ""Subject"".""Subject_ID""
            //    WHERE
            //     ""OfDate"" BETWEEN '{fromDate}' AND '{toDate}'
            //     AND
            //     ""StudentGroupCode"" = '{studentGroupCode}';";

            //return SchedulerDbContext.dbContext.DailyScheduleBodies.FromSqlInterpolated(select).ToList();
            #endregion

            var query = from dailySchedule in SchedulerDbContext.dbContext.DailyScheduleBodies
                        join classesTiming in SchedulerDbContext.dbContext.ClassesTimingHeaders on dailySchedule.ClassesTimingHeaderId 
                            equals classesTiming.ClassesTimingHeaderId into timingGroup
                        from timing in timingGroup.DefaultIfEmpty()
                        join employee in SchedulerDbContext.dbContext.Employees on dailySchedule.EmployeeId 
                            equals employee.EmployeeId into employeeGroup
                        from emp in employeeGroup.DefaultIfEmpty()
                        join subject in SchedulerDbContext.dbContext.Subjects on dailySchedule.SubjectId 
                            equals subject.SubjectId into subjectGroup
                        from subj in subjectGroup.DefaultIfEmpty()

                        where dailySchedule.OfDate >= fromDate && dailySchedule.OfDate <= toDate
                              && 
                              dailySchedule.StudentGroupCode == studentGroupCode
                        select new DayTab
                        {
                            OfDate = dailySchedule.OfDate,
                            DayOfWeek = dailySchedule.OfDate.DayOfWeek,
                            StudentGroupCode = dailySchedule.StudentGroupCode,
                            TimingName = timing.Name,
                            ClassNumber = dailySchedule.ClassNumber,
                            StartTime = 
                                SchedulerDbContext.dbContext.ClassesTimingBodies.First(c => c.ClassesTimingHeader == timing && c.ClassNumber == dailySchedule.ClassNumber).StartTime,
                            EndTime = 
                                SchedulerDbContext.dbContext.ClassesTimingBodies.First(c => c.ClassesTimingHeader == timing && c.ClassNumber == dailySchedule.ClassNumber).EndTime,
                            Tutor = SchedulerDbContext.dbContext.Employees.First(c => c.EmployeeId == dailySchedule.EmployeeId),
                            Subject = SchedulerDbContext.dbContext.Subjects.First(c => c.SubjectId == dailySchedule.SubjectId),
                            AtCabinet = SchedulerDbContext.dbContext.Cabinets.First(c => c.Number == dailySchedule.CabinetNumber)
                        };
            return query.Where(c => c.DayOfWeek == dayOfWeek).ToList();
        }

        public void CreatePivotScheduleIfHasNoAny()
        {
            // Pivot расписание на каждый день текущей недели, для каждой группы студентов
            if (!SchedulerDbContext.dbContext.DailyScheduleHeaders.Any(c => c.OfDate > CurrentWeek.SchoolyearStart))
            {
                StudentGroupPage studentGroupPage = new StudentGroupPage();


                List<StudentGroup> groups = SchedulerDbContext.dbContext.StudentGroups.ToList();

                // Шапки
                foreach (StudentGroup group in groups)
                {
                    for (int d = 0; d <= 4; d++)
                    {
                        SchedulerDbContext.dbContext.DailyScheduleHeaders.Add(new DailyScheduleHeader()
                        {
                            StudentGroupCode = group.StudentGroupCode,
                            OfDate = CurrentWeek.WeekStart.AddDays(d),
                        });
                    }
                }
                SchedulerDbContext.dbContext.SaveChanges();

                // Табличные части
                foreach (StudentGroup group in groups)
                {
                    for (int d = 0; d <= 4; d++) // Дней в неделе
                    {
                        for (int i = 0; i <= 3; i++) // Уроков в день
                        {
                            SchedulerDbContext.dbContext.DailyScheduleBodies.Add(new DailyScheduleBody()
                            {
                                DailyScheduleHeader = SchedulerDbContext.dbContext.DailyScheduleHeaders.First
                                    (c => c.OfDate == CurrentWeek.WeekStart.AddDays(d) && c.StudentGroupCode == group.StudentGroupCode),
                                ClassNumber = i + 1,
                                ClassesTimingHeaderId = SchedulerDbContext.dbContext.ClassesTimingHeaders.First(c => c.Name == "Основное").ClassesTimingHeaderId,
                                Subject = null,
                                Employee = null,
                                CabinetNumber = null
                            });
                        }
                    }
                }
                SchedulerDbContext.dbContext.SaveChanges();
                MessageBox.Show("Для каждой учебной группы созданы пустые расписания на текущую неделю", "Поздравляю!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void AddSchedule(string studentGroupCode)
        {
            DateOnly nextWeek = CurrentWeek.WeekStart.AddDays(7);

            // Pivot расписание на каждый день текущей недели
            for (int d = 0; d <= 4; d++)
            {
                SchedulerDbContext.dbContext.DailyScheduleHeaders.Add(new DailyScheduleHeader()
                {
                    StudentGroupCode = studentGroupCode,
                    OfDate = nextWeek.AddDays(d),
                });
            }
            SchedulerDbContext.dbContext.SaveChanges();


            for (int d = 0; d <= 4; d++) // Дней в неделе
            {
                for (int i = 0; i <= 3; i++) // Уроков в день
                {
                    SchedulerDbContext.dbContext.DailyScheduleBodies.Add(new DailyScheduleBody()
                    {
                        DailyScheduleHeader = SchedulerDbContext.dbContext.DailyScheduleHeaders.First(c => c.OfDate == nextWeek.AddDays(d)),
                        ClassNumber = i + 1,
                        ClassesTimingHeaderId = SchedulerDbContext.dbContext.ClassesTimingHeaders.First(c => c.Name == "Основное").ClassesTimingHeaderId,
                        Subject = null,
                        Employee = null,
                        CabinetNumber = null
                    });
                }
                SchedulerDbContext.dbContext.SaveChanges();
                CurrentWeek = new TimePeriod(SchedulerDbContext.dbContext.DailyScheduleHeaders.Max(c => c.OfDate));
                CurrentGroupCode = SchedulerDbContext.dbContext.DailyScheduleHeaders.First(c => c.OfDate == CurrentWeek.TodayDate).StudentGroupCode;
            }

            MessageBox.Show("Расписание на следующую неделю успешно создано!", "Поздравляю!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SetDayTabs()
        {
            MondayTab = GetDayTabOf(CurrentWeek.WeekStart, CurrentWeek.WeekEnd, CurrentGroupCode, DayOfWeek.Monday);
            TuesdayTab = GetDayTabOf(CurrentWeek.WeekStart, CurrentWeek.WeekEnd, CurrentGroupCode, DayOfWeek.Tuesday);
            WednesdayTab = GetDayTabOf(CurrentWeek.WeekStart, CurrentWeek.WeekEnd, CurrentGroupCode, DayOfWeek.Wednesday);
            ThursdayTab = GetDayTabOf(CurrentWeek.WeekStart, CurrentWeek.WeekEnd, CurrentGroupCode, DayOfWeek.Thursday);
            FridayTab = GetDayTabOf(CurrentWeek.WeekStart, CurrentWeek.WeekEnd, CurrentGroupCode, DayOfWeek.Friday);
        }


        public struct TimePeriod
        {
            public DateOnly TodayDate { get; set; }
            public DateOnly WeekStart { get; set; }
            public DateOnly WeekEnd { get; set; }
            public DateOnly SchoolyearStart { get; set; }
            public DateOnly SchoolyearEnd { get; set; }

            public TimePeriod(DateOnly todayDate)
            {
                    TodayDate = todayDate;
                switch (TodayDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        {
                            WeekStart = TodayDate.AddDays(-6);
                            WeekEnd = TodayDate;
                            break;
                        }
                    case DayOfWeek.Monday:
                        {
                            WeekStart = TodayDate;
                            WeekEnd = TodayDate.AddDays(6);
                            break;
                        }
                    default:
                        {
                            WeekStart = TodayDate.AddDays(-((int)TodayDate.DayOfWeek - 1));
                            WeekEnd = WeekStart.AddDays(6);
                            break; 
                        }
                }
                if (TodayDate.Month >= 9)
                {
                    SchoolyearStart = new DateOnly(TodayDate.Year, 9, 1);
                    SchoolyearEnd = new DateOnly(TodayDate.Year + 1, 7, 30);
                }
                else
                {
                    SchoolyearStart = new DateOnly(TodayDate.Year - 1, 9, 1);
                    SchoolyearEnd = new DateOnly(TodayDate.Year, 7, 30);
                }
            }

            public string GetWeekSpan() 
            { return $"{WeekStart:dd.MM.yyyy}  -  {WeekEnd:dd.MM.yyyy}"; }
            public string GetSchoolyearSpan()
            { return $"{SchoolyearStart.Year}-{SchoolyearEnd.Year}"; }
        }
        public class DayTab
        {
            public DateOnly OfDate { get; set; }
            public DayOfWeek DayOfWeek { get; set; }
            public string StudentGroupCode { get; set; }
            public string TimingName { get; set; }
            public int ClassNumber { get; set; }
            public TimeOnly StartTime { get; set; }
            public TimeOnly EndTime { get; set; }
            public string TimeSlot { get { return $"{StartTime}-{EndTime}"; } }
            public Subject? Subject { get; set; }
            public Cabinet? AtCabinet { get; set; }
            public Employee? Tutor { get; set; }
        }
    }
}
