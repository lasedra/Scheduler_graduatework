using System;

namespace Scheduler.Services
{
    public class TimePeriod
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
        { return $"{WeekStart:dd.MM.yyyy} – {WeekEnd:dd.MM.yyyy}"; }
        public string GetSchoolyearSpan()
        { return $"{SchoolyearStart.Year}-{SchoolyearEnd.Year}"; }
    }
}
