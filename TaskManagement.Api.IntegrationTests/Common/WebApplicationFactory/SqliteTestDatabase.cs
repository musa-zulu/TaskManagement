using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Common.Persistence;

namespace TaskManagement.Api.IntegrationTests.Common.WebApplicationFactory;

/// <summary>
/// I am using SQLite so no need to spin an actual database.
/// </summary>
public class SqliteTestDatabase : IDisposable
{
    public SqliteConnection Connection { get; }

    public static SqliteTestDatabase CreateAndInitialize()
    {
        var testDatabase = new SqliteTestDatabase("DataSource=:memory:");

        testDatabase.InitializeDatabase();

        return testDatabase;
    }

    public void InitializeDatabase()
    {
        Connection.Open();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(Connection)
            .Options;

        using var context = new AppDbContext(options, null!, null!);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    public void ResetDatabase()
    {
        Connection.Close();

        InitializeDatabase();
    }

    public void Dispose()
    {
        Connection.Close();
    }

    private SqliteTestDatabase(string connectionString)
    {
        Connection = new SqliteConnection(connectionString);
    }
}