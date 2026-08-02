using SQLite;

namespace tellescm_MD_Final.Models;

public class ServiceRecord
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int VehicleId { get; set; }

    [NotNull]
    public string ServiceType { get; set; } = string.Empty;

    public DateTime ServiceDate { get; set; }

    public int Mileage { get; set; }

    public double Cost { get; set; }

    public string Provider { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;
}
