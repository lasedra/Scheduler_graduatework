using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class ScheduleView
{
    public string? AtDay { get; set; }

    public string? StudentGroup { get; set; }

    public string? ClassesTimings { get; set; }

    public DateOnly? OfDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public string? Subject { get; set; }

    public int? AtCabinet { get; set; }

    public string? Tutor { get; set; }
}
