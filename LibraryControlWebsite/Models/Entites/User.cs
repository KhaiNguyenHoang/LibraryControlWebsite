using System;
using System.Collections.Generic;

namespace LibaryControlWebsite.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Address { get; set; }

    public string UserType { get; set; } = null!;

    /// <summary>
    /// Bcrypt hashed password
    /// </summary>
    public string PasswordHash { get; set; } = null!;

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Bookreview> Bookreviews { get; set; } = new List<Bookreview>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Waitlist> Waitlists { get; set; } = new List<Waitlist>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}