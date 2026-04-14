# 04 - Data Providers Caching And Persistence

## Provider architecture
Pattern in use:
- interfaces in `WCMS.Framework` (`IWebPageProvider`, `IWebUserProvider`, etc.)
- SQL implementations in `WCMS.Framework.Core.SqlProvider`
- optional XML implementations in `WCMS.Framework.Core.XmlProvider`
- manager wrappers in `WCMS.Framework/Core/Manager` layered on `StandardDataManager<T>`

## Cache model
`StandardDataManager<T>` supports:
- full cache mode (`CacheStatus.Full`) for in-memory query/filter operations.
- partial cache behavior.
- fallback to provider calls when not cached.
- object-level cache policy controlled through `WebObject` metadata.

## Persistence shape
- Dominantly stored-procedure-backed SQL access via `SqlHelper`.
- Setup assets include large procedural coverage:
  - 139 table create scripts
  - 333 procedure create scripts

## Data abstraction entry points
- `DataAccess.CreateProvider<T>()`, `CreateXmlProvider<T>()`, `CreateWebObjectProvider()`.
- `DbManager` and SQL SMO scripting for schema/data export-import.

## Key anchors
- `legacy/Portal/WebSystem/WCMS.Framework/Data/DataAccess.cs`
- `legacy/Portal/WebSystem/WCMS.Framework/Core/StandardDataManager.cs`
- `legacy/Portal/WebSystem/WCMS.Framework.Core.SqlProvider/*.cs`
- `legacy/Portal/WebSystem/WCMS.Framework.Core.XmlProvider/*.cs`

## Evaluation
Strength: clear provider abstraction and script-driven DB recovery path.
Risk: partial interface implementation is widespread (many `NotImplementedException` paths), so behavior coverage is uneven across providers.
