using System;
using System.Collections.Generic;

namespace SchoolWebApi.Models;

public partial class Teacher
{
    public Guid Tid { get; set; }

    public string TName { get; set; } = null!;

    public string? TEmail { get; set; }

    public string? TOffice { get; set; }

    public DateTime? TStart { get; set; }

    public DateTime? TEnd { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
