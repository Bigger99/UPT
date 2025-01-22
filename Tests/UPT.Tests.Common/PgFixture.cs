using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using UPT.Data;

namespace UPT.Common;

public class PgFixture : IAsyncDisposable
{
    public const string DatabaseName = "master_iiot";
    public const string PostgreImage = "postgres:latest";
    private readonly PostgreSqlContainer _container;

    public PgFixture()
    {
        _container = new PostgreSqlBuilder()
            .WithDatabase(DatabaseName)
            .WithImage(PostgreImage)
            .Build();
    }

    public Task Start() => _container.StartAsync();

    public ValueTask DisposeAsync() => _container.DisposeAsync();

    public string ConnectionString => _container.GetConnectionString();

    public UPTDbContext BuildDataContext() => new(new DbContextOptionsBuilder<UPTDbContext>().UseNpgsql(ConnectionString).Options);
}
