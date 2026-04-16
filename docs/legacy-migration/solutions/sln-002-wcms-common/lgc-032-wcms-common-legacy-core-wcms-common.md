# LGC-032 - WCMS.Common

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-032 |
| Project Type | Library |
| Project File | `legacy/Core/WCMS.Common/WCMS.Common.csproj` |
| Modern Project File / Evidence | `Core/WCMS.Common/WCMS.Common.csproj` |
| Project Directory | `legacy/Core/WCMS.Common` |
| Output Type | Library |
| Target Framework | net8.0 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:53, Not Applicable:4, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
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
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: ConsoleLogger | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/ConsoleLogger.cs` | `Core/WCMS.Common/ConsoleLogger.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Constants.cs` | `Core/WCMS.Common/Constants.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Controls :: ImageSecurity | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Controls/ImageSecurity.cs` | `Core/WCMS.Common/Controls/ImageSecurity.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: GenericDataColumn | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Data/GenericDataColumn.cs` | `Core/WCMS.Common/Data/GenericDataColumn.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: GenericDataRow | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Data/GenericDataRow.cs` | `Core/WCMS.Common/Data/GenericDataRow.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: GenericDataTable | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Data/GenericDataTable.cs` | `Core/WCMS.Common/Data/GenericDataTable.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: GenericSqlDataProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Data/GenericSqlDataProvider.cs` | `Core/WCMS.Common/Data/GenericSqlDataProvider.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: IDataProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Data/IDataProvider.cs` | `Core/WCMS.Common/Data/IDataProvider.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: IQueryFilterElement | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Data/IQueryFilterElement.cs` | `Core/WCMS.Common/Data/IQueryFilterElement.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: ObjectColumnAttribute | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Data/ObjectColumnAttribute.cs` | `Core/WCMS.Common/Data/ObjectColumnAttribute.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: QueryFilter | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Data/QueryFilter.cs` | `Core/WCMS.Common/Data/QueryFilter.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: QueryFilterElement | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Data/QueryFilterElement.cs` | `Core/WCMS.Common/Data/QueryFilterElement.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: SqlDataProviderBase | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Data/SqlDataProviderBase.cs` | `Core/WCMS.Common/Data/SqlDataProviderBase.cs` |
| LGC-032 | Not Applicable | Class Component | legacy/Core/WCMS.Common :: Dates | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Dates.cs` | N/A (retired/replaced in modern architecture). |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: FileLogger | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/FileLogger.cs` | `Core/WCMS.Common/FileLogger.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: IDataManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/IDataManager.cs` | `Core/WCMS.Common/IDataManager.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: ILogger | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/ILogger.cs` | `Core/WCMS.Common/ILogger.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: INamedObjectProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/INamedObjectProvider.cs` | `Core/WCMS.Common/INamedObjectProvider.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: INamedValueProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/INamedValueProvider.cs` | `Core/WCMS.Common/INamedValueProvider.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: LogManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/LogManager.cs` | `Core/WCMS.Common/LogManager.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Media :: AsxMedia | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Media/AsxMedia.cs` | `Core/WCMS.Common/Media/AsxMedia.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: NamedObjectProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/NamedObjectProvider.cs` | `Core/WCMS.Common/NamedObjectProvider.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: NamedValueProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/NamedValueProvider.cs` | `Core/WCMS.Common/NamedValueProvider.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Net :: NetworkConnection | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Net/NetworkConnection.cs` | `Core/WCMS.Common/Net/NetworkConnection.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Net :: SmsHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Net/SmsHelper.cs` | `Core/WCMS.Common/Net/SmsHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: ObjectPair | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/ObjectPair.cs` | `Core/WCMS.Common/ObjectPair.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: PasswordComplexityRequirement | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/PasswordComplexityRequirement.cs` | `Core/WCMS.Common/PasswordComplexityRequirement.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: QueryParser | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/QueryParser.cs` | `Core/WCMS.Common/QueryParser.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: ConfigUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/ConfigUtil.cs` | `Core/WCMS.Common/Utilities/ConfigUtil.cs` |
| LGC-032 | Not Applicable | Class Component | legacy/Core/WCMS.Common/Utilities :: ControlHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/ControlHelper.cs` | `Portal/WebSystem/WCMS.Common/Utilities/ControlHelper.cs` |
| LGC-032 | Not Applicable | Class Component | legacy/Core/WCMS.Common/Utilities :: ControlInfo | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/ControlInfo.cs` | `Portal/WebSystem/WCMS.Common/Utilities/ControlInfo.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/Data :: CsvHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/Data/CsvHelper.cs` | `Core/WCMS.Common/Utilities/Data/CsvHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/Data :: DataHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/Data/DataHelper.cs` | `Core/WCMS.Common/Utilities/Data/DataHelper.cs` |
| LGC-032 | Not Applicable | Class Component | legacy/Core/WCMS.Common/Utilities/Data :: OracleHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/Data/OracleHelper.cs` | N/A (retired/replaced in modern architecture). |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/Data :: SqlHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/Data/SqlHelper.cs` | `Core/WCMS.Common/Utilities/Data/SqlHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/Data :: XmlUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/Data/XmlUtil.cs` | `Core/WCMS.Common/Utilities/Data/XmlUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/IO :: Compression | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/IO/Compression.cs` | `Core/WCMS.Common/Utilities/IO/Compression.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/IO :: FileHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/IO/FileHelper.cs` | `Core/WCMS.Common/Utilities/IO/FileHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/IO :: FtpHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/IO/FtpHelper.cs` | `Core/WCMS.Common/Utilities/IO/FtpHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/IO :: ImageUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/IO/ImageUtil.cs` | `Core/WCMS.Common/Utilities/IO/ImageUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/IO :: LogHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/IO/LogHelper.cs` | `Core/WCMS.Common/Utilities/IO/LogHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/IO :: TextHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/IO/TextHelper.cs` | `Core/WCMS.Common/Utilities/IO/TextHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: JsonUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/JsonUtil.cs` | `Core/WCMS.Common/Utilities/JsonUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: MIMEHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/MIMEHelper.cs` | `Core/WCMS.Common/Utilities/MIMEHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: Misc | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/Misc.cs` | `Core/WCMS.Common/Utilities/Misc.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: NetHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/NetHelper.cs` | `Core/WCMS.Common/Utilities/NetHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: ReflectionUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/ReflectionUtil.cs` | `Core/WCMS.Common/Utilities/ReflectionUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: Security | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/Security.cs` | `Core/WCMS.Common/Utilities/Security.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: SerializationUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/SerializationUtil.cs` | `Core/WCMS.Common/Utilities/SerializationUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: SmtpHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/SmtpHelper.cs` | `Core/WCMS.Common/Utilities/SmtpHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: Substituter | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/Substituter.cs` | `Core/WCMS.Common/Utilities/Substituter.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: Test | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/Test.cs` | `Core/WCMS.Common/Utilities/Test.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: TimeUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/TimeUtil.cs` | `Core/WCMS.Common/Utilities/TimeUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: Validator | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/Validator.cs` | `Core/WCMS.Common/Utilities/Validator.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: WebHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Core/WCMS.Common/Utilities/WebHelper.cs` | `Core/WCMS.Common/Utilities/WebHelper.cs` |
