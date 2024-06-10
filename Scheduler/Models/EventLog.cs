﻿using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class EventLog
{
    public int EventId { get; set; }

    public DateTime DateTime { get; set; }

    public string Level { get; set; } = null!;

    public string Message { get; set; } = null!;

    public bool IsUpdatedByApp { get; set; }
}
