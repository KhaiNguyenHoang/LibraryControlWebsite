using System;
using System.Collections.Generic;

namespace LibaryControlWebsite.Models;

public partial class Loan
{
    public int LoanId { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public DateOnly LoanDate { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string? Status { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual ICollection<Fine> Fines { get; set; } = new List<Fine>();

    public virtual User User { get; set; } = null!;
}