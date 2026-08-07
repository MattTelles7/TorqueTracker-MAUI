using CommunityToolkit.Mvvm.Input;
using tellescm_MD_Final.Models;
using tellescm_MD_Final.Repositories;
using tellescm_MD_Final.Validation;

namespace tellescm_MD_Final.ViewModels;

public partial class AddVehicleViewModel(IVehicleRepository vehicleRepository) : BaseViewModel
{
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

        var validator = new VehicleInputValidator();

        var result = validator.Validate(
            trimmedNickname,
            trimmedMake,
            trimmedModel,
            Year,
            CurrentMileage);

        if (!result.IsValid)
        {
            ValidationMessage = result.ErrorMessage;
            return;
        }

        var parsedYear = result.Year;
        var parsedMileage = result.Mileage;

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
            await Shell.Current.DisplayAlert(
                "Vehicle saved",
                $"{vehicle.Nickname} was added.",
                "OK");

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
}
