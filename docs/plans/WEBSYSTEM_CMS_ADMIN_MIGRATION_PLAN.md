# WebSystem CMS Admin Migration Plan

**Created:** 2026-04-15  
**Status:** Planning  
**Scope:** 128 legacy WebForms CMS admin controls → ASP.NET Core ViewComponents/Razor Pages

## Overview

The WebSystem CMS admin layer is the internal administration UI for the mPortal content management system. It provides site management, security, template editing, tools, and web part configuration — all currently implemented as legacy WebForms `.ascx` user controls under `legacy/Portal/WebSystem/WebSystem-MVC/Content/`.

**60 of 188** WebSystem legacy files already have modern ViewComponent counterparts. **128 files** remain without modern equivalents.

## Already Migrated (60 files — existing VCs)

These WebSystem admin files already have ViewComponent counterparts in `Portal/WebSystem/WebSystem/ViewComponents/`:

| Category | Migrated Files | Modern VC Location |
|---|---|---|
| Central Top-Level | Dashboard, HeaderNavbar, HeaderPanel, SideBar, SiteMap, Breadcrumb, WebPartPreview | `ViewComponents/Admin/` |
| Agent | AgentDashboard, TaskEditor | `ViewComponents/Admin/` |
| Controls | ChangePassword, CreateUser, DesignMode, ElementDesigner, PanelDesigner | `ViewComponents/Admin/` |
| Misc | WebAddresses, WebOffices, WebParameters, WebResources, SubscriptionManager | `ViewComponents/Admin/` |
| Security | UserProfile, WebGroups, WebPermissions, WebRoles, WebSecurityTree, WebUsers | `ViewComponents/Admin/` |
| Template | WebSkins, WebTemplateEditor, WebTemplatePanels, WebTemplates, WebThemes | `ViewComponents/Admin/` |
| Tools | DataSyncDashboard, EventLogManager, PerformanceLogManager, QueryAnalyzer, ShortUrlManager, WebRegistry | `ViewComponents/Admin/` |
| WebPart | WebPartConfig, WebPartControls, WebPartControlTemplates, WebParts, WebPartTree | `ViewComponents/Admin/` |
| WebSites | WebChildSites, WebIdentities, WebMasterPages, WebPage, WebPageElements, WebPagePanels, WebPages, WebSite, WebSites, WebSitesControl | `ViewComponents/Admin/` |
| Common | Login, Comments, MessageBoard, TriggerTask, UserPhotoUpload | `ViewComponents/` |
| Themes | 11 theme VCs | `ViewComponents/Theme*.cs` |

## Remaining 128 Files — Phased Migration Plan

### Phase 1: Reusable UI Controls (11 files) — Priority: HIGH

These are shared UI building blocks used across multiple admin panels.

| # | Legacy File | Description | Effort |
|---|---|---|---|
| 1 | Controls/CKEditor.ascx | Rich text editor wrapper | M |
| 2 | Controls/ComboDatePicker.ascx | Date picker composite control | S |
| 3 | Controls/ContextActionBar.ascx | Actions toolbar | S |
| 4 | Controls/FullNamePicker.ascx | Name input composite | S |
| 5 | Controls/MonthPicker.ascx | Month selector | S |
| 6 | Controls/PhoneNumber.ascx | Phone input composite | S |
| 7 | Controls/TabControl.ascx | Tab panel container | M |
| 8 | Controls/TabControlV1.ascx | Tab panel v1 | S |
| 9 | Controls/TextEditor.ascx | Text editor wrapper | S |
| 10 | Controls/WMPControl.ascx | Windows Media Player embed | S |
| 11 | Controls/Breadcrumb.ascx | Navigation breadcrumb | S |

**Approach:** Create as Razor partial views or Tag Helpers. Replace WebForms server controls with HTML5 equivalents (e.g., `<input type="date">` for ComboDatePicker, integrate a JS rich text editor for CKEditor).

### Phase 2: Central Admin Controls (26 files) — Priority: HIGH

Tab views, form components, and configuration panels reused within admin pages.

