using tellescm_MD_Final.Models;

namespace tellescm_MD_Final.Repositories;

public interface IVehicleRepository
{
    Task<IReadOnlyList<Vehicle>> GetActiveVehiclesAsync();

    Task<Vehicle?> GetByIdAsync(int id);

    Task<int> AddAsync(Vehicle vehicle);

    Task ArchiveAysnc(int id);
}
