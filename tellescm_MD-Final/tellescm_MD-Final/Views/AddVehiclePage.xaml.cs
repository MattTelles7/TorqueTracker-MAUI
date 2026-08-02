using tellescm_MD_Final.ViewModels;

namespace tellescm_MD_Final.Views;

public partial class AddVehiclePage : ContentPage
{
    public AddVehiclePage(AddVehicleViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