| # | Legacy File | Description | Effort |
|---|---|---|---|
| 1 | Controls/ManagementSecurityOption.ascx | Security option toggle | S |
| 2 | Controls/ParameterSetSelector.ascx | Parameter set dropdown | S |
| 3 | Controls/SaveInFolder.ascx | Folder picker for save | S |
| 4 | Controls/TreeControls.ascx | Tree navigation buttons | S |
| 5 | Controls/UserProfileForm.ascx | User profile edit form | M |
| 6 | Controls/UserRolesForm.ascx | User-role assignment form | M |
| 7 | Controls/WebGenericTab.ascx | Generic tab content | S |
| 8 | Controls/WebGroupTab.ascx | Group detail tab | S |
| 9 | Controls/WebMasterPageTab.ascx | Master page detail tab | S |
| 10 | Controls/WebPagePanelTab.ascx | Page panel detail tab | S |
| 11 | Controls/WebPageTab.ascx | Page detail tab | S |
| 12 | Controls/WebPartControlTab.ascx | WebPart control tab | S |
| 13 | Controls/WebPartControlTemplateTab.ascx | WebPart control template tab | S |
| 14 | Controls/WebPartTab.ascx | WebPart detail tab | S |
| 15 | Controls/WebRoleTab.ascx | Role detail tab | S |
| 16 | Controls/WebSiteElementSelector.ascx | Site element picker | M |
| 17 | Controls/WebSiteTab.ascx | Site detail tab | S |
| 18 | Controls/WebThemeHome.ascx | Theme home tab | S |
| 19 | Controls/WebTemplateHome.ascx | Template home tab | S |
| 20 | Controls/WebUserTab.ascx | User detail tab | S |

**Approach:** Create as ViewComponents or Razor partial views. Tab controls become Bootstrap 5 tabs. Forms use `<form>` with ASP.NET Core model binding.

### Phase 3: Security Module (16 files) — Priority: MEDIUM

User, group, role, and permission management screens.

| # | Legacy File | Description | Effort |
|---|---|---|---|
| 1 | Security/UserActivities.ascx | User activity log | M |
| 2 | Security/WebGlobalPolicy.ascx | Global security policy editor | M |
| 3 | Security/WebGlobalPolicyHome.ascx | Policy home/list | S |
| 4 | Security/WebGroup.ascx | Group editor | M |
| 5 | Security/WebGroupHome.ascx | Group home/detail | S |
| 6 | Security/WebGroupTree.ascx | Groups tree nav | S |
| 7 | Security/WebGroupUsers.ascx | Group-user assignments | M |
| 8 | Security/WebObjectPermissions.ascx | Object-level permissions | M |
| 9 | Security/WebRoleHome.ascx | Role home/detail | S |
| 10 | Security/WebSecurity.ascx | Security dashboard | M |
| 11 | Security/WebUserGroups.ascx | User-group assignments | M |
| 12 | Security/WebUserHome.ascx | User home/detail | S |
| 13 | Security/WebUserPermissions.ascx | User permissions viewer | M |
| 14 | Security/WebUserRoles.ascx | User-role viewer | M |

**Approach:** Migrate to ViewComponents under `ViewComponents/Admin/`. CRUD operations via API controllers. Data binding with Entity Framework Core.

### Phase 4: Site & Page Management (11 files) — Priority: MEDIUM

Page, element, identity, and master page detail editors.

| # | Legacy File | Description | Effort |
|---|---|---|---|
| 1 | WebSites/WebChildPages.ascx | Child pages list | S |
| 2 | WebSites/WebIdentity.ascx | Identity editor | M |
| 3 | WebSites/WebLinkedParts.ascx | Linked parts viewer | S |
| 4 | WebSites/WebMasterPage.ascx | Master page editor | M |
| 5 | WebSites/WebMasterPageHome.ascx | Master page home | S |
| 6 | WebSites/WebPageElement.ascx | Page element editor | M |
| 7 | WebSites/WebPageElementHome.ascx | Page element home | S |
| 8 | WebSites/WebPageHome.ascx | Page home/detail | S |
| 9 | WebSites/WebPagePanel.ascx | Page panel editor | M |
| 10 | WebSites/WebPagePanelHome.ascx | Page panel home | S |
| 11 | WebSites/WebSiteHome.ascx | Site home/detail | S |

