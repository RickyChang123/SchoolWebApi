using System;
using System.Collections.Generic;

namespace SchoolWebApi.Models;

public partial class SelectClass
{
    public Guid Eid { get; set; }

    public Guid StudentId { get; set; }

    public Guid CourseId { get; set; }

    public DateTime? EStart { get; set; }

    public DateTime? EEnd { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
