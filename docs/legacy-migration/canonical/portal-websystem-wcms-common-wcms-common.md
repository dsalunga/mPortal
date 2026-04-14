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

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Core Component | `(root)` | `ConsoleLogger` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `ConsoleLogger.cs` |
| Core Component | `(root)` | `Constants` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Constants.cs` |
| Core Component | `Controls` | `ImageSecurity` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Controls/ImageSecurity.cs` |
| Core Component | `Data` | `GenericDataColumn` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Data/GenericDataColumn.cs` |
| Core Component | `Data` | `GenericDataRow` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Data/GenericDataRow.cs` |
| Core Component | `Data` | `GenericDataTable` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Data/GenericDataTable.cs` |
| Provider Component | `Data` | `GenericSqlDataProvider` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Data/GenericSqlDataProvider.cs` |
| Provider Component | `Data` | `IDataProvider` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Data/IDataProvider.cs` |
| Core Component | `Data` | `IQueryFilterElement` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Data/IQueryFilterElement.cs` |
| Core Component | `Data` | `ObjectColumnAttribute` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Data/ObjectColumnAttribute.cs` |
| Core Component | `Data` | `QueryFilter` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Data/QueryFilter.cs` |
| Core Component | `Data` | `QueryFilterElement` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Data/QueryFilterElement.cs` |
| Core Component | `Data` | `SqlDataProviderBase` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Data/SqlDataProviderBase.cs` |
| Core Component | `(root)` | `Dates` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Dates.cs` |
| Core Component | `(root)` | `FileLogger` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `FileLogger.cs` |
| Manager Component | `(root)` | `IDataManager` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `IDataManager.cs` |
| Core Component | `(root)` | `ILogger` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `ILogger.cs` |
| Provider Component | `(root)` | `INamedObjectProvider` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `INamedObjectProvider.cs` |
| Provider Component | `(root)` | `INamedValueProvider` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `INamedValueProvider.cs` |
| Manager Component | `(root)` | `LogManager` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `LogManager.cs` |
| Core Component | `Media` | `AsxMedia` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Media/AsxMedia.cs` |
| Provider Component | `(root)` | `NamedObjectProvider` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `NamedObjectProvider.cs` |
| Provider Component | `(root)` | `NamedValueProvider` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `NamedValueProvider.cs` |
| Core Component | `Net` | `NetworkConnection` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Net/NetworkConnection.cs` |
| Helper Component | `Net` | `SmsHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Net/SmsHelper.cs` |
| Core Component | `(root)` | `ObjectPair` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `ObjectPair.cs` |
| Core Component | `(root)` | `PasswordComplexityRequirement` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `PasswordComplexityRequirement.cs` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Properties/AssemblyInfo.cs` |
| Core Component | `(root)` | `QueryParser` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `QueryParser.cs` |
| Core Component | `Utilities` | `ConfigUtil` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/ConfigUtil.cs` |
| Helper Component | `Utilities` | `ControlHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/ControlHelper.cs` |
| Core Component | `Utilities` | `ControlInfo` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/ControlInfo.cs` |
| Helper Component | `Utilities/Data` | `CsvHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/Data/CsvHelper.cs` |
| Helper Component | `Utilities/Data` | `DataHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/Data/DataHelper.cs` |
| Helper Component | `Utilities/Data` | `OracleHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/Data/OracleHelper.cs` |
| Helper Component | `Utilities/Data` | `SqlHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/Data/SqlHelper.cs` |
| Core Component | `Utilities/Data` | `XmlUtil` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/Data/XmlUtil.cs` |
| Core Component | `Utilities/IO` | `Compression` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/IO/Compression.cs` |
| Helper Component | `Utilities/IO` | `FileHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/IO/FileHelper.cs` |
| Helper Component | `Utilities/IO` | `FtpHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/IO/FtpHelper.cs` |
| Core Component | `Utilities/IO` | `ImageUtil` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/IO/ImageUtil.cs` |
| Helper Component | `Utilities/IO` | `LogHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/IO/LogHelper.cs` |
| Helper Component | `Utilities/IO` | `TextHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/IO/TextHelper.cs` |
| Core Component | `Utilities` | `JsonUtil` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/JsonUtil.cs` |
| Helper Component | `Utilities` | `MIMEHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/MIMEHelper.cs` |
| Core Component | `Utilities` | `Misc` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/Misc.cs` |
| Helper Component | `Utilities` | `NetHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/NetHelper.cs` |
| Core Component | `Utilities` | `ReflectionUtil` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/ReflectionUtil.cs` |
| Core Component | `Utilities` | `Security` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/Security.cs` |
| Core Component | `Utilities` | `SerializationUtil` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/SerializationUtil.cs` |
| Helper Component | `Utilities` | `SmtpHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/SmtpHelper.cs` |
| Core Component | `Utilities` | `Substituter` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/Substituter.cs` |
| Core Component | `Utilities` | `Test` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/Test.cs` |
| Core Component | `Utilities` | `TimeUtil` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/TimeUtil.cs` |
| Core Component | `Utilities` | `Validator` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/Validator.cs` |
| Helper Component | `Utilities` | `WebHelper` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `Utilities/WebHelper.cs` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Configuration/Resource | `(root)` | `packages` | Partial | Complete remaining parity gaps, then decommission legacy path. | `TBD` | `packages.config` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