### Phase 5: WebPart Management (13 files) — Priority: MEDIUM

WebPart registration, configuration, control, and template management.

| # | Legacy File | Description | Effort |
|---|---|---|---|
| 1 | WebPart/WebPart.ascx | WebPart editor | M |
| 2 | WebPart/WebPartAdmin.ascx | WebPart admin | M |
| 3 | WebPart/WebPartAdminEntry.ascx | Admin entry form | S |
| 4 | WebPart/WebPartAdminHome.ascx | Admin home | S |
| 5 | WebPart/WebPartAdminMgmt.ascx | Admin management | M |
| 6 | WebPart/WebPartConfigEntry.ascx | Config entry form | S |
| 7 | WebPart/WebPartControl.ascx | Control editor | M |
| 8 | WebPart/WebPartControlHome.ascx | Control home | S |
| 9 | WebPart/WebPartControlTemplate.ascx | Control template editor | M |
| 10 | WebPart/WebPartControlTemplateHome.ascx | Control template home | S |
| 11 | WebPart/WebPartHome.ascx | WebPart home | S |
| 12 | WebPart/WebPartTemplatePanel.ascx | Template panel editor | M |
| 13 | WebPart/WebPartTemplatePanels.ascx | Template panels list | S |

### Phase 6: Template & Theme Management (8 files) — Priority: MEDIUM

Template, skin, and theme editors.

| # | Legacy File | Description | Effort |
|---|---|---|---|
| 1 | Template/WebSkin.ascx | Skin editor | M |
| 2 | Template/WebSkinHome.ascx | Skin home | S |
| 3 | Template/WebTemplate.ascx | Template editor | M |
| 4 | Template/WebTemplateHome.ascx | Template home | S |
| 5 | Template/WebTemplatePanel.ascx | Template panel editor | M |
| 6 | Template/WebTemplateVersions.ascx | Template versioning | M |
| 7 | Template/WebTheme.ascx | Theme editor | M |
| 8 | Template/WebThemeHome.ascx | Theme home | S |

### Phase 7: Tools & Utilities (17 files) — Priority: LOW

Data management, registry, import/export, and diagnostic tools.

| # | Legacy File | Description | Effort |
|---|---|---|---|
| 1 | Tools/DataStoreManager.ascx | Data store browser | M |
| 2 | Tools/DataSyncManager.ascx | Sync manager | M |
| 3 | Tools/ImportExportHome.ascx | Import/export home | S |
| 4 | Tools/ImportExportPage.ascx | Page import/export | M |
| 5 | Tools/ImportExportParameterSets.ascx | Parameter set import/export | M |
| 6 | Tools/MessageQueueManager.ascx | Message queue viewer | M |
| 7 | Tools/ShortUrlEdit.ascx | Short URL editor | S |
| 8 | Tools/SmtpAnalyzer.ascx | SMTP test tool | M |
| 9 | Tools/UserSessionManager.ascx | Active sessions viewer | M |
| 10 | Tools/WebDataExplorer.ascx | Data explorer | M |
| 11 | Tools/WebDataRows.ascx | Data row viewer | S |
| 12 | Tools/WebFolder.ascx | Folder manager | M |
| 13 | Tools/WebFolderTree.ascx | Folder tree nav | S |
| 14 | Tools/WebObjectEdit.ascx | Object editor | M |
| 15 | Tools/WebRegistryEntry.ascx | Registry entry editor | S |
| 16 | Tools/WebRegistryTree.ascx | Registry tree nav | S |
| 17 | Tools/WebToolsTree.ascx | Tools tree nav | S |

### Phase 8: Misc & Office Management (14 files) — Priority: LOW

Office, address, parameter set, and resource management.

