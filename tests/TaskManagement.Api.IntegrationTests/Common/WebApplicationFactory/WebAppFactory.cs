using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskManagement.Infrastructure.Common.Persistence;

namespace TaskManagement.Api.IntegrationTests.Common.WebApplicationFactory;

public class WebAppFactory : WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
{
    private SqliteTestDatabase _testDatabase = null!;

    public AppHttpClient CreateAppHttpClient()
    {
        return new AppHttpClient(CreateClient());
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        _testDatabase.Dispose();

        return Task.CompletedTask;
    }

    public void ResetDatabase()
    {
        _testDatabase.ResetDatabase();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _testDatabase = SqliteTestDatabase.CreateAndInitialize();

        _ = builder.ConfigureTestServices(services => services
            .RemoveAll<DbContextOptions<AppDbContext>>()
            .AddDbContext<AppDbContext>((sp, options) => options.UseSqlite(_testDatabase.Connection)));

        _ = builder.ConfigureAppConfiguration((context, conf) => conf.AddInMemoryCollection(new Dictionary<string, string?>
        {
            { "EmailSettings:EnableEmailNotifications", "false" },
        }));
    }
}