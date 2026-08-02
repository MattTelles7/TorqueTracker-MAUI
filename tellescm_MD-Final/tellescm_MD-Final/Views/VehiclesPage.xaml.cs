using tellescm_MD_Final.ViewModels;

namespace tellescm_MD_Final.Views;

public partial class VehiclesPage : ContentPage
{
    private readonly VehiclesViewModel viewModel;

    public VehiclesPage(VehiclesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = this.viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadAsync();
    }
}
