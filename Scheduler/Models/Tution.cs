using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class Tution
{
    public Guid TutionRowId { get; set; }

    public Guid SubjectId { get; set; }

    public Guid EmployeeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<DailyScheduleBody> DailyScheduleBodies { get; set; } = new List<DailyScheduleBody>();

    public virtual Employee Employee { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
