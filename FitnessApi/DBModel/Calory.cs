using System;
using System.Collections.Generic;

namespace FitnessApi.DBModel;

public partial class Calory
{
    public int Calorieid { get; set; }

    public int? Userid { get; set; }

    public string Fooditem { get; set; } = null!;

    public int Calories { get; set; }

    public string? Mealtype { get; set; }

    public DateTime? Indate { get; set; }

    public virtual User? User { get; set; }
}
