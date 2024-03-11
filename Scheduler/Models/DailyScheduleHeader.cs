using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class DailyScheduleHeader
{
    public int DayOfTheWeekId { get; set; }

    public Guid DailyScheduleHeaderId { get; set; }

    public Guid StudentGroupId { get; set; }

    public Guid ClassesTimingHeaderId { get; set; }

    public DateOnly OfDate { get; set; }

    public virtual ClassesTimingHeader ClassesTimingHeader { get; set; } = null!;

    public virtual ICollection<DailyScheduleBody> DailyScheduleBodies { get; set; } = new List<DailyScheduleBody>();

    public virtual DayOfTheWeek DayOfTheWeek { get; set; } = null!;

    public virtual StudentGroup StudentGroup { get; set; } = null!;
}
