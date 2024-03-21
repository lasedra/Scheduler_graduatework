﻿using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class Subject
{
    public Guid SubjectId { get; set; }

    public string Name { get; set; } = null!;

    public int ClassesNumber { get; set; }

    public virtual ICollection<DailyScheduleBody> DailyScheduleBodies { get; set; } = new List<DailyScheduleBody>();

    public virtual ICollection<Tution> Tutions { get; set; } = new List<Tution>();
}
