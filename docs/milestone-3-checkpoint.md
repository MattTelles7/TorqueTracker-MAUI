# Milestone 3 Checkpoint

## Completed implementation

- Replaced the starter counter page with a Torque Tracker dashboard.
- Added text-labeled Home, Vehicles, History, and Settings Shell tabs.
- Registered Add Vehicle as a non-tab Shell route.
- Added `Vehicle`, `ServiceRecord`, and `MaintenanceReminder` SQLite entity shapes.
- Added a lazily initialized `SQLiteAsyncConnection` stored under `FileSystem.AppDataDirectory`.
- Added `IVehicleRepository` and its SQLite implementation with active-list, ID lookup, and insert operations.
- Added CommunityToolkit.Mvvm view models and asynchronous commands for the dashboard, vehicle list, and add form.
- Added validation for required text, year range 1886 through next calendar year, and nonnegative integer mileage.
- Added save confirmation, back navigation, list refresh on return, and dashboard refresh on appearance.
- Added honest placeholder pages for maintenance history and settings.
- Added repository ignore rules for build output, IDE state, deployment packages, signing files, and local databases.

## Important files

- `tellescm_MD-Final/tellescm_MD-Final/MauiProgram.cs`: dependency registrations
- `tellescm_MD-Final/tellescm_MD-Final/AppShell.xaml.cs`: primary tabs and Add Vehicle route
- `tellescm_MD-Final/tellescm_MD-Final/Data/DatabaseService.cs`: database path and table initialization
- `tellescm_MD-Final/tellescm_MD-Final/Repositories/IVehicleRepository.cs`: vehicle persistence contract
- `tellescm_MD-Final/tellescm_MD-Final/Repositories/SQLiteVehicleRepository.cs`: SQLite vehicle operations
- `tellescm_MD-Final/tellescm_MD-Final/ViewModels/`: observable state and commands
- `tellescm_MD-Final/tellescm_MD-Final/Views/`: dashboard, list, form, and placeholder pages

## Verification commands and actual results

The following commands were run from the repository root on August 1, 2026.

| Command | Result |
| --- | --- |
| `dotnet --info` | Succeeded. Active SDK: 9.0.316; host runtime: 9.0.18; .NET 8.0.29 runtime also installed. No `global.json` is present. |
| `dotnet workload list` | Succeeded. Android, iOS, Mac Catalyst, and MAUI Windows workloads are installed. |
| `dotnet restore .\tellescm_MD-Final\tellescm_MD-Final.sln` | Succeeded. Packages restored for the existing project. |
| `dotnet build .\tellescm_MD-Final\tellescm_MD-Final\tellescm_MD-Final.csproj -f net8.0-windows10.0.19041.0 --no-restore` | Final retry succeeded with 0 warnings and 0 errors. An earlier successful compile reported MVVM Toolkit WinRT-generation warnings; the source was revised instead of suppressing them. |
| `dotnet build .\tellescm_MD-Final\tellescm_MD-Final\tellescm_MD-Final.csproj -f net8.0-android --no-restore` | Succeeded with 0 warnings and 0 errors. No emulator was required for compilation. |
| `dotnet list .\tellescm_MD-Final\tellescm_MD-Final\tellescm_MD-Final.csproj package` | Succeeded. Confirmed CommunityToolkit.Mvvm 8.4.0 and sqlite-net-pcl 1.9.172 resolved for every target. |
| `adb devices -l` | Succeeded, but listed no connected device or running emulator. |
| `dotnet run --project .\tellescm_MD-Final\tellescm_MD-Final\tellescm_MD-Final.csproj -f net8.0-windows10.0.19041.0 --no-build` | Failed before app startup. The launch profile could not be applied, followed by Windows App Runtime activation failure `0x80040154 (REGDB_E_CLASSNOTREG): Class not registered`. |
| Visual Studio deployment to Windows Machine | Succeeded after correcting the package identity. Registered `com.companyname.torquetracker_1.0.0.1_x64__9zz4h110yvjzm` with 0 deployment errors. |
| Visual Studio Windows runtime test | Passed. The app launched, rendered all four tabs, validated the add form, saved a vehicle, refreshed Home and Vehicles, and retained the vehicle after a full stop/restart. |

`dotnet workload restore` was not run because the required Android and MAUI Windows workloads were already installed and both corresponding builds succeeded.

## Test pass — August 1, 2026

- Environment: Windows 10.0.26200, .NET SDK 9.0.316, Visual Studio 2022 17.14, compiling the project's unchanged .NET 8 targets.
- Compiled targets: `net8.0-windows10.0.19041.0` and `net8.0-android`, both with 0 warnings and 0 errors.
- Runtime target launched: Windows Machine through Visual Studio. ADB still listed no Android target, so Android runtime behavior was not exercised.
- Runtime test matrix: passed on Windows. Verified the empty Home state; Home, Vehicles, History, and Settings navigation; missing-field validation; invalid-year validation; a valid vehicle save; immediate Home and Vehicles refresh; and SQLite persistence after a full stop/restart.
- Code-path inspection: passed. Views do not access SQLite; DI registrations match injected constructors; async persistence calls are awaited; `async void` is limited to page lifecycle overrides; save concurrency and busy-state cleanup are guarded; initialization uses a semaphore; the database path uses `FileSystem.AppDataDirectory`; refresh calls occur in `OnAppearing`; and no fake stored records were found.
- Defects repaired during this test pass: changed the invalid underscore-containing application ID to `com.companyname.torquetracker`; changed app startup so `App.xaml` resources load before DI constructs `AppShell` and its pages, eliminating the startup `XamlParseException` for `PageTitle`.
- Remaining runtime coverage gap: start an existing Android emulator or connect a device, then repeat the runtime matrix on Android.

## Manual demonstration steps

1. Open the existing solution in Visual Studio 2022.
2. Choose the MAUI project as the startup project.
3. Select Windows Machine, or select an already-configured Android emulator and wait for its home screen.
4. Run the app without changing the target framework.
5. Confirm Home opens with the Torque Tracker title, empty vehicle state, and honest maintenance empty state.
6. Select **Add your first vehicle**. Try saving once with missing fields to show the validation message.
7. Enter `Daily Driver`, `2020`, `Toyota`, `Camry`, and `45000`, then select **Save vehicle**.
8. Dismiss the built-in confirmation. Confirm the new vehicle appears in Vehicles.
9. Select Home and confirm the nickname, year, make, model, and mileage now appear.
10. Select History and Settings to show which areas are deliberately deferred.

This sequence fits a two-minute checkpoint demonstration on Windows Machine or a running Android emulator.

## Known limitations

- Windows runtime behavior was verified through Visual Studio; the direct `dotnet run` launch path still fails on this machine before app startup with Windows App Runtime error `0x80040154`.
- No Android device or emulator was connected during command-line verification.
- iOS and Mac Catalyst runtime verification requires a Mac and was not attempted.
- Only the add/list/current-vehicle slice is implemented. Vehicle changes and all maintenance behavior remain out of scope.

## Next recommended implementation stage

Implement service logging and real maintenance history for a selected vehicle. Add a service-record repository, a small validated entry form, and a history list before beginning reminders or notifications.
