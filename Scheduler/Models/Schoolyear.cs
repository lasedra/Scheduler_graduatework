using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class Schoolyear
{
    public Guid SchoolyearId { get; set; }

    public string? Years { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<DailyScheduleHeader> DailyScheduleHeaders { get; set; } = new List<DailyScheduleHeader>();
}
