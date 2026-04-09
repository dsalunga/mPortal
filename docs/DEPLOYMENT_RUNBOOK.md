# mPortal CMS — Production Deployment Runbook

## Overview

This document describes how to deploy the mPortal CMS (.NET 10) to production environments.

## Prerequisites

- .NET 10 SDK (or runtime) installed on the target server
- SQL Server 2019+ with the WCMS database schema deployed
- Docker (optional, for containerized deployment)

## Deployment Options

### Option 1: Docker Compose (Recommended)

```bash
# Clone the repository
git clone <repo-url> && cd mPortal-private

# Set the SA password
export SA_PASSWORD="$(openssl rand -base64 24)"

# Start SQL Server and web application
docker compose up -d

# Verify
curl http://localhost:8080/health
```

The web application is exposed on port 8080. SQL Server is on port 1433.

### Option 2: GitHub Actions Deployment Pipeline

1. Navigate to **Actions** → **Deploy** workflow
2. Click **Run workflow**
3. Select target environment (`staging` or `production`)
4. Select application (`web`, `integration`, or `all`)
5. The pipeline builds, publishes artifacts, and pushes a Docker image to GHCR

### Option 3: Manual Deployment (IIS / Kestrel)

```bash
# Publish the main portal
dotnet publish Portal/WebSystem/WebSystem/WCMS.WebSystem.WebApp.csproj -c Release -o ./publish

# Copy to target server
scp -r ./publish/* user@server:/var/www/mportal/

# On the server: configure appsettings.json with production connection strings
# Then start the application
cd /var/www/mportal
dotnet WCMS.WebSystem.WebApp.dll
```

For IIS hosting, create an IIS site pointing to the publish directory with the ASP.NET Core Module installed.

## Configuration

### Connection Strings

Set in `appsettings.json` or via environment variables:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<host>;Database=WCMS;User Id=<user>;Password=<pass>;TrustServerCertificate=True;"
  },
  "WConfig": {
    "Environment": "PROD",
    "AllowCache": true,
    "DefaultSite": 1
  }
}
```

For secrets, use:
- **Environment variables**: `ConnectionStrings__DefaultConnection=...`
- **Azure Key Vault**: Configure via `builder.Configuration.AddAzureKeyVault()`
- **User secrets**: `dotnet user-secrets set "ConnectionStrings:DefaultConnection" "..."`
- Set application security keys from secrets/env:
  - `Security__PasswordSalt`
  - `Security__LoginEncryptionKey` (base64, 32 bytes)
  - `Security__LoginEncryptionIV` (base64, 16 bytes)

### Environment Variables

| Variable | Description | Default |
|---|---|---|
| `ASPNETCORE_ENVIRONMENT` | Runtime environment | `Production` |
| `ASPNETCORE_URLS` | Listen URLs | `http://+:8080` |
| `ConnectionStrings__DefaultConnection` | SQL Server connection string | — |

## Web Hosts

The application consists of multiple web hosts:

| Host | Project | Port (dev) | Description |
|---|---|---|---|
| Main Portal | `WCMS.WebSystem.WebApp` | 5000 | Primary CMS portal |
| Integration | `WCMS.WebSystem.Apps.Integration.WebApp` | 5001 | Member management, music competition |
| SystemParts | `WCMS.WebSystem.Apps.SystemApps.WebApp` | 5002 | Content, articles, calendar |
| SystemPartsG2 | `WCMS.WebSystem.Apps.SystemApps2.WebApp` | 5003 | Forum, social, ads |
| SystemPartsG3 | `WCMS.WebSystem.Apps.SystemApps3.WebApp` | 5004 | Incidents, jobs |
| BibleReader | `BibleReader.WebApp` | 5005 | Bible verse reader |
| LessonReviewer | `LessonReviewer` | 5006 | Lesson playback |
| BranchLocator | `WCMS.WebSystem.Apps.BranchLocator.WebApp` | 5007 | Branch/locale search |

## Health Checks

Each host exposes a `/health` endpoint:

```bash
curl http://localhost:8080/health  # Returns "ok"
```

## Rollback Procedure

1. Stop the running application
2. Restore the previous deployment artifact
3. Restart the application
4. Verify with `/health` endpoint

For Docker deployments, tag the previous image and redeploy:
```bash
docker compose down
docker compose up -d --pull always
```

## Monitoring

- Application logs: stdout/stderr (configure via `appsettings.json` logging section)
- Health check: `GET /health`
- System info: `GET /api/system/info`
