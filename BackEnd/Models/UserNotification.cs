using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class UserNotification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public bool IsRead { get; set; }

    public virtual TripNotification Notification { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
