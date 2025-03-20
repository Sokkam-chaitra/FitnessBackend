using System;
using System.Collections.Generic;

namespace FitnessApi.DBModel;

public partial class Tblworkout
{
    public int Id { get; set; }

    public string WorkoutName { get; set; } = null!;

    public string? Description { get; set; }

    public int? Maxcalories { get; set; }

    public int? Duration { get; set; }

    public int? Userid { get; set; }

    public DateTime? Indate { get; set; }

    public virtual User? User { get; set; }
}
