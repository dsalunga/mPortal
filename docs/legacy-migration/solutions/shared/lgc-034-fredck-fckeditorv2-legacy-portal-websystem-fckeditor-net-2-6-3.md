# LGC-034 - FredCK.FCKeditorV2

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-034 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.csproj` |
| Modern Project File / Evidence | TipTap OSS replacement (no migrated project file). |
| Project Directory | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Do Not Migrate As-Is |
| Status Basis | Replacement-only scope verified from inventory: FCKeditor artifacts are Do Not Migrate As-Is and replaced with TipTap OSS. |
| Project References | 0 |
| Surface Artifacts | 8 |
| Component/Class Artifacts | 18 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Do Not Migrate As-Is | Replace all legacy editor surfaces with TipTap OSS and close with sanitization parity checks. |
| WebForms Surface Present | Yes | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | No | Do not port FCKeditor classes; implement TipTap integration and migrate content safely. |

## Pages And Views

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) | Code-Behind / Pair (relative to Project Directory) |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample01.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `./_samples/aspx/1.1/sample01.aspx` | TipTap OSS replacement (no 1:1 file). |  |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample02.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `./_samples/aspx/1.1/sample02.aspx` | TipTap OSS replacement (no 1:1 file). | `./_samples/aspx/1.1/sample02.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample03.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `./_samples/aspx/1.1/sample03.aspx` | TipTap OSS replacement (no 1:1 file). | `./_samples/aspx/1.1/sample03.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample04.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `./_samples/aspx/1.1/sample04.aspx` | TipTap OSS replacement (no 1:1 file). | `./_samples/aspx/1.1/sample04.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample01.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `./_samples/aspx/2.0/sample01.aspx` | TipTap OSS replacement (no 1:1 file). |  |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample02.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `./_samples/aspx/2.0/sample02.aspx` | TipTap OSS replacement (no 1:1 file). | `./_samples/aspx/2.0/sample02.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample03.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `./_samples/aspx/2.0/sample03.aspx` | TipTap OSS replacement (no 1:1 file). | `./_samples/aspx/2.0/sample03.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample04.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `./_samples/aspx/2.0/sample04.aspx` | TipTap OSS replacement (no 1:1 file). | `./_samples/aspx/2.0/sample04.aspx.cs` |

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: FCKeditor | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./FCKeditor.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: FCKeditorConfigurations | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./FCKeditorConfigurations.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: FCKeditorDesigner | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./FCKeditorDesigner.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: Config | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./FileBrowser/Config.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: Connector | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./FileBrowser/Connector.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: FileWorkerBase | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./FileBrowser/FileWorkerBase.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: TypeConfig | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./FileBrowser/TypeConfig.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: TypeConfigList | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./FileBrowser/TypeConfigList.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: Uploader | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./FileBrowser/Uploader.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: XmlResponseHandler | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./FileBrowser/XmlResponseHandler.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: Util | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./Util.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: XmlUtil | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./XmlUtil.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample02 | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./_samples/aspx/1.1/sample02.aspx.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample03 | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./_samples/aspx/1.1/sample03.aspx.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample04 | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./_samples/aspx/1.1/sample04.aspx.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample02 | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./_samples/aspx/2.0/sample02.aspx.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample03 | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./_samples/aspx/2.0/sample03.aspx.cs` | TipTap OSS replacement (no 1:1 file). |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample04 | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `./_samples/aspx/2.0/sample04.aspx.cs` | TipTap OSS replacement (no 1:1 file). |
