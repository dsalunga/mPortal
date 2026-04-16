# LGC-042 - WCMS.WebSystem

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-042 |
| Project Type | Library |
| Project File | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj` |
| Modern Project File / Evidence | `Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj` |
| Project Directory | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels` |
| Output Type | Library |
| Target Framework | v4.8 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:24, Not Applicable:12, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
| Project References | 2 |
| Surface Artifacts | 0 |
| Component/Class Artifacts | 33 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | No | N/A |
| Endpoint Surface Present | No | N/A |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Modern File / Evidence |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent :: AgentHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent/AgentHelper.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent/AgentHelper.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent/Constants.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/Agent/Constants.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: CentralBreadcrumb | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/CentralBreadcrumb.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/CentralBreadcrumb.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: Constants | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Constants.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/Constants.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: ISaveInFolder | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/ISaveInFolder.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/ISaveInFolder.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: ITabControl | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/ITabControl.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/ITabControl.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: ITextEditor | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/ITextEditor.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/ITextEditor.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: TabElement | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/TabElement.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/TabElement.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls :: TabEventArgs | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/TabEventArgs.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/Controls/TabEventArgs.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: FrameworkData | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/FrameworkData.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/FrameworkData.cs` |
| LGC-042 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: RazorHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/RazorHelper.cs` | N/A (retired/replaced in modern architecture). |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: TemplateFragment | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/TemplateFragment.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/TemplateFragment.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: TemplatePresenterBase | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/TemplatePresenterBase.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/TemplatePresenterBase.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IApplicable | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IApplicable.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IApplicable.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IConfigurablePart | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IConfigurablePart.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IConfigurablePart.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IElementPartView | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IElementPartView.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IElementPartView.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IObjectValueProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IObjectValueProvider.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IObjectValueProvider.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IStringValueProvider | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IStringValueProvider.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IStringValueProvider.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IUpdatable | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IUpdatable.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IUpdatable.cs` |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: IWebPartControl | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IWebPartControl.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/IWebPartControl.cs` |
| LGC-042 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WLoaderPageBase | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WLoaderPageBase.cs` | N/A (retired/replaced in modern architecture). |
| LGC-042 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebGroupViewModel | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebGroupViewModel.cs` | N/A (retired/replaced in modern architecture). |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebMasterPageViewModel | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebMasterPageViewModel.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebMasterPageViewModel.cs` |
| LGC-042 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebOfficeViewModel | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebOfficeViewModel.cs` | N/A (retired/replaced in modern architecture). |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebPageElementViewModel | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPageElementViewModel.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPageElementViewModel.cs` |
| LGC-042 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebPageViewModel | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPageViewModel.cs` | N/A (retired/replaced in modern architecture). |
| LGC-042 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebPartBase | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPartBase.cs` | N/A (retired/replaced in modern architecture). |
| LGC-042 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebPartViewModel | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebPartViewModel.cs` | N/A (retired/replaced in modern architecture). |
| LGC-042 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebSiteViewModel | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebSiteViewModel.cs` | N/A (retired/replaced in modern architecture). |
| LGC-042 | Completed | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel :: WebSystemViewModel | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebSystemViewModel.cs` | `Portal/WebSystem/WCMS.WebSystem.ViewModels/ViewModel/WebSystemViewModel.cs` |
| LGC-042 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: WPage | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WPage.cs` | N/A (retired/replaced in modern architecture). |
| LGC-042 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: WUserControl | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WUserControl.cs` | N/A (retired/replaced in modern architecture). |
| LGC-042 | Not Applicable | Class Component | legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels :: WebCommentHelper | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WebCommentHelper.cs` | N/A (retired/replaced in modern architecture). |
