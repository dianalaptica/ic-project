using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class AppliedForGuide
{
    public int UserId { get; set; }

    public byte[] IdentityCard { get; set; } = null!;

    public bool IsApproved { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
