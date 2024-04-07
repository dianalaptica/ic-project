using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class TripNotification
{
    public int Id { get; set; }

    public int TripId { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public virtual Trip Trip { get; set; } = null!;

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
}
