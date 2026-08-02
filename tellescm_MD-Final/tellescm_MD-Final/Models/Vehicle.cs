using SQLite;

namespace tellescm_MD_Final.Models;

public class Vehicle
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Nickname { get; set; } = string.Empty;

    public int Year { get; set; }

    [NotNull]
    public string Make { get; set; } = string.Empty;

    [NotNull]
    public string Model { get; set; } = string.Empty;

    public int CurrentMileage { get; set; }

    public bool IsArchived { get; set; }
}
