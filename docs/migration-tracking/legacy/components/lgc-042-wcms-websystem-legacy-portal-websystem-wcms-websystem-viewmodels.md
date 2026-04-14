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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent :: AgentHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent/AgentHelper.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent :: Constants | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent/Constants.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: CentralBreadcrumb | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/CentralBreadcrumb.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: Constants | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Constants.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: ISaveInFolder | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/ISaveInFolder.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: ITabControl | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/ITabControl.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: ITextEditor | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/ITextEditor.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: TabElement | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/TabElement.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: TabEventArgs | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/TabEventArgs.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: FrameworkData | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/FrameworkData.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: RazorHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/RazorHelper.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: TemplateFragment | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/TemplateFragment.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: TemplatePresenterBase | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/TemplatePresenterBase.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IApplicable | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IApplicable.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IConfigurablePart | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IConfigurablePart.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IElementPartView | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IElementPartView.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IObjectValueProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IObjectValueProvider.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IStringValueProvider | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IStringValueProvider.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IUpdatable | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IUpdatable.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IWebPartControl | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IWebPartControl.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WLoaderPageBase | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WLoaderPageBase.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebGroupViewModel | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebGroupViewModel.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebMasterPageViewModel | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebMasterPageViewModel.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebOfficeViewModel | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebOfficeViewModel.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebPageElementViewModel | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPageElementViewModel.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebPageViewModel | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPageViewModel.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebPartBase | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPartBase.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebPartViewModel | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPartViewModel.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebSiteViewModel | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebSiteViewModel.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebSystemViewModel | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebSystemViewModel.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: WPage | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WPage.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: WUserControl | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WUserControl.cs` |
| LGC-042 | Partial | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: WebCommentHelper | Library/business component; assess API compatibility and dependencies. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WebCommentHelper.cs` |
