using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class DailyScheduleBody
{
    public Guid DailyScheduleHeaderId { get; set; }

    public Guid LessonId { get; set; }

    public Guid TimeSlotId { get; set; }

    public Guid? SubjectId { get; set; }

    public Guid? CabinetId { get; set; }

    public Guid? EmployeeId { get; set; }

    public virtual Cabinet? Cabinet { get; set; }

    public virtual DailyScheduleHeader DailyScheduleHeader { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual Subject? Subject { get; set; }

    public virtual ClassesTimingBody TimeSlot { get; set; } = null!;
}
