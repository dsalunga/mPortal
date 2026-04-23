using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WCMS.WebSystem.Apps.Integration.Data;

namespace WCMS.WebSystem.Apps.Integration.Migrations.PostgreSql;

internal static class PostgreSqlDesignTimeOptions
{
    private const string FallbackConnection = "Host=localhost;Port=5432;Database=mportal_test;Username=postgres;Password=postgres";

    public static string ResolveConnectionString()
    {
        return Environment.GetEnvironmentVariable("POSTGRES_TEST_CONNECTION_STRING")
               ?? Environment.GetEnvironmentVariable("ConnectionStrings__ExternalDb")
               ?? Environment.GetEnvironmentVariable("ConnectionStrings__ConnectionString")
               ?? FallbackConnection;
    }

    public static DbContextOptions<TContext> BuildOptions<TContext>() where TContext : DbContext
    {
        var migrationsAssembly = typeof(PostgreSqlExternalDbContextFactory).Assembly.GetName().Name;
        return new DbContextOptionsBuilder<TContext>()
            .UseNpgsql(ResolveConnectionString(), npgsql =>
                npgsql.MigrationsAssembly(migrationsAssembly))
            .Options;
    }
}

public sealed class PostgreSqlExternalDbContextFactory : IDesignTimeDbContextFactory<ExternalDbContext>
{
    public ExternalDbContext CreateDbContext(string[] args)
        => new(PostgreSqlDesignTimeOptions.BuildOptions<ExternalDbContext>());
}

public sealed class PostgreSqlIntegrationDbContextFactory : IDesignTimeDbContextFactory<IntegrationDbContext>
{
    public IntegrationDbContext CreateDbContext(string[] args)
        => new(PostgreSqlDesignTimeOptions.BuildOptions<IntegrationDbContext>());
}

public sealed class PostgreSqlMusicDbContextFactory : IDesignTimeDbContextFactory<MusicDbContext>
{
    public MusicDbContext CreateDbContext(string[] args)
        => new(PostgreSqlDesignTimeOptions.BuildOptions<MusicDbContext>());
}
