using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class StudentGroup
{
    public string StudentGroupCode { get; set; } = null!;

    public string? Specialization { get; set; }

    public virtual ICollection<DailyScheduleHeader> DailyScheduleHeaders { get; set; } = new List<DailyScheduleHeader>();

    public virtual ICollection<Studying> Studyings { get; set; } = new List<Studying>();
}
