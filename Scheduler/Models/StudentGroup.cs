using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class StudentGroup
{
    public Guid StudentGroupId { get; set; }

    public string Code { get; set; } = null!;

    public virtual ICollection<DailyScheduleHeader> DailyScheduleHeaders { get; set; } = new List<DailyScheduleHeader>();
}
