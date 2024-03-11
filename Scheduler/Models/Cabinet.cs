using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class Cabinet
{
    public Guid CabinetId { get; set; }

    public int Number { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<DailyScheduleBody> DailyScheduleBodies { get; set; } = new List<DailyScheduleBody>();
}
