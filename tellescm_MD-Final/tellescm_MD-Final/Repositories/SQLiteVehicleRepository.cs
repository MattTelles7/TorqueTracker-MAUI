using tellescm_MD_Final.Data;
using tellescm_MD_Final.Models;

namespace tellescm_MD_Final.Repositories;

public class SQLiteVehicleRepository(DatabaseService databaseService) : IVehicleRepository
{
    public async Task<IReadOnlyList<Vehicle>> GetActiveVehiclesAsync()
    {
        var database = await databaseService.GetConnectionAsync();
        var vehicles = await database.Table<Vehicle>()
            .Where(vehicle => !vehicle.IsArchived)
            .ToListAsync();

        return vehicles
            .OrderBy(vehicle => vehicle.Nickname, StringComparer.OrdinalIgnoreCase)
            .ThenBy(vehicle => vehicle.Id)
            .ToList();
    }

    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        var database = await databaseService.GetConnectionAsync();
        return await database.Table<Vehicle>()
            .Where(vehicle => vehicle.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> AddAsync(Vehicle vehicle)
    {
        var database = await databaseService.GetConnectionAsync();
        return await database.InsertAsync(vehicle);
    }
}
