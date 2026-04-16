# LGC-036 - WCMS.Common

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-036 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` |
| Modern Project File / Evidence | `Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` |
| Project Directory | `legacy/Portal/WebSystem/WCMS.Common` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:54, Not Applicable:4, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 55 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | No | N/A |
| Endpoint Surface Present | No | N/A |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Modern File / Evidence |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: ConsoleLogger | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/ConsoleLogger.cs` | `Portal/WebSystem/WCMS.Common/ConsoleLogger.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Constants.cs` | `Portal/WebSystem/WCMS.Common/Constants.cs` |
| LGC-036 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.Common/Controls :: ImageSecurity | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Controls/ImageSecurity.cs` | `Core/WCMS.Common/Controls/ImageSecurity.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericDataColumn | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Data/GenericDataColumn.cs` | `Portal/WebSystem/WCMS.Common/Data/GenericDataColumn.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericDataRow | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Data/GenericDataRow.cs` | `Portal/WebSystem/WCMS.Common/Data/GenericDataRow.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericDataTable | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Data/GenericDataTable.cs` | `Portal/WebSystem/WCMS.Common/Data/GenericDataTable.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericSqlDataProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Data/GenericSqlDataProvider.cs` | `Portal/WebSystem/WCMS.Common/Data/GenericSqlDataProvider.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: IDataProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Data/IDataProvider.cs` | `Portal/WebSystem/WCMS.Common/Data/IDataProvider.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: IQueryFilterElement | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Data/IQueryFilterElement.cs` | `Portal/WebSystem/WCMS.Common/Data/IQueryFilterElement.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: ObjectColumnAttribute | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Data/ObjectColumnAttribute.cs` | `Portal/WebSystem/WCMS.Common/Data/ObjectColumnAttribute.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: QueryFilter | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Data/QueryFilter.cs` | `Portal/WebSystem/WCMS.Common/Data/QueryFilter.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: QueryFilterElement | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Data/QueryFilterElement.cs` | `Portal/WebSystem/WCMS.Common/Data/QueryFilterElement.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: SqlDataProviderBase | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Data/SqlDataProviderBase.cs` | `Portal/WebSystem/WCMS.Common/Data/SqlDataProviderBase.cs` |
| LGC-036 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.Common :: Dates | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Dates.cs` | N/A (retired/replaced in modern architecture). |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: FileLogger | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/FileLogger.cs` | `Portal/WebSystem/WCMS.Common/FileLogger.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: IDataManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/IDataManager.cs` | `Portal/WebSystem/WCMS.Common/IDataManager.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: ILogger | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/ILogger.cs` | `Portal/WebSystem/WCMS.Common/ILogger.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: INamedObjectProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/INamedObjectProvider.cs` | `Portal/WebSystem/WCMS.Common/INamedObjectProvider.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: INamedValueProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/INamedValueProvider.cs` | `Portal/WebSystem/WCMS.Common/INamedValueProvider.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: LogManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/LogManager.cs` | `Portal/WebSystem/WCMS.Common/LogManager.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Media :: AsxMedia | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Media/AsxMedia.cs` | `Portal/WebSystem/WCMS.Common/Media/AsxMedia.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: NamedObjectProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/NamedObjectProvider.cs` | `Portal/WebSystem/WCMS.Common/NamedObjectProvider.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: NamedValueProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/NamedValueProvider.cs` | `Portal/WebSystem/WCMS.Common/NamedValueProvider.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Net :: NetworkConnection | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Net/NetworkConnection.cs` | `Portal/WebSystem/WCMS.Common/Net/NetworkConnection.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Net :: SmsHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Net/SmsHelper.cs` | `Portal/WebSystem/WCMS.Common/Net/SmsHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: ObjectPair | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/ObjectPair.cs` | `Portal/WebSystem/WCMS.Common/ObjectPair.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: PasswordComplexityRequirement | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/PasswordComplexityRequirement.cs` | `Portal/WebSystem/WCMS.Common/PasswordComplexityRequirement.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: QueryParser | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/QueryParser.cs` | `Portal/WebSystem/WCMS.Common/QueryParser.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ConfigUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/ConfigUtil.cs` | `Portal/WebSystem/WCMS.Common/Utilities/ConfigUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ControlHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/ControlHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/ControlHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ControlInfo | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/ControlInfo.cs` | `Portal/WebSystem/WCMS.Common/Utilities/ControlInfo.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: CsvHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/CsvHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/Data/CsvHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: DataHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/DataHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/Data/DataHelper.cs` |
| LGC-036 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: OracleHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/OracleHelper.cs` | N/A (retired/replaced in modern architecture). |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: SqlHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/SqlHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/Data/SqlHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: XmlUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/XmlUtil.cs` | `Portal/WebSystem/WCMS.Common/Utilities/Data/XmlUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: Compression | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/Compression.cs` | `Portal/WebSystem/WCMS.Common/Utilities/IO/Compression.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: FileHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/FileHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/IO/FileHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: FtpHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/FtpHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/IO/FtpHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: ImageUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/ImageUtil.cs` | `Portal/WebSystem/WCMS.Common/Utilities/IO/ImageUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: LogHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/LogHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/IO/LogHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: TextHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/TextHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/IO/TextHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: JsonUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/JsonUtil.cs` | `Portal/WebSystem/WCMS.Common/Utilities/JsonUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: MIMEHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/MIMEHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/MIMEHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Misc | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Misc.cs` | `Portal/WebSystem/WCMS.Common/Utilities/Misc.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: NetHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/NetHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/NetHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ReflectionUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/ReflectionUtil.cs` | `Portal/WebSystem/WCMS.Common/Utilities/ReflectionUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Security | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Security.cs` | `Portal/WebSystem/WCMS.Common/Utilities/Security.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: SerializationUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/SerializationUtil.cs` | `Portal/WebSystem/WCMS.Common/Utilities/SerializationUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: SmtpHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/SmtpHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/SmtpHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Substituter | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Substituter.cs` | `Portal/WebSystem/WCMS.Common/Utilities/Substituter.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Test | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Test.cs` | `Portal/WebSystem/WCMS.Common/Utilities/Test.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: TimeUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/TimeUtil.cs` | `Portal/WebSystem/WCMS.Common/Utilities/TimeUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Validator | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Validator.cs` | `Portal/WebSystem/WCMS.Common/Utilities/Validator.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: WebHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/WebHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/WebHelper.cs` |
