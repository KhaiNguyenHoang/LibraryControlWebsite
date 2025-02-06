using System;
using System.Collections.Generic;

namespace LibaryControlWebsite.Models;

public partial class Bookcopy
{
    public int CopyId { get; set; }

    public int BookId { get; set; }

    public int CopyNumber { get; set; }

    public string? Conditions { get; set; }

    public string? Location { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual Book Book { get; set; } = null!;
}
