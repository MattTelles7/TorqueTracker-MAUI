using tellescm_MD_Final.ViewModels;

namespace tellescm_MD_Final.Views;

public partial class DashboardPage : ContentPage
{
    private readonly DashboardViewModel viewModel;

    public DashboardPage(DashboardViewModel viewModel)
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
