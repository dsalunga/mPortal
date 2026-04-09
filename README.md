# mPortal
A custom ASP.NET web content management system (WCMS).

## Current Status
- Runtime and libraries target `.NET 10` (`net10.0`).
- Windows-only desktop utilities remain `net10.0-windows`.
- Legacy pre-migration source is preserved in `/legacy` for reference.

## Prerequisites
- .NET 10 SDK (`dotnet --list-sdks` should include a `10.0.x` SDK).
- PostgreSQL (recommended for cross-platform setup), or SQL Server for Windows.
- On macOS/Linux: standard shell tooling (`bash`/`zsh`).
- Optional on Windows only: Visual Studio 2026 for `net10.0-windows` utilities.

## Quick Start (macOS/Linux)
1. Restore runtime projects (recommended on macOS/Linux):
```bash
dotnet restore Portal/WebSystem/WebSystem/WCMS.WebSystem.WebApp.csproj
dotnet restore Tests/WCMS.Integration.Tests/WCMS.Integration.Tests.csproj
```
If you need to restore the full solution graph (including Windows-only projects metadata):
```bash
dotnet restore mPortal.slnx -p:EnableWindowsTargeting=true
```
2. Configure database settings (example for PostgreSQL):
```bash
export WCMS__DatabaseProvider=PostgreSql
export PG_PASSWORD='<set-a-strong-password>'
export ConnectionStrings__ConnectionString="Host=localhost;Port=5432;Database=mportal;Username=postgres;Password=${PG_PASSWORD}"
export ConnectionStrings__DefaultConnection="${ConnectionStrings__ConnectionString}"
```
3. Run the main CMS web app:
```bash
dotnet run --project Portal/WebSystem/WebSystem/WCMS.WebSystem.WebApp.csproj --urls http://localhost:5088
```
4. Open:
- `http://localhost:5088/`
- `http://localhost:5088/Central/`

## Configuration Notes
- Main app settings: `Portal/WebSystem/WebSystem/appsettings.json`
- WCMS provider switch: `WCMS:DatabaseProvider` (`SqlServer` or `PostgreSql`)
- Primary connection string key: `ConnectionStrings:ConnectionString`
- Health endpoint: `/health`
- Security keys should be provided from environment variables (see `.env.example`):
  - `Security__PasswordSalt`
  - `Security__LoginEncryptionKey`
  - `Security__LoginEncryptionIV`

## Database Notes
- A blank database can result in limited/empty site rendering until seed or migrated data is loaded.
- SQL Server dumps can be processed and migrated to PostgreSQL as part of modernization execution.

## Logging
- Application logs default under: `Content/Admin/Data/Logs/`

## Migration Tracking
- File-level source-of-truth inventory:
  - `docs/plans/legacy-migration/inventory/legacy-source-tracking-all.csv`
- Execution board for active migration batches:
  - `docs/plans/legacy-migration/EXECUTION_BOARD.md`

## Tests
- Integration unit test project:
  - `Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration.UnitTest`
- Repo test projects:
  - `Tests/WCMS.Framework.Tests`
  - `Tests/WCMS.Integration.Tests`

## Contributing
1. Create a feature branch for your work.
2. Keep migration changes traceable in the legacy tracker CSV + execution board.
3. Run local secret checks before commit:
```bash
git config core.hooksPath .githooks
```
4. Submit a pull request with scope, risks, validation notes, and confirmation that no plaintext secrets/PII were introduced.
