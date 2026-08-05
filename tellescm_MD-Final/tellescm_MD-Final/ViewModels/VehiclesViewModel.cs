using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using tellescm_MD_Final.Models;
using tellescm_MD_Final.Repositories;
using tellescm_MD_Final.Views;

namespace tellescm_MD_Final.ViewModels;

public partial class VehiclesViewModel(IVehicleRepository vehicleRepository) : BaseViewModel
{
    public ObservableCollection<Vehicle> Vehicles { get; } = [];

    public List<string> SortOptions { get; } =
    [
        "Name (A-Z)",
        "Mileage (High-Low)"
    ];

    private string? selectedSortOption;

    public string? SelectedSortOption
    {
        get => selectedSortOption;
        set
        {
            if (SetProperty(ref selectedSortOption, value))
            {
                SortVehicles();
            }
        }
    }

    public int VehicleCount => Vehicles.Count;


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

            OnPropertyChanged(nameof(VehicleCount));
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


    private void SortVehicles()
    {
        IEnumerable<Vehicle> sortedVehicles = Vehicles;

        if (SelectedSortOption == "Name (A-Z)")
        {
            sortedVehicles = Vehicles
                .OrderBy(v => $"{v.Make} {v.Model}");
        }
        else if (SelectedSortOption == "Mileage (High-Low)")
        {
            sortedVehicles = Vehicles
                .OrderByDescending(v => v.CurrentMileage);
        }

        Vehicles.Clear();

        foreach (var vehicle in sortedVehicles)
        {
            Vehicles.Add(vehicle);
        }
    }


    [RelayCommand]
    private Task AddVehicleAsync() => Shell.Current.GoToAsync(nameof(AddVehiclePage));
}