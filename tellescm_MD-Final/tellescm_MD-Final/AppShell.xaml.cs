using tellescm_MD_Final.Views;

namespace tellescm_MD_Final;

public partial class AppShell : Shell
{
    public AppShell(
        DashboardPage dashboardPage,
        VehiclesPage vehiclesPage,
        HistoryPage historyPage,
        SettingsPage settingsPage)
    {
        InitializeComponent();

        var tabs = new TabBar();
        tabs.Items.Add(CreateTab("Home", "Home", dashboardPage));
        tabs.Items.Add(CreateTab("Vehicles", "Vehicles", vehiclesPage));
        tabs.Items.Add(CreateTab("History", "History", historyPage));
        tabs.Items.Add(CreateTab("Settings", "Settings", settingsPage));
        Items.Add(tabs);

        Routing.RegisterRoute(nameof(AddVehiclePage), typeof(AddVehiclePage));
    }

    private static ShellContent CreateTab(string title, string route, Page page)
    {
        return new ShellContent
        {
            Title = title,
            Route = route,
            Content = page
        };
    }
}
