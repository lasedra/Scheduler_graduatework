using System;
using System.Collections.Generic;

namespace Scheduler.Models;

public partial class TutionLog
{
    public int StatementId { get; set; }

    public Guid SubjectId { get; set; }

    public Guid EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
