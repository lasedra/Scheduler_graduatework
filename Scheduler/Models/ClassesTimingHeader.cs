using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class ClassesTimingHeader
{
    public Guid ClassesTimingHeaderId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ClassesTimingBody> ClassesTimingBodies { get; set; } = new List<ClassesTimingBody>();

    public virtual ICollection<DailyScheduleHeader> DailyScheduleHeaders { get; set; } = new List<DailyScheduleHeader>();
}
