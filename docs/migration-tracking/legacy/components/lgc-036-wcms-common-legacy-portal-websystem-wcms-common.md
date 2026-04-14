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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: ConsoleLogger | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/ConsoleLogger.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: Constants | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Constants.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Controls :: ImageSecurity | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Controls/ImageSecurity.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericDataColumn | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Data/GenericDataColumn.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericDataRow | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Data/GenericDataRow.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericDataTable | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Data/GenericDataTable.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: GenericSqlDataProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Data/GenericSqlDataProvider.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: IDataProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Data/IDataProvider.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: IQueryFilterElement | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Data/IQueryFilterElement.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: ObjectColumnAttribute | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Data/ObjectColumnAttribute.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: QueryFilter | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Data/QueryFilter.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: QueryFilterElement | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Data/QueryFilterElement.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Data :: SqlDataProviderBase | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Data/SqlDataProviderBase.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: Dates | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Dates.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: FileLogger | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/FileLogger.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: IDataManager | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/IDataManager.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: ILogger | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/ILogger.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: INamedObjectProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/INamedObjectProvider.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: INamedValueProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/INamedValueProvider.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: LogManager | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/LogManager.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Media :: AsxMedia | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Media/AsxMedia.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: NamedObjectProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/NamedObjectProvider.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: NamedValueProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/NamedValueProvider.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Net :: NetworkConnection | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Net/NetworkConnection.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Net :: SmsHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Net/SmsHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: ObjectPair | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/ObjectPair.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: PasswordComplexityRequirement | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/PasswordComplexityRequirement.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common :: QueryParser | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/QueryParser.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ConfigUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/ConfigUtil.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ControlHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/ControlHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ControlInfo | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/ControlInfo.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: CsvHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/CsvHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: DataHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/DataHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: OracleHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/OracleHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: SqlHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/SqlHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/Data :: XmlUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Data/XmlUtil.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: Compression | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/Compression.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: FileHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/FileHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: FtpHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/FtpHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: ImageUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/ImageUtil.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: LogHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/LogHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities/IO :: TextHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/IO/TextHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: JsonUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/JsonUtil.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: MIMEHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/MIMEHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Misc | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Misc.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: NetHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/NetHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: ReflectionUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/ReflectionUtil.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Security | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Security.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: SerializationUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/SerializationUtil.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: SmtpHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/SmtpHelper.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Substituter | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Substituter.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Test | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Test.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: TimeUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/TimeUtil.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: Validator | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/Validator.cs` |
| LGC-036 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.Common/Utilities :: WebHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.Common/Utilities/WebHelper.cs` |
