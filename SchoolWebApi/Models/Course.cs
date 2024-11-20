using System;
using System.Collections.Generic;

namespace SchoolWebApi.Models;

public partial class Course
{
    public Guid Cid { get; set; }

    public string? CourseName { get; set; }

    public string? CourseDescription { get; set; }

    public string Credits { get; set; } = null!;

    public DateTime? CStart { get; set; }

    public DateTime? CEnd { get; set; }

    public string? Location { get; set; }

    public Guid? TeacherId { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public string? Day { get; set; }

    public virtual ICollection<SelectClass> SelectClasses { get; set; } = new List<SelectClass>();

    public virtual Teacher? Teacher { get; set; }
}
