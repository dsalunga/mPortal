# 16 - Standalone App BibleReader

## Scope
`legacy/BibleReader` (`BibleReader.WebApp` + `BibleReader.Core`).

## Feature summary
- Exposes Bible verse retrieval via ASMX service (`BibleService.asmx`).
- Provides WebForms UI shell (`Default.aspx`, `About.aspx`, account pages).
- Includes setup/admin endpoint (`Setup.aspx`) for schema/data script operations.
- Supports multiple Bible versions and localized book names through core providers.

## Architecture
- `BibleReader.WebApp`: ASP.NET WebForms application (.NET Framework 4.7 target).
- `BibleReader.Core`: domain models/providers for versions, books, verses.
- Service/API layer is thin and delegates directly to `BibleManager`.

## Data model and provider flow
- `BibleManager` preloads version/book metadata using static caches.
- Verse retrieval path: version + book mapping -> `BibleVerse.Provider.Get(...)`.
- Local admin data folder contains SQL + XML seed artifacts (`Admin/Data/Database/*`).

## Coupling profile
- Web app references shared WCMS assemblies from Portal build output (`WCMS.Common`, `WCMS.Framework`).
- Operationally related to integration-side BibleReader modules, but kept as separate solution.

## Evaluation
Strengths:
- Bounded, understandable service scope with low routing complexity.
- Straightforward provider abstraction for verse retrieval.

Risks:
- Static cache lifetime and limited invalidation strategy.
- Setup endpoint includes substantial legacy/commented code paths.
- Heavy dependency on shared Portal binaries for runtime compatibility.

## Key anchors
- `legacy/BibleReader/BibleReader/BibleService.asmx.cs`
- `legacy/BibleReader/BibleReader/BibleReader.WebApp.csproj`
- `legacy/BibleReader/BibleReader/Setup.aspx.cs`
- `legacy/BibleReader/BibleReader.Core/BibleManager.cs`
- `legacy/BibleReader/BibleReader.Core/Providers/*`
- `legacy/BibleReader/BibleReader/Admin/Data/Database/*`

