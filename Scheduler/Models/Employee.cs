﻿using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class Employee
{
    public Guid EmployeeId { get; set; }

    public bool WorkingStatus { get; set; }

    public long? TgBotChatId { get; set; }

    public bool Role { get; set; }

    public string Name { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public long Phone { get; set; }

    public string? EMail { get; set; }

    public virtual ICollection<DailyScheduleBody> DailyScheduleBodies { get; set; } = new List<DailyScheduleBody>();

    public virtual ICollection<Tution> Tutions { get; set; } = new List<Tution>();
}
