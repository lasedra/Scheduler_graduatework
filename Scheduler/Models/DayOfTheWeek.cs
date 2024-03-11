using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class DayOfTheWeek
{
    public int DayOfTheWeekId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DailyScheduleHeader> DailyScheduleHeaders { get; set; } = new List<DailyScheduleHeader>();
}
