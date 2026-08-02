# Torque Tracker

Torque Tracker is an Android-first .NET MAUI application for keeping basic information about personal vehicles and, in future stages, tracking their maintenance.

## Milestone 3 Checkpoint

This checkpoint is a small working prototype, not the final application.

### What currently works

- Shell navigation with text-labeled Home, Vehicles, History, and Settings tabs
- An empty-state dashboard and vehicle list
- An Add Vehicle form with required-field, year, and mileage validation
- Asynchronous vehicle persistence in a local SQLite database
- Vehicle-list refresh after saving
- Dashboard display of the first active saved vehicle
- Dependency injection for the database service, repository, view models, and pages
- SQLite table initialization for vehicles, future service records, and future maintenance reminders

### Intentionally unfinished

Vehicle editing, archiving, deletion, service logging, maintenance history, reminder calculations, preferences, notifications, and notification permissions are not implemented. The History and Settings tabs clearly identify this future work and do not show sample data.

### Architecture

The prototype follows this flow:

`XAML Views -> CommunityToolkit.Mvvm ViewModels -> IVehicleRepository -> SQLiteAsyncConnection`

The database file is created under `FileSystem.AppDataDirectory`. Views do not access SQLite directly.

### Required packages

- `CommunityToolkit.Mvvm` 8.4.0
- `sqlite-net-pcl` 1.9.172

The existing MAUI and debug-logging package references remain in place.

### Restore and build

From the repository root:

```powershell
dotnet restore .\tellescm_MD-Final\tellescm_MD-Final.sln
dotnet build .\tellescm_MD-Final\tellescm_MD-Final\tellescm_MD-Final.csproj -f net8.0-windows10.0.19041.0 --no-restore
dotnet build .\tellescm_MD-Final\tellescm_MD-Final\tellescm_MD-Final.csproj -f net8.0-android --no-restore
```

Verified project target frameworks are `net8.0-android`, `net8.0-ios`, `net8.0-maccatalyst`, and, on Windows, `net8.0-windows10.0.19041.0`. Windows and Android compilation both completed with zero warnings and zero errors on the checkpoint machine.

### Demonstrate the vehicle workflow

1. Open `tellescm_MD-Final/tellescm_MD-Final.sln` in Visual Studio.
2. Select Windows Machine, or select a configured Android emulator, and start the app.
3. On Home, use **Add your first vehicle**.
4. Enter a nickname, year, make, model, and nonnegative current mileage, then save.
5. Dismiss the confirmation and verify the vehicle appears on Vehicles.
6. Return to Home and verify the saved vehicle is shown as the current vehicle.
7. Open History and Settings to show the explicitly labeled future-work placeholders.

### Local environment limitations

Visual Studio deployment to Windows Machine and the full vehicle workflow were verified successfully. Direct `dotnet run` still fails before app startup on the checkpoint machine because its CLI launch path cannot activate the Windows App Runtime (`REGDB_E_CLASSNOTREG`). Android compilation is available, but ADB reports no connected device or running emulator.

See [docs/milestone-3-checkpoint.md](docs/milestone-3-checkpoint.md) for the complete verification record.
