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

| Artifact Type | Feature / Functionality (Inferred) | Source File | Code-Behind / Pair | Migration Note |
| --- | --- | --- | --- | --- |
| Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample01.aspx | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample01.aspx` |  | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample02.aspx | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample02.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample02.aspx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample03.aspx | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample03.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample03.aspx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample04.aspx | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample04.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample04.aspx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample01.aspx | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample01.aspx` |  | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample02.aspx | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample02.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample02.aspx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample03.aspx | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample03.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample03.aspx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |
| Page | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample04.aspx | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample04.aspx` | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample04.aspx.cs` | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). |

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: FCKeditor | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FCKeditor.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: FCKeditorConfigurations | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FCKeditorConfigurations.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: FCKeditorDesigner | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FCKeditorDesigner.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: Config | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/Config.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: Connector | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/Connector.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: FileWorkerBase | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/FileWorkerBase.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: TypeConfig | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/TypeConfig.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: TypeConfigList | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/TypeConfigList.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: Uploader | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/Uploader.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/FileBrowser :: XmlResponseHandler | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/FileBrowser/XmlResponseHandler.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: Util | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/Util.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3 :: XmlUtil | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/XmlUtil.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample02 | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample02.aspx.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample03 | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample03.aspx.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/1.1 :: sample04 | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/1.1/sample04.aspx.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample02 | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample02.aspx.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample03 | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample03.aspx.cs` | Library/business component; assess API compatibility and dependencies. |
| Class Component | legacy/Portal/WebSystem/FCKeditor.Net 2.6.3/ samples/aspx/2.0 :: sample04 | `legacy/Portal/WebSystem/FCKeditor.Net_2.6.3/_samples/aspx/2.0/sample04.aspx.cs` | Library/business component; assess API compatibility and dependencies. |
