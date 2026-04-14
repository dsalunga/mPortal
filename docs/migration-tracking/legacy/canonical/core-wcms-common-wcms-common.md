# P003 - WCMS.Common

## Project Tracking Summary

| Field | Value |
|---|---|
| Project Path | `legacy/Core/WCMS.Common/WCMS.Common.csproj` |
| Project Kind | Library/Component |
| Assembly Name | `WCMS.Common` |
| Target Framework | `net8.0` |
| Output Type | `Library` |
| Migration Status | Completed |
| Status Basis | Project targets modern .NET (net8+), indicating completed baseline migration for this artifact. |
| Target Alternative | TBD |
| Tracking Owner | `TBD` |
| Target Milestone | `TBD` |

## Surface Coverage Snapshot

| Surface | Count |
|---|---:|
| Provider Component | 6 |
| Manager Component | 2 |
| Helper Component | 14 |
| Core Component | 33 |
| Assembly Metadata | 1 |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Core Component | `(root)` | `ConsoleLogger` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `ConsoleLogger.cs` |
| Core Component | `(root)` | `Constants` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Constants.cs` |
| Core Component | `Controls` | `ImageSecurity` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Controls/ImageSecurity.cs` |
| Core Component | `Data` | `GenericDataColumn` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Data/GenericDataColumn.cs` |
| Core Component | `Data` | `GenericDataRow` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Data/GenericDataRow.cs` |
| Core Component | `Data` | `GenericDataTable` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Data/GenericDataTable.cs` |
| Provider Component | `Data` | `GenericSqlDataProvider` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Data/GenericSqlDataProvider.cs` |
| Provider Component | `Data` | `IDataProvider` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Data/IDataProvider.cs` |
| Core Component | `Data` | `IQueryFilterElement` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Data/IQueryFilterElement.cs` |
| Core Component | `Data` | `ObjectColumnAttribute` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Data/ObjectColumnAttribute.cs` |
| Core Component | `Data` | `QueryFilter` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Data/QueryFilter.cs` |
| Core Component | `Data` | `QueryFilterElement` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Data/QueryFilterElement.cs` |
| Core Component | `Data` | `SqlDataProviderBase` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Data/SqlDataProviderBase.cs` |
| Core Component | `(root)` | `Dates` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Dates.cs` |
| Core Component | `(root)` | `FileLogger` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `FileLogger.cs` |
| Manager Component | `(root)` | `IDataManager` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `IDataManager.cs` |
| Core Component | `(root)` | `ILogger` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `ILogger.cs` |
| Provider Component | `(root)` | `INamedObjectProvider` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `INamedObjectProvider.cs` |
| Provider Component | `(root)` | `INamedValueProvider` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `INamedValueProvider.cs` |
| Manager Component | `(root)` | `LogManager` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `LogManager.cs` |
| Core Component | `Media` | `AsxMedia` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Media/AsxMedia.cs` |
| Provider Component | `(root)` | `NamedObjectProvider` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `NamedObjectProvider.cs` |
| Provider Component | `(root)` | `NamedValueProvider` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `NamedValueProvider.cs` |
| Core Component | `Net` | `NetworkConnection` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Net/NetworkConnection.cs` |
| Helper Component | `Net` | `SmsHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Net/SmsHelper.cs` |
| Core Component | `(root)` | `ObjectPair` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `ObjectPair.cs` |
| Core Component | `(root)` | `PasswordComplexityRequirement` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `PasswordComplexityRequirement.cs` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Properties/AssemblyInfo.cs` |
| Core Component | `(root)` | `QueryParser` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `QueryParser.cs` |
| Core Component | `Utilities` | `ConfigUtil` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/ConfigUtil.cs` |
| Helper Component | `Utilities` | `ControlHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/ControlHelper.cs` |
| Core Component | `Utilities` | `ControlInfo` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/ControlInfo.cs` |
| Helper Component | `Utilities/Data` | `CsvHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/Data/CsvHelper.cs` |
| Helper Component | `Utilities/Data` | `DataHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/Data/DataHelper.cs` |
| Helper Component | `Utilities/Data` | `OracleHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/Data/OracleHelper.cs` |
| Helper Component | `Utilities/Data` | `SqlHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/Data/SqlHelper.cs` |
| Core Component | `Utilities/Data` | `XmlUtil` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/Data/XmlUtil.cs` |
| Core Component | `Utilities/IO` | `Compression` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/IO/Compression.cs` |
| Helper Component | `Utilities/IO` | `FileHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/IO/FileHelper.cs` |
| Helper Component | `Utilities/IO` | `FtpHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/IO/FtpHelper.cs` |
| Core Component | `Utilities/IO` | `ImageUtil` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/IO/ImageUtil.cs` |
| Helper Component | `Utilities/IO` | `LogHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/IO/LogHelper.cs` |
| Helper Component | `Utilities/IO` | `TextHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/IO/TextHelper.cs` |
| Core Component | `Utilities` | `JsonUtil` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/JsonUtil.cs` |
| Helper Component | `Utilities` | `MIMEHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/MIMEHelper.cs` |
| Core Component | `Utilities` | `Misc` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/Misc.cs` |
| Helper Component | `Utilities` | `NetHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/NetHelper.cs` |
| Core Component | `Utilities` | `ReflectionUtil` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/ReflectionUtil.cs` |
| Core Component | `Utilities` | `Security` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/Security.cs` |
| Core Component | `Utilities` | `SerializationUtil` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/SerializationUtil.cs` |
| Helper Component | `Utilities` | `SmtpHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/SmtpHelper.cs` |
| Core Component | `Utilities` | `Substituter` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/Substituter.cs` |
| Core Component | `Utilities` | `Test` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/Test.cs` |
| Core Component | `Utilities` | `TimeUtil` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/TimeUtil.cs` |
| Core Component | `Utilities` | `Validator` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/Validator.cs` |
| Helper Component | `Utilities` | `WebHelper` | Completed | Already migrated baseline; continue parity and hardening. | `TBD` | `Utilities/WebHelper.cs` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

