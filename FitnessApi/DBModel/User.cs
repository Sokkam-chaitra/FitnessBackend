using System;
using System.Collections.Generic;

namespace FitnessApi.DBModel;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? Gender { get; set; }

    public string? ActivityLevel { get; set; }

    public DateTime? CreatedTime { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Calory> Calories { get; set; } = new List<Calory>();

    public virtual ICollection<Tblworkout> Tblworkouts { get; set; } = new List<Tblworkout>();
}
