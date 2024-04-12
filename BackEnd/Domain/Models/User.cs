namespace BackEnd.Domain.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public DateTime TokenCreated { get; set; }

    public DateTime TokenExpires { get; set; }

    public byte[]? ProfilePicture { get; set; }

    public int RoleId { get; set; }

    public string Email { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
