using Scheduler.Models;
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
            CurrentWeek = new TimePeriod();
            CreatePivotScheduleIfHasNoAny();
            MondayTab = GetDaytabs(CurrentWeek.WeekStart, CurrentWeek.WeekEnd, DayOfWeek.Monday, null);
            TuesdayTab = GetDaytabs(CurrentWeek.WeekStart, CurrentWeek.WeekEnd, DayOfWeek.Tuesday, null);
            WednesdayTab = GetDaytabs(CurrentWeek.WeekStart, CurrentWeek.WeekEnd, DayOfWeek.Wednesday, null);
            ThursdayTab = GetDaytabs(CurrentWeek.WeekStart, CurrentWeek.WeekEnd, DayOfWeek.Thursday, null);
            FridayTab = GetDaytabs(CurrentWeek.WeekStart, CurrentWeek.WeekEnd, DayOfWeek.Friday, null);
        }

        public List<DayTab> GetDaytabs(DateOnly fromDate, DateOnly toDate, DayOfWeek dayOfWeek, string? studentGroupCode)
        {
            var queryResult = from dailyScheduleBody in SchedulerDbContext.dbContext.DailyScheduleBodies
                              join dailyScheduleHeader in SchedulerDbContext.dbContext.DailyScheduleHeaders
                                  on dailyScheduleBody.DailyScheduleHeaderId equals dailyScheduleHeader.DailyScheduleHeaderId into dsHeaderGroup
                              from dailyScheduleHeader in dsHeaderGroup.DefaultIfEmpty()
                              join schoolyear in SchedulerDbContext.dbContext.Schoolyears
                                  on dailyScheduleHeader.SchoolyearId equals schoolyear.SchoolyearId into syGroup
                              from schoolyear in syGroup.DefaultIfEmpty()
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
                                    studentGroup.Code == studentGroupCode &&
                                    dailyScheduleHeader.OfDate.DayOfWeek == dayOfWeek
                              select new DayTab
                              {
                                  Year = schoolyear.Years,
                                  DayOfWeek = dailyScheduleHeader.OfDate.DayOfWeek,
                                  StudentGroup = studentGroup.Code,
                                  ClassesTimings = classesTimingHeader.Name,
                                  OfDate = dailyScheduleHeader.OfDate,
                                  TimeSlot = $"{classesTimingBody.StartTime} - {classesTimingBody.EndTime}",
                                  StartTime = classesTimingBody.StartTime,
                                  EndTime = classesTimingBody.EndTime,
                                  Subject = subject.Name,
                                  AtCabinet = cabinet.Number,
                                  Tutor = employee.Name
                              };

            return queryResult.OrderBy(c => c.StartTime).ToList();
        }

        public void CreatePivotScheduleIfHasNoAny()
        {
            if(!SchedulerDbContext.dbContext.Schoolyears.Any(c => c.Years == CurrentWeek.GetSchoolyearSpan()))
            {
                // Current schoolyear registered
                SchedulerDbContext.dbContext.Schoolyears.Add(new Schoolyear()
                {
                    SchoolyearId = default,
                    Years = CurrentWeek.GetSchoolyearSpan(),
                    StartDate = CurrentWeek.SchoolyearStart,
                    EndDate = CurrentWeek.SchoolyearEnd
                }); SchedulerDbContext.dbContext.SaveChanges();

                // Empty pivot schedule registered to this schoolyear
                for (int d = 0; d <= 4; d++) // Days in a week
                {
                    SchedulerDbContext.dbContext.DailyScheduleHeaders.Add(new DailyScheduleHeader()
                    {
                        DailyScheduleHeaderId = default,
                        #warning _
                        StudentGroup = null,
                        ClassesTimingHeader = SchedulerDbContext.dbContext.ClassesTimingHeaders.First(c => c.Name == "Основное"),
                        OfDate = CurrentWeek.WeekStart.AddDays(d),
                        Schoolyear = SchedulerDbContext.dbContext.Schoolyears.First(c => c.Years == CurrentWeek.GetSchoolyearSpan()),
                    });
                }
                SchedulerDbContext.dbContext.SaveChanges();


                // Empty schedule tabs registered for this week
                TimeOnly[] timings = new TimeOnly[]{
                    new TimeOnly(9, 00),
                        new TimeOnly(10, 30),
                    new TimeOnly(10, 40),
                        new TimeOnly(12, 10),
                    new TimeOnly(13, 00),
                        new TimeOnly(14, 30),
                    new TimeOnly(14, 40),
                        new TimeOnly(16, 10)
                };
                int t;
                for (int d = 0; d <= 4; d++) // Days in a week
                {
                    t = 0;
                    for (int i = 0; i <= 3; i++) // Lessons per day
                    {
                        SchedulerDbContext.dbContext.DailyScheduleBodies.Add(new DailyScheduleBody()
                        {
                            DailyScheduleHeader = SchedulerDbContext.dbContext.DailyScheduleHeaders.First(c => c.OfDate == CurrentWeek.WeekStart.AddDays(d)),
                            LessonId = default,
                            TimeSlot = SchedulerDbContext.dbContext.ClassesTimingBodies.First(c =>
                                    c.ClassesTimingHeaderId == (SchedulerDbContext.dbContext.DailyScheduleHeaders.First(c =>
                                        c.OfDate == CurrentWeek.WeekStart.AddDays(d)).ClassesTimingHeaderId) &&
                                        c.StartTime == timings[t] &&
                                        c.EndTime == timings[t + 1]),
                            TutionRow = null,
                            Subject = null,
                            Employee = null,
                            Cabinet = null,
                        });

                        t = t + 2;
                    }
                    SchedulerDbContext.dbContext.SaveChanges();
                }

                MessageBox.Show("Вы стали первым пользователем, создавшим расписание!\nВозмите с полки пирожок!", "Поздравляю!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        public struct TimePeriod
        {
            public DateOnly TodayDate { get; set; }
            public DateOnly WeekStart { get; set; }
            public DateOnly WeekEnd { get; set; }
            public DateOnly SchoolyearStart { get; set; }
            public DateOnly SchoolyearEnd { get; set; }

            public TimePeriod()
            {
                TodayDate = DateOnly.FromDateTime(DateTime.Now.Date);
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
                if (TodayDate.Month != 8)
                {
                    if (TodayDate.Month >= 9) {
                        SchoolyearStart = new DateOnly(TodayDate.Year, 9, 1);
                        SchoolyearEnd = new DateOnly(TodayDate.Year + 1, 7, 30);
                    } else {
                        SchoolyearStart = new DateOnly(TodayDate.Year - 1, 9, 1);
                        SchoolyearEnd = new DateOnly(TodayDate.Year, 7, 30);
                    }
                }
                else
                    throw new Exception("It's vacation... \nGo touch some grass, bruh");
            }

            public string GetWeekSpan() 
            { return $"{WeekStart} - {WeekEnd}"; }
            public string GetSchoolyearSpan()
            { return $"{SchoolyearStart.Year}-{SchoolyearEnd.Year}"; }
        }
        public class DayTab
        {
            public string Year { get; set; }
            public DayOfWeek DayOfWeek { get; set; }
            public string StudentGroup { get; set; }
            public string ClassesTimings { get; set; }
            public DateOnly OfDate { get; set; }
            public string TimeSlot { get; set; }
            public TimeOnly StartTime { get; set; }
            public TimeOnly EndTime { get; set; }
            public string? Subject { get; set; }
            public int? AtCabinet { get; set; }
            public string? Tutor { get; set; }
        }
    }
}
