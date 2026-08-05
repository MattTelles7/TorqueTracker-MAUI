namespace tellescm_MD_Final.Validation;

public class VehicleInputValidator
{
    public const int EarliestVehicleYear = 1886;

    public VehicleValidationResult Validate(
        string nickname,
        string make,
        string model,
        string year,
        string mileage)
    {
        nickname = nickname?.Trim() ?? "";
        make = make?.Trim() ?? "";
        model = model?.Trim() ?? "";
        year = year?.Trim() ?? "";
        mileage = mileage?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(nickname))
        {
            return Invalid("Enter a nickname for the vehicle.");
        }

        if (string.IsNullOrWhiteSpace(make))
        {
            return Invalid("Enter the vehicle make.");
        }

        if (string.IsNullOrWhiteSpace(model))
        {
            return Invalid("Enter the vehicle model.");
        }

        int latestVehicleYear = DateTime.Today.Year + 1;

        if (!int.TryParse(year, out int parsedYear) ||
            parsedYear < EarliestVehicleYear ||
            parsedYear > latestVehicleYear)
        {
            return Invalid(
                $"Enter a year from {EarliestVehicleYear} through {latestVehicleYear}.");
        }

        if (!int.TryParse(mileage, out int parsedMileage) ||
            parsedMileage < 0)
        {
            return Invalid("Enter a current mileage of zero or greater.");
        }

        return new VehicleValidationResult
        {
            IsValid = true,
            Year = parsedYear,
            Mileage = parsedMileage
        };
    }

    private static VehicleValidationResult Invalid(string message)
    {
        return new VehicleValidationResult
        {
            IsValid = false,
            ErrorMessage = message
        };
    }
}
