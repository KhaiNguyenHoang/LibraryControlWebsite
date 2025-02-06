using System;
using System.Collections.Generic;

namespace LibaryControlWebsite.Models;

public partial class Waitlist
{
    public int WaitlistId { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? Status { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}