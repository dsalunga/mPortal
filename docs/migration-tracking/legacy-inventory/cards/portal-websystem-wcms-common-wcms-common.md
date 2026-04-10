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
| Configuration/Resource | 1 |
| Assembly Metadata | 1 |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Core Component | `(root)` | `ConsoleLogger` | `ConsoleLogger.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `(root)` | `Constants` | `Constants.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Controls` | `ImageSecurity` | `Controls/ImageSecurity.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Data` | `GenericDataColumn` | `Data/GenericDataColumn.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Data` | `GenericDataRow` | `Data/GenericDataRow.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Data` | `GenericDataTable` | `Data/GenericDataTable.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Provider Component | `Data` | `GenericSqlDataProvider` | `Data/GenericSqlDataProvider.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Provider Component | `Data` | `IDataProvider` | `Data/IDataProvider.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Data` | `IQueryFilterElement` | `Data/IQueryFilterElement.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Data` | `ObjectColumnAttribute` | `Data/ObjectColumnAttribute.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Data` | `QueryFilter` | `Data/QueryFilter.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Data` | `QueryFilterElement` | `Data/QueryFilterElement.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Data` | `SqlDataProviderBase` | `Data/SqlDataProviderBase.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `(root)` | `Dates` | `Dates.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `(root)` | `FileLogger` | `FileLogger.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Manager Component | `(root)` | `IDataManager` | `IDataManager.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `(root)` | `ILogger` | `ILogger.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Provider Component | `(root)` | `INamedObjectProvider` | `INamedObjectProvider.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Provider Component | `(root)` | `INamedValueProvider` | `INamedValueProvider.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Manager Component | `(root)` | `LogManager` | `LogManager.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Media` | `AsxMedia` | `Media/AsxMedia.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Provider Component | `(root)` | `NamedObjectProvider` | `NamedObjectProvider.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Provider Component | `(root)` | `NamedValueProvider` | `NamedValueProvider.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Net` | `NetworkConnection` | `Net/NetworkConnection.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Net` | `SmsHelper` | `Net/SmsHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `(root)` | `ObjectPair` | `ObjectPair.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `(root)` | `PasswordComplexityRequirement` | `PasswordComplexityRequirement.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | `Properties/AssemblyInfo.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `(root)` | `QueryParser` | `QueryParser.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities` | `ConfigUtil` | `Utilities/ConfigUtil.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities` | `ControlHelper` | `Utilities/ControlHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities` | `ControlInfo` | `Utilities/ControlInfo.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities/Data` | `CsvHelper` | `Utilities/Data/CsvHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities/Data` | `DataHelper` | `Utilities/Data/DataHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities/Data` | `OracleHelper` | `Utilities/Data/OracleHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities/Data` | `SqlHelper` | `Utilities/Data/SqlHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities/Data` | `XmlUtil` | `Utilities/Data/XmlUtil.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities/IO` | `Compression` | `Utilities/IO/Compression.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities/IO` | `FileHelper` | `Utilities/IO/FileHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities/IO` | `FtpHelper` | `Utilities/IO/FtpHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities/IO` | `ImageUtil` | `Utilities/IO/ImageUtil.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities/IO` | `LogHelper` | `Utilities/IO/LogHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities/IO` | `TextHelper` | `Utilities/IO/TextHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities` | `JsonUtil` | `Utilities/JsonUtil.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities` | `MIMEHelper` | `Utilities/MIMEHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities` | `Misc` | `Utilities/Misc.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities` | `NetHelper` | `Utilities/NetHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities` | `ReflectionUtil` | `Utilities/ReflectionUtil.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities` | `Security` | `Utilities/Security.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities` | `SerializationUtil` | `Utilities/SerializationUtil.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities` | `SmtpHelper` | `Utilities/SmtpHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities` | `Substituter` | `Utilities/Substituter.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities` | `Test` | `Utilities/Test.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities` | `TimeUtil` | `Utilities/TimeUtil.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Core Component | `Utilities` | `Validator` | `Utilities/Validator.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |
| Helper Component | `Utilities` | `WebHelper` | `Utilities/WebHelper.cs` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Configuration/Resource | `(root)` | `packages` | `packages.config` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

