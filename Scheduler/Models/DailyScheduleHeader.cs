using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class DailyScheduleHeader
{
    public Guid DailyScheduleHeaderId { get; set; }

    public Guid? StudentGroupId { get; set; }

    public Guid ClassesTimingHeaderId { get; set; }

    public DateOnly OfDate { get; set; }

    public Guid SchoolyearId { get; set; }

    public virtual ClassesTimingHeader ClassesTimingHeader { get; set; } = null!;

    public virtual ICollection<DailyScheduleBody> DailyScheduleBodies { get; set; } = new List<DailyScheduleBody>();

    public virtual Schoolyear Schoolyear { get; set; } = null!;

    public virtual StudentGroup? StudentGroup { get; set; }
}
