using SQLite;
using tellescm_MD_Final.Models;

namespace tellescm_MD_Final.Data;

public class DatabaseService
{
    private const string DatabaseFilename = "torque-tracker.db3";

    private readonly SQLiteAsyncConnection database;
    private readonly SemaphoreSlim initializationLock = new(1, 1);
    private bool isInitialized;

    public DatabaseService()
    {
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        var flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        database = new SQLiteAsyncConnection(databasePath, flags);
    }

    public async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        await InitializeAsync();
        return database;
    }

    public async Task InitializeAsync()
    {
        if (isInitialized)
        {
            return;
        }

        await initializationLock.WaitAsync();
        try
        {
            if (isInitialized)
            {
                return;
            }

            await database.CreateTableAsync<Vehicle>();
            await database.CreateTableAsync<ServiceRecord>();
            await database.CreateTableAsync<MaintenanceReminder>();

            isInitialized = true;
        }
        finally
        {
            initializationLock.Release();
        }
    }
}
