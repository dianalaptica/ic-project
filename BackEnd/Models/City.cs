using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual ICollection<AppliedForGuide> AppliedForGuides { get; set; } = new List<AppliedForGuide>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
