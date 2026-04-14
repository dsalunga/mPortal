# LGC-034 - FredCK.FCKeditorV2

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-034 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.csproj` |
| Project Directory | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Do Not Migrate As-Is |
| Status Basis | FCKeditorV2 is a non-migration target; all editor usage must be replaced with TipTap OSS + server-side HTML sanitization. |
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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample01.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample01.aspx` |  |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample02.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample02.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample02.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample03.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample03.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample03.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample04.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample04.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample04.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample01.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample01.aspx` |  |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample02.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample02.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample02.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample03.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample03.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample03.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample04.aspx | Do not migrate legacy editor pages/controls; replace with TipTap OSS integration. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample04.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample04.aspx.cs` |

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: FCKeditor | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FCKeditor.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: FCKeditorConfigurations | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FCKeditorConfigurations.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: FCKeditorDesigner | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FCKeditorDesigner.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: Config | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/Config.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: Connector | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/Connector.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: FileWorkerBase | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/FileWorkerBase.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: TypeConfig | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/TypeConfig.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: TypeConfigList | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/TypeConfigList.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: Uploader | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/Uploader.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: XmlResponseHandler | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/XmlResponseHandler.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: Util | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/Util.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: XmlUtil | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/XmlUtil.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample02 | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample02.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample03 | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample03.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample04 | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample04.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample02 | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample02.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample03 | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample03.aspx.cs` |
| LGC-034 | Do Not Migrate As-Is | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample04 | Do not port FCKeditor internals; replace editor behavior with TipTap OSS wrappers and sanitization pipeline. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample04.aspx.cs` |
