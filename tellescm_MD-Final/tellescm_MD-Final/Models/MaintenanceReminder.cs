using SQLite;

namespace tellescm_MD_Final.Models;

public class MaintenanceReminder
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int VehicleId { get; set; }

    [NotNull]
    public string ServiceType { get; set; } = string.Empty;

    public DateTime? DueDate { get; set; }

    public int? DueMileage { get; set; }

    public int LeadTimeDays { get; set; }

    [NotNull]
    public string Status { get; set; } = string.Empty;
}
