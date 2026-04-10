# LGC-036 - WCMS.Common

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-036 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/WCMS.Common/WCMS.Common.csproj` |
| Project Directory | `legacy/Portal/WebSystem/WCMS.Common` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Partial |
| Status Basis | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 55 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Execution (In Progress) | Close remaining legacy runtime/UI/endpoint gaps and define cutover tests. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- | --- | --- |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: ConsoleLogger | `legacy/Portal/WebSystem/WCMS.Common/ConsoleLogger.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: Constants | `legacy/Portal/WebSystem/WCMS.Common/Constants.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Controls :: ImageSecurity | `legacy/Portal/WebSystem/WCMS.Common/Controls/ImageSecurity.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericDataColumn | `legacy/Portal/WebSystem/WCMS.Common/Data/GenericDataColumn.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericDataRow | `legacy/Portal/WebSystem/WCMS.Common/Data/GenericDataRow.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericDataTable | `legacy/Portal/WebSystem/WCMS.Common/Data/GenericDataTable.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericSqlDataProvider | `legacy/Portal/WebSystem/WCMS.Common/Data/GenericSqlDataProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: IDataProvider | `legacy/Portal/WebSystem/WCMS.Common/Data/IDataProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: IQueryFilterElement | `legacy/Portal/WebSystem/WCMS.Common/Data/IQueryFilterElement.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: ObjectColumnAttribute | `legacy/Portal/WebSystem/WCMS.Common/Data/ObjectColumnAttribute.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: QueryFilter | `legacy/Portal/WebSystem/WCMS.Common/Data/QueryFilter.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: QueryFilterElement | `legacy/Portal/WebSystem/WCMS.Common/Data/QueryFilterElement.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: SqlDataProviderBase | `legacy/Portal/WebSystem/WCMS.Common/Data/SqlDataProviderBase.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: Dates | `legacy/Portal/WebSystem/WCMS.Common/Dates.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: FileLogger | `legacy/Portal/WebSystem/WCMS.Common/FileLogger.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: IDataManager | `legacy/Portal/WebSystem/WCMS.Common/IDataManager.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: ILogger | `legacy/Portal/WebSystem/WCMS.Common/ILogger.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: INamedObjectProvider | `legacy/Portal/WebSystem/WCMS.Common/INamedObjectProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: INamedValueProvider | `legacy/Portal/WebSystem/WCMS.Common/INamedValueProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: LogManager | `legacy/Portal/WebSystem/WCMS.Common/LogManager.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Media :: AsxMedia | `legacy/Portal/WebSystem/WCMS.Common/Media/AsxMedia.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: NamedObjectProvider | `legacy/Portal/WebSystem/WCMS.Common/NamedObjectProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: NamedValueProvider | `legacy/Portal/WebSystem/WCMS.Common/NamedValueProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Net :: NetworkConnection | `legacy/Portal/WebSystem/WCMS.Common/Net/NetworkConnection.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Net :: SmsHelper | `legacy/Portal/WebSystem/WCMS.Common/Net/SmsHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: ObjectPair | `legacy/Portal/WebSystem/WCMS.Common/ObjectPair.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: PasswordComplexityRequirement | `legacy/Portal/WebSystem/WCMS.Common/PasswordComplexityRequirement.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: QueryParser | `legacy/Portal/WebSystem/WCMS.Common/QueryParser.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ConfigUtil | `legacy/Portal/WebSystem/WCMS.Common/Utilities/ConfigUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ControlHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/ControlHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ControlInfo | `legacy/Portal/WebSystem/WCMS.Common/Utilities/ControlInfo.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: CsvHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/CsvHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: DataHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/DataHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: OracleHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/OracleHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: SqlHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/SqlHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: XmlUtil | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/XmlUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: Compression | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/Compression.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: FileHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/FileHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: FtpHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/FtpHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: ImageUtil | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/ImageUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: LogHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/LogHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: TextHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/TextHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: JsonUtil | `legacy/Portal/WebSystem/WCMS.Common/Utilities/JsonUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: MIMEHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/MIMEHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Misc | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Misc.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: NetHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/NetHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ReflectionUtil | `legacy/Portal/WebSystem/WCMS.Common/Utilities/ReflectionUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Security | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Security.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: SerializationUtil | `legacy/Portal/WebSystem/WCMS.Common/Utilities/SerializationUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: SmtpHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/SmtpHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Substituter | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Substituter.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Test | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Test.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: TimeUtil | `legacy/Portal/WebSystem/WCMS.Common/Utilities/TimeUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Validator | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Validator.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: WebHelper | `legacy/Portal/WebSystem/WCMS.Common/Utilities/WebHelper.cs` | Library/business component; assess API compatibility and dependencies. |