| # | Legacy File | Description | Effort |
|---|---|---|---|
| 1 | Manager/Home.ascx | Manager home page | S |
| 2 | Misc/WebAddress.ascx | Address editor | S |
| 3 | Misc/WebOffice.ascx | Office editor | M |
| 4 | Misc/WebOfficeHome.ascx | Office home | S |
| 5 | Misc/WebOfficeTree.ascx | Office tree nav | S |
| 6 | Misc/WebParameter.ascx | Parameter editor | S |
| 7 | Misc/WebParameterSet.ascx | Parameter set editor | M |
| 8 | Misc/WebParameterSetHome.ascx | Parameter set home | S |
| 9 | Misc/WebParameterSets.ascx | Parameter sets list | S |
| 10 | Misc/WebParametersXml.ascx | XML parameter editor | M |
| 11 | Misc/WebResource.ascx | Resource editor | S |
| 12 | Misc/WebResourceManager.ascx | Resource manager | M |
| 13 | TreePanel.ascx | Main tree panel | M |
| 14 | WebOpen.ascx | Open item dialog | S |

### Phase 9: Agent, Common & Test (12 files) — Priority: LOW

Agent tasks, common components, and test controls.

| # | Legacy File | Description | Effort |
|---|---|---|---|
| 1 | Agent/Dashboard.ascx | Agent dashboard | M |
| 2 | Agent/TaskManager.ascx | Task list manager | M |
| 3 | Agent/TaskView.ascx | Task detail viewer | S |
| 4 | Common/AdminCommentManager.ascx | Comment moderation | M |
| 5 | Common/LoginV2.ascx | Login form v2 | S |
| 6 | Test/CategoryPart.ascx | Test category | S |
| 7 | Test/DetailsPart.ascx | Test details | S |
| 8 | Test/ListPart.ascx | Test list | S |
| 9 | Test/TemplateFormEditor.ascx | Test template editor | S |
| 10 | Test/WebUserControl1.ascx | Test control | S |

### Phase 10: Plugin & Theme Config (1 file) — Priority: LOWEST

| # | Legacy File | Description | Effort |
|---|---|---|---|
| 1 | Plugins/fckeditor/.../config.ascx | FCKEditor config | S — **Likely obsolete** (replaced by CKEditor/TinyMCE) |

## Effort Summary

| Effort | S (Small) | M (Medium) | Total |
|---|---|---|---|
| Phase 1: UI Controls | 9 | 2 | **11** |
| Phase 2: Admin Controls | 15 | 5 | **20** |
| Phase 3: Security | 5 | 9 | **14** |
| Phase 4: Sites & Pages | 5 | 6 | **11** |
| Phase 5: WebPart Mgmt | 5 | 8 | **13** |
| Phase 6: Templates | 2 | 6 | **8** |
| Phase 7: Tools | 6 | 11 | **17** |
| Phase 8: Misc & Office | 8 | 6 | **14** |
| Phase 9: Agent/Common/Test | 6 | 4 | **10** |
| Phase 10: Plugin | 1 | 0 | **1** |
| **Totals** | **62** | **57** | **128** |

## Implementation Checklist

For each file:
1. [ ] Read legacy .ascx + .ascx.cs code-behind
2. [ ] Create ViewComponent .cs with ViewModel
3. [ ] Create Default.cshtml Razor view with Bootstrap 5
4. [ ] Wire data access via existing framework services
5. [ ] Build verify (0 errors)
6. [ ] Update CSV inventory with modern file paths
7. [ ] Commit

## Dependencies & Risks

- **Data access:** Admin panels heavily use `SqlHelper`, `WebRegistry`, and direct SQL. These need to use the already-migrated `IDataManager` and `DbManager`.
- **Tree controls:** Several panels use `TreeView` server control. Need a JS tree component (e.g., jsTree or custom Bootstrap accordion).
- **Tab controls:** WebForms `MultiView`/`View` → Bootstrap 5 tabs/pills.
- **Postback model:** WebForms postback model → MVC form posts / AJAX API calls.
- **Server-side events:** `OnClick`, `OnSelectedIndexChanged` → API endpoints.

## Notes

- Phase 1 (UI Controls) and Phase 2 (Admin Controls) should be done first as they're dependencies for later phases.
- The Test controls (Phase 9) may be candidates for removal if no longer needed.
- The FCKEditor config (Phase 10) is almost certainly obsolete.
- Consider migrating some phases to Razor Pages instead of ViewComponents if the admin pages benefit from a page-routing model.
