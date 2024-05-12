namespace BackEnd.Domain.Models;

public partial class Trip
{
    public int Id { get; set; }

    public int GuideId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int MaxTourists { get; set; }

    public string Address { get; set; } = null!;

    public byte[] Image { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual AppliedForGuide Guide { get; set; } = null!;

    public virtual ICollection<TripNotification> TripNotifications { get; set; } = new List<TripNotification>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
