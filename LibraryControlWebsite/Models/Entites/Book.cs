using System;
using System.Collections.Generic;

namespace LibaryControlWebsite.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Publisher { get; set; }

    public short? PublishYear { get; set; }

    public string Isbn { get; set; } = null!;

    public int TotalCopies { get; set; }

    public int AvailableCopies { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Bookcopy> Bookcopies { get; set; } = new List<Bookcopy>();

    public virtual ICollection<Bookreview> Bookreviews { get; set; } = new List<Bookreview>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Waitlist> Waitlists { get; set; } = new List<Waitlist>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
