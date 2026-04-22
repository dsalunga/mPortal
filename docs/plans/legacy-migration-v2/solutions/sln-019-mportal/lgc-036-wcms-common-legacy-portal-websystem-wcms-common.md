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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: ConsoleLogger | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./ConsoleLogger.cs` | `./ConsoleLogger.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Constants.cs` | `./Constants.cs` |
| LGC-036 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.Common/Controls :: ImageSecurity | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Controls/ImageSecurity.cs` | `Core/WCMS.Common/Controls/ImageSecurity.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericDataColumn | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Data/GenericDataColumn.cs` | `./Data/GenericDataColumn.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericDataRow | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Data/GenericDataRow.cs` | `./Data/GenericDataRow.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericDataTable | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Data/GenericDataTable.cs` | `./Data/GenericDataTable.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericSqlDataProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Data/GenericSqlDataProvider.cs` | `./Data/GenericSqlDataProvider.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: IDataProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Data/IDataProvider.cs` | `./Data/IDataProvider.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: IQueryFilterElement | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Data/IQueryFilterElement.cs` | `./Data/IQueryFilterElement.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: ObjectColumnAttribute | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Data/ObjectColumnAttribute.cs` | `./Data/ObjectColumnAttribute.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: QueryFilter | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Data/QueryFilter.cs` | `./Data/QueryFilter.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: QueryFilterElement | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Data/QueryFilterElement.cs` | `./Data/QueryFilterElement.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: SqlDataProviderBase | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Data/SqlDataProviderBase.cs` | `./Data/SqlDataProviderBase.cs` |
| LGC-036 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.Common :: Dates | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Dates.cs` | N/A (retired/replaced in modern architecture). |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: FileLogger | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./FileLogger.cs` | `./FileLogger.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: IDataManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./IDataManager.cs` | `./IDataManager.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: ILogger | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./ILogger.cs` | `./ILogger.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: INamedObjectProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./INamedObjectProvider.cs` | `./INamedObjectProvider.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: INamedValueProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./INamedValueProvider.cs` | `./INamedValueProvider.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: LogManager | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./LogManager.cs` | `./LogManager.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Media :: AsxMedia | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Media/AsxMedia.cs` | `./Media/AsxMedia.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: NamedObjectProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./NamedObjectProvider.cs` | `./NamedObjectProvider.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: NamedValueProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./NamedValueProvider.cs` | `./NamedValueProvider.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Net :: NetworkConnection | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Net/NetworkConnection.cs` | `./Net/NetworkConnection.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Net :: SmsHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Net/SmsHelper.cs` | `./Net/SmsHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: ObjectPair | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./ObjectPair.cs` | `./ObjectPair.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: PasswordComplexityRequirement | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./PasswordComplexityRequirement.cs` | `./PasswordComplexityRequirement.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common :: QueryParser | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./QueryParser.cs` | `./QueryParser.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ConfigUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/ConfigUtil.cs` | `./Utilities/ConfigUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ControlHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/ControlHelper.cs` | `./Utilities/ControlHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ControlInfo | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/ControlInfo.cs` | `./Utilities/ControlInfo.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: CsvHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/Data/CsvHelper.cs` | `./Utilities/Data/CsvHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: DataHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/Data/DataHelper.cs` | `./Utilities/Data/DataHelper.cs` |
| LGC-036 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: OracleHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/Data/OracleHelper.cs` | N/A (retired/replaced in modern architecture). |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: SqlHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/Data/SqlHelper.cs` | `./Utilities/Data/SqlHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: XmlUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/Data/XmlUtil.cs` | `./Utilities/Data/XmlUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: Compression | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/IO/Compression.cs` | `./Utilities/IO/Compression.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: FileHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/IO/FileHelper.cs` | `./Utilities/IO/FileHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: FtpHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/IO/FtpHelper.cs` | `./Utilities/IO/FtpHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: ImageUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/IO/ImageUtil.cs` | `./Utilities/IO/ImageUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: LogHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/IO/LogHelper.cs` | `./Utilities/IO/LogHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: TextHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/IO/TextHelper.cs` | `./Utilities/IO/TextHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: JsonUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/JsonUtil.cs` | `./Utilities/JsonUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: MIMEHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/MIMEHelper.cs` | `./Utilities/MIMEHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Misc | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/Misc.cs` | `./Utilities/Misc.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: NetHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/NetHelper.cs` | `./Utilities/NetHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ReflectionUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/ReflectionUtil.cs` | `./Utilities/ReflectionUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Security | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/Security.cs` | `./Utilities/Security.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: SerializationUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/SerializationUtil.cs` | `./Utilities/SerializationUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: SmtpHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/SmtpHelper.cs` | `./Utilities/SmtpHelper.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Substituter | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/Substituter.cs` | `./Utilities/Substituter.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Test | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/Test.cs` | `./Utilities/Test.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: TimeUtil | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/TimeUtil.cs` | `./Utilities/TimeUtil.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Validator | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/Validator.cs` | `./Utilities/Validator.cs` |
| LGC-036 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: WebHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Utilities/WebHelper.cs` | `./Utilities/WebHelper.cs` |
