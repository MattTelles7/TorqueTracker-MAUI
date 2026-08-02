using CommunityToolkit.Mvvm.Input;
using tellescm_MD_Final.Models;
using tellescm_MD_Final.Repositories;
using tellescm_MD_Final.Views;

namespace tellescm_MD_Final.ViewModels;

public partial class DashboardViewModel(IVehicleRepository vehicleRepository) : BaseViewModel
{
    private Vehicle? currentVehicle;

    public Vehicle? CurrentVehicle
    {
        get => currentVehicle;
        set
        {
            if (SetProperty(ref currentVehicle, value))
            {
                OnPropertyChanged(nameof(HasVehicle));
                OnPropertyChanged(nameof(HasNoVehicle));
            }
        }
    }

    public bool HasVehicle => CurrentVehicle is not null;

    public bool HasNoVehicle => CurrentVehicle is null;

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
            CurrentVehicle = (await vehicleRepository.GetActiveVehiclesAsync()).FirstOrDefault();
        }
        catch (Exception)
        {
            ErrorMessage = "Vehicle information could not be loaded. Please try again.";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private Task OpenVehiclesAsync() => Shell.Current.GoToAsync("//Vehicles");

    [RelayCommand]
    private Task AddVehicleAsync() => Shell.Current.GoToAsync(nameof(AddVehiclePage));
}
