# P041 - WCMS.Common

## Project Tracking Summary

| Field | Value |
|---|---|
| Project Path | `legacy/Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` |
| Project Kind | Library/Component |
| Assembly Name | `WCMS.Common` |
| Target Framework | `v4.8` |
| Output Type | `Library` |
| Migration Status | Partial |
| Status Basis | Legacy project coexists with a modern-target peer artifact; migration appears split/in-progress. |
| Tracking Owner | `TBD` |
| Target Milestone | `TBD` |

## Surface Coverage Snapshot

| Surface | Count |
|---|---:|
| Provider Component | 6 |
| Manager Component | 2 |
| Helper Component | 14 |
| Core Component | 33 |
| Configuration/Resource | 1 |
| Assembly Metadata | 1 |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Tracking Notes |
|---|---|---|---|---|---|
| Core Component | `(root)` | `ConsoleLogger` | `ConsoleLogger.cs` | Partial | `TBD` |
| Core Component | `(root)` | `Constants` | `Constants.cs` | Partial | `TBD` |
| Core Component | `Controls` | `ImageSecurity` | `Controls/ImageSecurity.cs` | Partial | `TBD` |
| Core Component | `Data` | `GenericDataColumn` | `Data/GenericDataColumn.cs` | Partial | `TBD` |
| Core Component | `Data` | `GenericDataRow` | `Data/GenericDataRow.cs` | Partial | `TBD` |
| Core Component | `Data` | `GenericDataTable` | `Data/GenericDataTable.cs` | Partial | `TBD` |
| Provider Component | `Data` | `GenericSqlDataProvider` | `Data/GenericSqlDataProvider.cs` | Partial | `TBD` |
| Provider Component | `Data` | `IDataProvider` | `Data/IDataProvider.cs` | Partial | `TBD` |
| Core Component | `Data` | `IQueryFilterElement` | `Data/IQueryFilterElement.cs` | Partial | `TBD` |
| Core Component | `Data` | `ObjectColumnAttribute` | `Data/ObjectColumnAttribute.cs` | Partial | `TBD` |
| Core Component | `Data` | `QueryFilter` | `Data/QueryFilter.cs` | Partial | `TBD` |
| Core Component | `Data` | `QueryFilterElement` | `Data/QueryFilterElement.cs` | Partial | `TBD` |
| Core Component | `Data` | `SqlDataProviderBase` | `Data/SqlDataProviderBase.cs` | Partial | `TBD` |
| Core Component | `(root)` | `Dates` | `Dates.cs` | Partial | `TBD` |
| Core Component | `(root)` | `FileLogger` | `FileLogger.cs` | Partial | `TBD` |
| Manager Component | `(root)` | `IDataManager` | `IDataManager.cs` | Partial | `TBD` |
| Core Component | `(root)` | `ILogger` | `ILogger.cs` | Partial | `TBD` |
| Provider Component | `(root)` | `INamedObjectProvider` | `INamedObjectProvider.cs` | Partial | `TBD` |
| Provider Component | `(root)` | `INamedValueProvider` | `INamedValueProvider.cs` | Partial | `TBD` |
| Manager Component | `(root)` | `LogManager` | `LogManager.cs` | Partial | `TBD` |
| Core Component | `Media` | `AsxMedia` | `Media/AsxMedia.cs` | Partial | `TBD` |
| Provider Component | `(root)` | `NamedObjectProvider` | `NamedObjectProvider.cs` | Partial | `TBD` |
| Provider Component | `(root)` | `NamedValueProvider` | `NamedValueProvider.cs` | Partial | `TBD` |
| Core Component | `Net` | `NetworkConnection` | `Net/NetworkConnection.cs` | Partial | `TBD` |
| Helper Component | `Net` | `SmsHelper` | `Net/SmsHelper.cs` | Partial | `TBD` |
| Core Component | `(root)` | `ObjectPair` | `ObjectPair.cs` | Partial | `TBD` |
| Core Component | `(root)` | `PasswordComplexityRequirement` | `PasswordComplexityRequirement.cs` | Partial | `TBD` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | `Properties/AssemblyInfo.cs` | Partial | `TBD` |
| Core Component | `(root)` | `QueryParser` | `QueryParser.cs` | Partial | `TBD` |
| Core Component | `Utilities` | `ConfigUtil` | `Utilities/ConfigUtil.cs` | Partial | `TBD` |
| Helper Component | `Utilities` | `ControlHelper` | `Utilities/ControlHelper.cs` | Partial | `TBD` |
| Core Component | `Utilities` | `ControlInfo` | `Utilities/ControlInfo.cs` | Partial | `TBD` |
| Helper Component | `Utilities/Data` | `CsvHelper` | `Utilities/Data/CsvHelper.cs` | Partial | `TBD` |
| Helper Component | `Utilities/Data` | `DataHelper` | `Utilities/Data/DataHelper.cs` | Partial | `TBD` |
| Helper Component | `Utilities/Data` | `OracleHelper` | `Utilities/Data/OracleHelper.cs` | Partial | `TBD` |
| Helper Component | `Utilities/Data` | `SqlHelper` | `Utilities/Data/SqlHelper.cs` | Partial | `TBD` |
| Core Component | `Utilities/Data` | `XmlUtil` | `Utilities/Data/XmlUtil.cs` | Partial | `TBD` |
| Core Component | `Utilities/IO` | `Compression` | `Utilities/IO/Compression.cs` | Partial | `TBD` |
| Helper Component | `Utilities/IO` | `FileHelper` | `Utilities/IO/FileHelper.cs` | Partial | `TBD` |
| Helper Component | `Utilities/IO` | `FtpHelper` | `Utilities/IO/FtpHelper.cs` | Partial | `TBD` |
| Core Component | `Utilities/IO` | `ImageUtil` | `Utilities/IO/ImageUtil.cs` | Partial | `TBD` |
| Helper Component | `Utilities/IO` | `LogHelper` | `Utilities/IO/LogHelper.cs` | Partial | `TBD` |
| Helper Component | `Utilities/IO` | `TextHelper` | `Utilities/IO/TextHelper.cs` | Partial | `TBD` |
| Core Component | `Utilities` | `JsonUtil` | `Utilities/JsonUtil.cs` | Partial | `TBD` |
| Helper Component | `Utilities` | `MIMEHelper` | `Utilities/MIMEHelper.cs` | Partial | `TBD` |
| Core Component | `Utilities` | `Misc` | `Utilities/Misc.cs` | Partial | `TBD` |
| Helper Component | `Utilities` | `NetHelper` | `Utilities/NetHelper.cs` | Partial | `TBD` |
| Core Component | `Utilities` | `ReflectionUtil` | `Utilities/ReflectionUtil.cs` | Partial | `TBD` |
| Core Component | `Utilities` | `Security` | `Utilities/Security.cs` | Partial | `TBD` |
| Core Component | `Utilities` | `SerializationUtil` | `Utilities/SerializationUtil.cs` | Partial | `TBD` |
| Helper Component | `Utilities` | `SmtpHelper` | `Utilities/SmtpHelper.cs` | Partial | `TBD` |
| Core Component | `Utilities` | `Substituter` | `Utilities/Substituter.cs` | Partial | `TBD` |
| Core Component | `Utilities` | `Test` | `Utilities/Test.cs` | Partial | `TBD` |
| Core Component | `Utilities` | `TimeUtil` | `Utilities/TimeUtil.cs` | Partial | `TBD` |
| Core Component | `Utilities` | `Validator` | `Utilities/Validator.cs` | Partial | `TBD` |
| Helper Component | `Utilities` | `WebHelper` | `Utilities/WebHelper.cs` | Partial | `TBD` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Tracking Notes |
|---|---|---|---|---|---|
| Configuration/Resource | `(root)` | `packages` | `packages.config` | Partial | `TBD` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

