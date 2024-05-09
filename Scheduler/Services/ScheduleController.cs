using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Scheduler.Services
{
    public class ScheduleController
    {
        public class DayTab
        {
            public DateOnly OfDate { get; set; }
            public DayOfWeek DayOfWeek { get; set; }
            public string StudentGroupCode { get; set; } = null!;
            public string TimingName { get; set; } = null!;
            public int ClassNumber { get; set; }
            public TimeOnly StartTime { get; set; }
            public TimeOnly EndTime { get; set; }
            public string TimeSlot { get { return $"{StartTime}-{EndTime}"; } }
            public Subject? Subject { get; set; }
            public Cabinet? AtCabinet { get; set; }
            public Employee? Tutor { get; set; }
        }

        public TimePeriod CurrentWeek { get; private set; } = null!;
        public string CurrentGroupCode { get; private set; } = null!;

        public List<DayTab> MondayTab { get; set; } = null!;
        public List<DayTab> TuesdayTab { get; set; } = null!;
        public List<DayTab> WednesdayTab { get; set; } = null!;
        public List<DayTab> ThursdayTab { get; set; } = null!;
        public List<DayTab> FridayTab { get; set; } = null!;
        public List<DayTab>? SaturdayTab { get; set; }


        public ScheduleController()
        {
            CreatePivotScheduleIfHasNoAny();
            SetCurrentWeek(new TimePeriod(DateOnly.FromDateTime(DateTime.Now)));
            SetCurrentGroupCode(SchedulerDbContext.DbContext.StudentGroups.First().StudentGroupCode);
            SetDayTabs();
        }


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
        public List<DayTab> GetDayTabsRange(string studentGroupCode, DateOnly fromDate, DateOnly toDate)
        {
            var query = from dailySchedule in SchedulerDbContext.DbContext.DailyScheduleBodies
                        join classesTiming in SchedulerDbContext.DbContext.ClassesTimingHeaders on dailySchedule.ClassesTimingHeaderId 
                            equals classesTiming.ClassesTimingHeaderId into timingGroup
                        from timing in timingGroup.DefaultIfEmpty()
                        join employee in SchedulerDbContext.DbContext.Employees on dailySchedule.EmployeeId 
                            equals employee.EmployeeId into employeeGroup
                        from emp in employeeGroup.DefaultIfEmpty()
                        join subject in SchedulerDbContext.DbContext.Subjects on dailySchedule.SubjectId 
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
                                SchedulerDbContext.DbContext.ClassesTimingBodies.First(c => c.ClassesTimingHeader == timing && c.ClassNumber == dailySchedule.ClassNumber).StartTime,
                            EndTime = 
                                SchedulerDbContext.DbContext.ClassesTimingBodies.First(c => c.ClassesTimingHeader == timing && c.ClassNumber == dailySchedule.ClassNumber).EndTime,
                            Tutor = SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == dailySchedule.EmployeeId),
                            Subject = SchedulerDbContext.DbContext.Subjects.First(c => c.SubjectId == dailySchedule.SubjectId),
                            AtCabinet = SchedulerDbContext.DbContext.Cabinets.First(c => c.Number == dailySchedule.CabinetNumber)
                        };
            return query.ToList();
        }
        public List<DayTab> GetCurrentWeekDayTabs(string studentGroupCode)
        {
            var query = from dailySchedule in SchedulerDbContext.DbContext.DailyScheduleBodies
                        join classesTiming in SchedulerDbContext.DbContext.ClassesTimingHeaders on dailySchedule.ClassesTimingHeaderId
                            equals classesTiming.ClassesTimingHeaderId into timingGroup
                        from timing in timingGroup.DefaultIfEmpty()
                        join employee in SchedulerDbContext.DbContext.Employees on dailySchedule.EmployeeId
                            equals employee.EmployeeId into employeeGroup
                        from emp in employeeGroup.DefaultIfEmpty()
                        join subject in SchedulerDbContext.DbContext.Subjects on dailySchedule.SubjectId
                            equals subject.SubjectId into subjectGroup
                        from subj in subjectGroup.DefaultIfEmpty()

                        where dailySchedule.OfDate >= CurrentWeek.WeekStart 
                              && 
                              dailySchedule.OfDate <= CurrentWeek.WeekEnd
                              &&
                              dailySchedule.StudentGroupCode == studentGroupCode
                        select new DayTab
                        {
                            OfDate = dailySchedule.OfDate,
                            DayOfWeek = dailySchedule.OfDate.DayOfWeek,
                            StudentGroupCode = dailySchedule.StudentGroupCode,
                            TimingName = timing.Name,
                            ClassNumber = dailySchedule.ClassNumber,
                            StartTime = SchedulerDbContext.DbContext.ClassesTimingBodies
                                .First(c => c.ClassesTimingHeader == timing && c.ClassNumber == dailySchedule.ClassNumber).StartTime,
                            EndTime =SchedulerDbContext.DbContext.ClassesTimingBodies
                                .First(c => c.ClassesTimingHeader == timing && c.ClassNumber == dailySchedule.ClassNumber).EndTime,
                            Tutor = SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == dailySchedule.EmployeeId),
                            Subject = SchedulerDbContext.DbContext.Subjects.First(c => c.SubjectId == dailySchedule.SubjectId),
                            AtCabinet = SchedulerDbContext.DbContext.Cabinets.First(c => c.Number == dailySchedule.CabinetNumber)
                        };

            return query.ToList();
        }
        public List<DayTab> GetCurrentWeekDayTab(string studentGroupCode, DayOfWeek dayOfWeek)
        {
            var query = from dailySchedule in SchedulerDbContext.DbContext.DailyScheduleBodies
                        join classesTiming in SchedulerDbContext.DbContext.ClassesTimingHeaders on dailySchedule.ClassesTimingHeaderId
                            equals classesTiming.ClassesTimingHeaderId into timingGroup
                        from timing in timingGroup.DefaultIfEmpty()
                        join employee in SchedulerDbContext.DbContext.Employees on dailySchedule.EmployeeId
                            equals employee.EmployeeId into employeeGroup
                        from emp in employeeGroup.DefaultIfEmpty()
                        join subject in SchedulerDbContext.DbContext.Subjects on dailySchedule.SubjectId
                            equals subject.SubjectId into subjectGroup
                        from subj in subjectGroup.DefaultIfEmpty()

                        where dailySchedule.OfDate >= CurrentWeek.WeekStart && dailySchedule.OfDate <= CurrentWeek.WeekEnd
                              &&
                              dailySchedule.StudentGroupCode == studentGroupCode
                              &&
                              dailySchedule.OfDate.DayOfWeek == dayOfWeek
                        select new DayTab
                        {
                            OfDate = dailySchedule.OfDate,
                            DayOfWeek = dailySchedule.OfDate.DayOfWeek,
                            StudentGroupCode = dailySchedule.StudentGroupCode,
                            TimingName = timing.Name,
                            ClassNumber = dailySchedule.ClassNumber,
                            StartTime = SchedulerDbContext.DbContext.ClassesTimingBodies
                                .First(c => c.ClassesTimingHeader == timing && c.ClassNumber == dailySchedule.ClassNumber).StartTime,
                            EndTime = SchedulerDbContext.DbContext.ClassesTimingBodies
                                .First(c => c.ClassesTimingHeader == timing && c.ClassNumber == dailySchedule.ClassNumber).EndTime,
                            Tutor = SchedulerDbContext.DbContext.Employees.First(c => c.EmployeeId == dailySchedule.EmployeeId),
                            Subject = SchedulerDbContext.DbContext.Subjects.First(c => c.SubjectId == dailySchedule.SubjectId),
                            AtCabinet = SchedulerDbContext.DbContext.Cabinets.First(c => c.Number == dailySchedule.CabinetNumber)
                        };

            return query.ToList();
        }

        public void CreatePivotScheduleIfHasNoAny()
        {
            if (!SchedulerDbContext.DbContext.DailyScheduleHeaders.Any())
            {
                List<StudentGroup> groups = SchedulerDbContext.DbContext.StudentGroups.ToList();
                SetCurrentWeek(new TimePeriod(DateOnly.FromDateTime(DateTime.Now)));
                SetCurrentGroupCode(groups.First().StudentGroupCode);

                foreach (StudentGroup group in groups)
                    CreatePivotSchedule(CurrentWeek.WeekStart, group.StudentGroupCode);

                SetDayTabs();

                MessageBox.Show("Для каждой группы создан шаблон расписания на текущую неделю", "Поздравляю!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void CreatePivotSchedule(DateOnly onDate,string studentGroupCode)
        {
            // Шапка
            for (int d = 0; d <= 4; d++)
            {
                SchedulerDbContext.DbContext.DailyScheduleHeaders.Add(new DailyScheduleHeader()
                {
                    StudentGroupCode = studentGroupCode,
                    OfDate = onDate.AddDays(d),
                });
            }
            SchedulerDbContext.DbContext.SaveChanges();

            // Табличная часть
            for (int d = 0; d <= 4; d++) // Дней в неделе
            {
                for (int i = 0; i <= 3; i++) // Уроков в день
                {
                    SchedulerDbContext.DbContext.DailyScheduleBodies.Add(new DailyScheduleBody()
                    {
                        DailyScheduleHeader = SchedulerDbContext.DbContext.DailyScheduleHeaders
                            .First(c => c.OfDate == onDate.AddDays(d) && c.StudentGroupCode == studentGroupCode),
                        ClassNumber = i + 1,
                        ClassesTimingHeaderId = SchedulerDbContext.DbContext.ClassesTimingHeaders
                            .First(c => c.Name == "Основное").ClassesTimingHeaderId,
                        Subject = null,
                        Employee = null,
                        CabinetNumber = null
                    });
                }
            }
            SchedulerDbContext.DbContext.SaveChanges();
        }

        public void AddSchedule(string studentGroupCode)
        {
            DateOnly nextWeek = CurrentWeek.WeekStart.AddDays(7);
            CreatePivotSchedule(nextWeek, studentGroupCode);
            MessageBox.Show("Расписание на следующую неделю успешно создано!", "Поздравляю!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void SetCurrentWeek(TimePeriod timePeriod)
        {
            CurrentWeek = timePeriod;
        }

        public void SetCurrentGroupCode(string studentGroupCode)
        {
            CurrentGroupCode = studentGroupCode;
        }

        public void SetDayTabs()
        {
            MondayTab = GetCurrentWeekDayTab(CurrentGroupCode, DayOfWeek.Monday);
            TuesdayTab = GetCurrentWeekDayTab(CurrentGroupCode, DayOfWeek.Tuesday);
            WednesdayTab = GetCurrentWeekDayTab(CurrentGroupCode, DayOfWeek.Wednesday);
            ThursdayTab = GetCurrentWeekDayTab(CurrentGroupCode, DayOfWeek.Thursday);
            FridayTab = GetCurrentWeekDayTab(CurrentGroupCode, DayOfWeek.Friday);
        }
    }
}
