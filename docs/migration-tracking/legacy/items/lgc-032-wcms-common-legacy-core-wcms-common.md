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

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/Core/WCMS.Common :: ConsoleLogger | `legacy/Core/WCMS.Common/ConsoleLogger.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: Constants | `legacy/Core/WCMS.Common/Constants.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Controls :: ImageSecurity | `legacy/Core/WCMS.Common/Controls/ImageSecurity.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Data :: GenericDataColumn | `legacy/Core/WCMS.Common/Data/GenericDataColumn.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Data :: GenericDataRow | `legacy/Core/WCMS.Common/Data/GenericDataRow.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Data :: GenericDataTable | `legacy/Core/WCMS.Common/Data/GenericDataTable.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Data :: GenericSqlDataProvider | `legacy/Core/WCMS.Common/Data/GenericSqlDataProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Data :: IDataProvider | `legacy/Core/WCMS.Common/Data/IDataProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Data :: IQueryFilterElement | `legacy/Core/WCMS.Common/Data/IQueryFilterElement.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Data :: ObjectColumnAttribute | `legacy/Core/WCMS.Common/Data/ObjectColumnAttribute.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Data :: QueryFilter | `legacy/Core/WCMS.Common/Data/QueryFilter.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Data :: QueryFilterElement | `legacy/Core/WCMS.Common/Data/QueryFilterElement.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Data :: SqlDataProviderBase | `legacy/Core/WCMS.Common/Data/SqlDataProviderBase.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: Dates | `legacy/Core/WCMS.Common/Dates.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: FileLogger | `legacy/Core/WCMS.Common/FileLogger.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: IDataManager | `legacy/Core/WCMS.Common/IDataManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: ILogger | `legacy/Core/WCMS.Common/ILogger.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: INamedObjectProvider | `legacy/Core/WCMS.Common/INamedObjectProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: INamedValueProvider | `legacy/Core/WCMS.Common/INamedValueProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: LogManager | `legacy/Core/WCMS.Common/LogManager.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Media :: AsxMedia | `legacy/Core/WCMS.Common/Media/AsxMedia.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: NamedObjectProvider | `legacy/Core/WCMS.Common/NamedObjectProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: NamedValueProvider | `legacy/Core/WCMS.Common/NamedValueProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Net :: NetworkConnection | `legacy/Core/WCMS.Common/Net/NetworkConnection.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Net :: SmsHelper | `legacy/Core/WCMS.Common/Net/SmsHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: ObjectPair | `legacy/Core/WCMS.Common/ObjectPair.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: PasswordComplexityRequirement | `legacy/Core/WCMS.Common/PasswordComplexityRequirement.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common :: QueryParser | `legacy/Core/WCMS.Common/QueryParser.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: ConfigUtil | `legacy/Core/WCMS.Common/Utilities/ConfigUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: ControlHelper | `legacy/Core/WCMS.Common/Utilities/ControlHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: ControlInfo | `legacy/Core/WCMS.Common/Utilities/ControlInfo.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities/Data :: CsvHelper | `legacy/Core/WCMS.Common/Utilities/Data/CsvHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities/Data :: DataHelper | `legacy/Core/WCMS.Common/Utilities/Data/DataHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities/Data :: OracleHelper | `legacy/Core/WCMS.Common/Utilities/Data/OracleHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities/Data :: SqlHelper | `legacy/Core/WCMS.Common/Utilities/Data/SqlHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities/Data :: XmlUtil | `legacy/Core/WCMS.Common/Utilities/Data/XmlUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities/IO :: Compression | `legacy/Core/WCMS.Common/Utilities/IO/Compression.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities/IO :: FileHelper | `legacy/Core/WCMS.Common/Utilities/IO/FileHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities/IO :: FtpHelper | `legacy/Core/WCMS.Common/Utilities/IO/FtpHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities/IO :: ImageUtil | `legacy/Core/WCMS.Common/Utilities/IO/ImageUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities/IO :: LogHelper | `legacy/Core/WCMS.Common/Utilities/IO/LogHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities/IO :: TextHelper | `legacy/Core/WCMS.Common/Utilities/IO/TextHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: JsonUtil | `legacy/Core/WCMS.Common/Utilities/JsonUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: MIMEHelper | `legacy/Core/WCMS.Common/Utilities/MIMEHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: Misc | `legacy/Core/WCMS.Common/Utilities/Misc.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: NetHelper | `legacy/Core/WCMS.Common/Utilities/NetHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: ReflectionUtil | `legacy/Core/WCMS.Common/Utilities/ReflectionUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: Security | `legacy/Core/WCMS.Common/Utilities/Security.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: SerializationUtil | `legacy/Core/WCMS.Common/Utilities/SerializationUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: SmtpHelper | `legacy/Core/WCMS.Common/Utilities/SmtpHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: Substituter | `legacy/Core/WCMS.Common/Utilities/Substituter.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: Test | `legacy/Core/WCMS.Common/Utilities/Test.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: TimeUtil | `legacy/Core/WCMS.Common/Utilities/TimeUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: Validator | `legacy/Core/WCMS.Common/Utilities/Validator.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Core/WCMS.Common/Utilities :: WebHelper | `legacy/Core/WCMS.Common/Utilities/WebHelper.cs` | Library/business component; assess API compatibility and dependencies. |
