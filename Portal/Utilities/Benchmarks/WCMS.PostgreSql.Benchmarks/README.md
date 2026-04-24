# WCMS PostgreSQL Benchmarks

Simple BenchmarkDotNet harness for PostgreSQL runtime baseline checks.

## Run

```bash
export POSTGRES_BENCHMARK_CONNECTION_STRING='Host=localhost;Port=5432;Database=mportal;Username=postgres;Password=postgres'
dotnet run --project Portal/Utilities/Benchmarks/WCMS.PostgreSql.Benchmarks/WCMS.PostgreSql.Benchmarks.csproj -c Release
```

## Benchmarks

- `HealthQuery` (`SELECT 1`)
- `CountWebPageRows` (`SELECT COUNT(*) FROM "WebPage"`)
