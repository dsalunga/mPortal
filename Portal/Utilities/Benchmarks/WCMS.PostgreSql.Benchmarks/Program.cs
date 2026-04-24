using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Npgsql;

namespace WCMS.PostgreSql.Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<PostgreSqlBenchmarks>();
    }
}

[MemoryDiagnoser]
public class PostgreSqlBenchmarks
{
    private string _connectionString = string.Empty;

    [GlobalSetup]
    public void Setup()
    {
        _connectionString = Environment.GetEnvironmentVariable("POSTGRES_BENCHMARK_CONNECTION_STRING") ?? string.Empty;
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("Set POSTGRES_BENCHMARK_CONNECTION_STRING before running benchmarks.");
        }
    }

    [Benchmark]
    public async Task<long> HealthQuery()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand("SELECT 1", connection);
        var value = await command.ExecuteScalarAsync();
        return Convert.ToInt64(value);
    }

    [Benchmark]
    public async Task<long> CountWebPageRows()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand("SELECT COUNT(*) FROM \"WebPage\"", connection);
        var value = await command.ExecuteScalarAsync();
        return Convert.ToInt64(value);
    }
}
