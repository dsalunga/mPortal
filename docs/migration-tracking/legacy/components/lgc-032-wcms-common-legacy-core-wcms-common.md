# LGC-032 - WCMS.Common

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-032 |
| Project Type | Library |
| Project File | `legacy/Core/WCMS.Common/WCMS.Common.csproj` |
| Project Directory | `legacy/Core/WCMS.Common` |
| Output Type | Library |
| Target Framework | net8.0 |
| Migration Status | Completed |
| Status Basis | Modern target framework detected (net8.0). |
| Project References | 0 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 55 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Regression validation and documentation hardening. |
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
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: ConsoleLogger | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/ConsoleLogger.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: Constants | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Constants.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Controls :: ImageSecurity | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Controls/ImageSecurity.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: GenericDataColumn | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Data/GenericDataColumn.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: GenericDataRow | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Data/GenericDataRow.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: GenericDataTable | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Data/GenericDataTable.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: GenericSqlDataProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Data/GenericSqlDataProvider.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: IDataProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Data/IDataProvider.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: IQueryFilterElement | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Data/IQueryFilterElement.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: ObjectColumnAttribute | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Data/ObjectColumnAttribute.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: QueryFilter | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Data/QueryFilter.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: QueryFilterElement | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Data/QueryFilterElement.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Data :: SqlDataProviderBase | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Data/SqlDataProviderBase.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: Dates | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Dates.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: FileLogger | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/FileLogger.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: IDataManager | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/IDataManager.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: ILogger | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/ILogger.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: INamedObjectProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/INamedObjectProvider.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: INamedValueProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/INamedValueProvider.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: LogManager | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/LogManager.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Media :: AsxMedia | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Media/AsxMedia.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: NamedObjectProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/NamedObjectProvider.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: NamedValueProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/NamedValueProvider.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Net :: NetworkConnection | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Net/NetworkConnection.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Net :: SmsHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Net/SmsHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: ObjectPair | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/ObjectPair.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: PasswordComplexityRequirement | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/PasswordComplexityRequirement.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common :: QueryParser | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/QueryParser.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: ConfigUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/ConfigUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: ControlHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/ControlHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: ControlInfo | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/ControlInfo.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/Data :: CsvHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/Data/CsvHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/Data :: DataHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/Data/DataHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/Data :: OracleHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/Data/OracleHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/Data :: SqlHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/Data/SqlHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/Data :: XmlUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/Data/XmlUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/IO :: Compression | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/IO/Compression.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/IO :: FileHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/IO/FileHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/IO :: FtpHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/IO/FtpHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/IO :: ImageUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/IO/ImageUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/IO :: LogHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/IO/LogHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities/IO :: TextHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/IO/TextHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: JsonUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/JsonUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: MIMEHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/MIMEHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: Misc | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/Misc.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: NetHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/NetHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: ReflectionUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/ReflectionUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: Security | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/Security.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: SerializationUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/SerializationUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: SmtpHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/SmtpHelper.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: Substituter | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/Substituter.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: Test | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/Test.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: TimeUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/TimeUtil.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: Validator | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/Validator.cs` |
| LGC-032 | Completed | Class Component | legacy/Core/WCMS.Common/Utilities :: WebHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Core/WCMS.Common/Utilities/WebHelper.cs` |
