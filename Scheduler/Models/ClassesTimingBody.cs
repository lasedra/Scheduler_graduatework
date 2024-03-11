using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class ClassesTimingBody
{
    public Guid ClassesTimingHeaderId { get; set; }

    public Guid TimeSlotId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public virtual ClassesTimingHeader ClassesTimingHeader { get; set; } = null!;

    public virtual ICollection<DailyScheduleBody> DailyScheduleBodies { get; set; } = new List<DailyScheduleBody>();
}
