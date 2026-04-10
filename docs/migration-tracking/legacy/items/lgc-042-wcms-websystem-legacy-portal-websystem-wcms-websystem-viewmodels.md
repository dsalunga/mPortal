# LGC-042 - WCMS.WebSystem

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-042 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj` |
| Project Directory | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Partial |
| Status Basis | Legacy target (v4.8) with modern build artifacts in obj/bin. |
| Project References | 2 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 33 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Execution (In Progress) | Close remaining legacy runtime/UI/endpoint gaps and define cutover tests. |
| WebForms Surface Present | No | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | No | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Project References

| --- | --- | --- |
| ../WCMS.Common/WCMS.Common.csproj |
| ../WCMS.Framework/WCMS.Framework.csproj |

## Pages And Views

_No artifacts found._

## User Controls

_No artifacts found._

## Services And Handlers

_No artifacts found._

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Source File | Migration Note |
| --- | --- | --- | --- | --- | --- |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent :: AgentHelper | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent/AgentHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent :: Constants | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent/Constants.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: CentralBreadcrumb | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/CentralBreadcrumb.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: Constants | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Constants.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: ISaveInFolder | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/ISaveInFolder.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: ITabControl | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/ITabControl.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: ITextEditor | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/ITextEditor.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: TabElement | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/TabElement.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: TabEventArgs | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/TabEventArgs.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: FrameworkData | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/FrameworkData.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: RazorHelper | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/RazorHelper.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: TemplateFragment | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/TemplateFragment.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: TemplatePresenterBase | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/TemplatePresenterBase.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IApplicable | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IApplicable.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IConfigurablePart | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IConfigurablePart.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IElementPartView | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IElementPartView.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IObjectValueProvider | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IObjectValueProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IStringValueProvider | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IStringValueProvider.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IUpdatable | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IUpdatable.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IWebPartControl | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IWebPartControl.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WLoaderPageBase | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WLoaderPageBase.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebGroupViewModel | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebGroupViewModel.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebMasterPageViewModel | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebMasterPageViewModel.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebOfficeViewModel | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebOfficeViewModel.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebPageElementViewModel | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPageElementViewModel.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebPageViewModel | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPageViewModel.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebPartBase | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPartBase.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebPartViewModel | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPartViewModel.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebSiteViewModel | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebSiteViewModel.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebSystemViewModel | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebSystemViewModel.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: WPage | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WPage.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: WUserControl | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WUserControl.cs` | Library/business component; assess API compatibility and dependencies. |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: WebCommentHelper | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WebCommentHelper.cs` | Library/business component; assess API compatibility and dependencies. |
