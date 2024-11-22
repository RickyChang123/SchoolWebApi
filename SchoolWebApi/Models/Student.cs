using System;
using System.Collections.Generic;

namespace SchoolWebApi.Models;

public partial class Student
{
    public Guid Sid { get; set; }

    public string SName { get; set; } = null!;

    public string SEmail { get; set; } = null!;

    public string SPassword { get; set; } = null!;

    public DateTime? SStart { get; set; }

    public DateTime? SEnd { get; set; }

    public virtual ICollection<SelectClass> SelectClasses { get; set; } = new List<SelectClass>();
}
