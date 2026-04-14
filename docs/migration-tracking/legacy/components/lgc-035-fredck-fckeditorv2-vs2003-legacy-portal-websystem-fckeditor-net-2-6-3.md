# LGC-035 - FredCK.FCKeditorV2.vs2003

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-035 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FredCK.FCKeditorV2.vs2003.csproj` |
| Project Directory | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3` |
| Output Type | Library |
| Target Framework | Not specified |
| Migration Status | Partial |
| Status Basis | Modern build artifacts detected, but project target is not explicit. |
| Project References | 0 |
| Surface Artifacts | 8 |
| Component/Class Artifacts | 18 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Execution (In Progress) | Close remaining legacy runtime/UI/endpoint gaps and define cutover tests. |
| WebForms Surface Present | Yes | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Pages And Views

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-035 | Partial | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample01.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample01.aspx` |  |
| LGC-035 | Partial | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample02.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample02.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample02.aspx.cs` |
| LGC-035 | Partial | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample03.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample03.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample03.aspx.cs` |
| LGC-035 | Partial | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample04.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample04.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample04.aspx.cs` |
| LGC-035 | Partial | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample01.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample01.aspx` |  |
| LGC-035 | Partial | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample02.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample02.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample02.aspx.cs` |
| LGC-035 | Partial | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample03.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample03.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample03.aspx.cs` |
| LGC-035 | Partial | Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample04.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample04.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample04.aspx.cs` |

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: FCKeditor | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FCKeditor.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: FCKeditorConfigurations | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FCKeditorConfigurations.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: FCKeditorDesigner | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FCKeditorDesigner.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: Config | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/Config.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: Connector | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/Connector.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: FileWorkerBase | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/FileWorkerBase.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: TypeConfig | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/TypeConfig.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: TypeConfigList | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/TypeConfigList.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: Uploader | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/Uploader.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: XmlResponseHandler | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/XmlResponseHandler.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: Util | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/Util.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: XmlUtil | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/XmlUtil.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample02 | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample02.aspx.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample03 | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample03.aspx.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample04 | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample04.aspx.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample02 | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample02.aspx.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample03 | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample03.aspx.cs` |
| LGC-035 | Partial | Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample04 | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample04.aspx.cs` |
