namespace tellescm_MD_Final.Validation;

public class VehicleValidationResult
{
    public bool IsValid { get; init; }

    public string ErrorMessage { get; init; } = string.Empty;

    public int Year { get; init; }

    public int Mileage { get; init; }
}
