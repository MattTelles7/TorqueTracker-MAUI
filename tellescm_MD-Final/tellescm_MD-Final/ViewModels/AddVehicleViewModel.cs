using CommunityToolkit.Mvvm.Input;
using tellescm_MD_Final.Models;
using tellescm_MD_Final.Repositories;

namespace tellescm_MD_Final.ViewModels;

public partial class AddVehicleViewModel(IVehicleRepository vehicleRepository) : BaseViewModel
{
    private const int EarliestVehicleYear = 1886;

    private string nickname = string.Empty;
    private string year = string.Empty;
    private string make = string.Empty;
    private string model = string.Empty;
    private string currentMileage = string.Empty;
    private string validationMessage = string.Empty;

    public string Nickname
    {
        get => nickname;
        set => SetProperty(ref nickname, value);
    }

    public string Year
    {
        get => year;
        set => SetProperty(ref year, value);
    }

    public string Make
    {
        get => make;
        set => SetProperty(ref make, value);
    }

    public string Model
    {
        get => model;
        set => SetProperty(ref model, value);
    }

    public string CurrentMileage
    {
        get => currentMileage;
        set => SetProperty(ref currentMileage, value);
    }

    public string ValidationMessage
    {
        get => validationMessage;
        set
        {
            if (SetProperty(ref validationMessage, value))
            {
                OnPropertyChanged(nameof(HasValidationMessage));
            }
        }
    }

    public bool HasValidationMessage => !string.IsNullOrWhiteSpace(ValidationMessage);

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (IsBusy)
        {
            return;
        }

        var trimmedNickname = Nickname.Trim();
        var trimmedMake = Make.Trim();
        var trimmedModel = Model.Trim();

        if (!TryValidate(trimmedNickname, trimmedMake, trimmedModel, out var parsedYear, out var parsedMileage))
        {
            return;
        }

        try
        {
            IsBusy = true;
            ValidationMessage = string.Empty;

            var vehicle = new Vehicle
            {
                Nickname = trimmedNickname,
                Year = parsedYear,
                Make = trimmedMake,
                Model = trimmedModel,
                CurrentMileage = parsedMileage
            };

            await vehicleRepository.AddAsync(vehicle);
            await Shell.Current.DisplayAlert("Vehicle saved", $"{vehicle.Nickname} was added.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception)
        {
            ValidationMessage = "The vehicle could not be saved. Please try again.";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private bool TryValidate(
        string trimmedNickname,
        string trimmedMake,
        string trimmedModel,
        out int parsedYear,
        out int parsedMileage)
    {
        parsedYear = 0;
        parsedMileage = 0;

        if (string.IsNullOrWhiteSpace(trimmedNickname))
        {
            ValidationMessage = "Enter a nickname for the vehicle.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(trimmedMake))
        {
            ValidationMessage = "Enter the vehicle make.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(trimmedModel))
        {
            ValidationMessage = "Enter the vehicle model.";
            return false;
        }

        var latestVehicleYear = DateTime.Today.Year + 1;
        if (!int.TryParse(Year.Trim(), out parsedYear) ||
            parsedYear < EarliestVehicleYear ||
            parsedYear > latestVehicleYear)
        {
            ValidationMessage = $"Enter a year from {EarliestVehicleYear} through {latestVehicleYear}.";
            return false;
        }

        if (!int.TryParse(CurrentMileage.Trim(), out parsedMileage) || parsedMileage < 0)
        {
            ValidationMessage = "Enter a current mileage of zero or greater.";
            return false;
        }

        return true;
    }
}
