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

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Tracking Notes |
|---|---|---|---|---|---|
| Core Component | `(root)` | `ConsoleLogger` | `ConsoleLogger.cs` | Completed | `TBD` |
| Core Component | `(root)` | `Constants` | `Constants.cs` | Completed | `TBD` |
| Core Component | `Controls` | `ImageSecurity` | `Controls/ImageSecurity.cs` | Completed | `TBD` |
| Core Component | `Data` | `GenericDataColumn` | `Data/GenericDataColumn.cs` | Completed | `TBD` |
| Core Component | `Data` | `GenericDataRow` | `Data/GenericDataRow.cs` | Completed | `TBD` |
| Core Component | `Data` | `GenericDataTable` | `Data/GenericDataTable.cs` | Completed | `TBD` |
| Provider Component | `Data` | `GenericSqlDataProvider` | `Data/GenericSqlDataProvider.cs` | Completed | `TBD` |
| Provider Component | `Data` | `IDataProvider` | `Data/IDataProvider.cs` | Completed | `TBD` |
| Core Component | `Data` | `IQueryFilterElement` | `Data/IQueryFilterElement.cs` | Completed | `TBD` |
| Core Component | `Data` | `ObjectColumnAttribute` | `Data/ObjectColumnAttribute.cs` | Completed | `TBD` |
| Core Component | `Data` | `QueryFilter` | `Data/QueryFilter.cs` | Completed | `TBD` |
| Core Component | `Data` | `QueryFilterElement` | `Data/QueryFilterElement.cs` | Completed | `TBD` |
| Core Component | `Data` | `SqlDataProviderBase` | `Data/SqlDataProviderBase.cs` | Completed | `TBD` |
| Core Component | `(root)` | `Dates` | `Dates.cs` | Completed | `TBD` |
| Core Component | `(root)` | `FileLogger` | `FileLogger.cs` | Completed | `TBD` |
| Manager Component | `(root)` | `IDataManager` | `IDataManager.cs` | Completed | `TBD` |
| Core Component | `(root)` | `ILogger` | `ILogger.cs` | Completed | `TBD` |
| Provider Component | `(root)` | `INamedObjectProvider` | `INamedObjectProvider.cs` | Completed | `TBD` |
| Provider Component | `(root)` | `INamedValueProvider` | `INamedValueProvider.cs` | Completed | `TBD` |
| Manager Component | `(root)` | `LogManager` | `LogManager.cs` | Completed | `TBD` |
| Core Component | `Media` | `AsxMedia` | `Media/AsxMedia.cs` | Completed | `TBD` |
| Provider Component | `(root)` | `NamedObjectProvider` | `NamedObjectProvider.cs` | Completed | `TBD` |
| Provider Component | `(root)` | `NamedValueProvider` | `NamedValueProvider.cs` | Completed | `TBD` |
| Core Component | `Net` | `NetworkConnection` | `Net/NetworkConnection.cs` | Completed | `TBD` |
| Helper Component | `Net` | `SmsHelper` | `Net/SmsHelper.cs` | Completed | `TBD` |
| Core Component | `(root)` | `ObjectPair` | `ObjectPair.cs` | Completed | `TBD` |
| Core Component | `(root)` | `PasswordComplexityRequirement` | `PasswordComplexityRequirement.cs` | Completed | `TBD` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | `Properties/AssemblyInfo.cs` | Completed | `TBD` |
| Core Component | `(root)` | `QueryParser` | `QueryParser.cs` | Completed | `TBD` |
| Core Component | `Utilities` | `ConfigUtil` | `Utilities/ConfigUtil.cs` | Completed | `TBD` |
| Helper Component | `Utilities` | `ControlHelper` | `Utilities/ControlHelper.cs` | Completed | `TBD` |
| Core Component | `Utilities` | `ControlInfo` | `Utilities/ControlInfo.cs` | Completed | `TBD` |
| Helper Component | `Utilities/Data` | `CsvHelper` | `Utilities/Data/CsvHelper.cs` | Completed | `TBD` |
| Helper Component | `Utilities/Data` | `DataHelper` | `Utilities/Data/DataHelper.cs` | Completed | `TBD` |
| Helper Component | `Utilities/Data` | `OracleHelper` | `Utilities/Data/OracleHelper.cs` | Completed | `TBD` |
| Helper Component | `Utilities/Data` | `SqlHelper` | `Utilities/Data/SqlHelper.cs` | Completed | `TBD` |
| Core Component | `Utilities/Data` | `XmlUtil` | `Utilities/Data/XmlUtil.cs` | Completed | `TBD` |
| Core Component | `Utilities/IO` | `Compression` | `Utilities/IO/Compression.cs` | Completed | `TBD` |
| Helper Component | `Utilities/IO` | `FileHelper` | `Utilities/IO/FileHelper.cs` | Completed | `TBD` |
| Helper Component | `Utilities/IO` | `FtpHelper` | `Utilities/IO/FtpHelper.cs` | Completed | `TBD` |
| Core Component | `Utilities/IO` | `ImageUtil` | `Utilities/IO/ImageUtil.cs` | Completed | `TBD` |
| Helper Component | `Utilities/IO` | `LogHelper` | `Utilities/IO/LogHelper.cs` | Completed | `TBD` |
| Helper Component | `Utilities/IO` | `TextHelper` | `Utilities/IO/TextHelper.cs` | Completed | `TBD` |
| Core Component | `Utilities` | `JsonUtil` | `Utilities/JsonUtil.cs` | Completed | `TBD` |
| Helper Component | `Utilities` | `MIMEHelper` | `Utilities/MIMEHelper.cs` | Completed | `TBD` |
| Core Component | `Utilities` | `Misc` | `Utilities/Misc.cs` | Completed | `TBD` |
| Helper Component | `Utilities` | `NetHelper` | `Utilities/NetHelper.cs` | Completed | `TBD` |
| Core Component | `Utilities` | `ReflectionUtil` | `Utilities/ReflectionUtil.cs` | Completed | `TBD` |
| Core Component | `Utilities` | `Security` | `Utilities/Security.cs` | Completed | `TBD` |
| Core Component | `Utilities` | `SerializationUtil` | `Utilities/SerializationUtil.cs` | Completed | `TBD` |
| Helper Component | `Utilities` | `SmtpHelper` | `Utilities/SmtpHelper.cs` | Completed | `TBD` |
| Core Component | `Utilities` | `Substituter` | `Utilities/Substituter.cs` | Completed | `TBD` |
| Core Component | `Utilities` | `Test` | `Utilities/Test.cs` | Completed | `TBD` |
| Core Component | `Utilities` | `TimeUtil` | `Utilities/TimeUtil.cs` | Completed | `TBD` |
| Core Component | `Utilities` | `Validator` | `Utilities/Validator.cs` | Completed | `TBD` |
| Helper Component | `Utilities` | `WebHelper` | `Utilities/WebHelper.cs` | Completed | `TBD` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

