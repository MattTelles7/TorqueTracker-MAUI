using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using tellescm_MD_Final.Models;
using tellescm_MD_Final.Repositories;
using tellescm_MD_Final.Views;

namespace tellescm_MD_Final.ViewModels;

public partial class VehiclesViewModel(IVehicleRepository vehicleRepository) : BaseViewModel
{
    public ObservableCollection<Vehicle> Vehicles { get; } = [];

    [RelayCommand]
    public async Task LoadAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;
            ErrorMessage = string.Empty;
            var vehicles = await vehicleRepository.GetActiveVehiclesAsync();

            Vehicles.Clear();
            foreach (var vehicle in vehicles)
            {
                Vehicles.Add(vehicle);
            }
        }
        catch (Exception)
        {
            ErrorMessage = "Vehicles could not be loaded. Please try again.";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private Task AddVehicleAsync() => Shell.Current.GoToAsync(nameof(AddVehiclePage));
}
