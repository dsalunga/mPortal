# P005 - LessonReviewer

## Project Tracking Summary

| Field | Value |
|---|---|
| Project Path | `legacy/LessonReviewer/LessonReviewer/LessonReviewer.csproj` |
| Project Kind | Web Application |
| Assembly Name | `WCMS.LessonReviewer` |
| Target Framework | `v4.8` |
| Output Type | `Library` |
| Migration Status | Not Stated |
| Status Basis | No explicit migration metadata or roadmap marker found in project artifact. |
| Target Alternative | TBD |
| Tracking Owner | `TBD` |
| Target Milestone | `TBD` |

## Surface Coverage Snapshot

| Surface | Count |
|---|---:|
| Page/View | 4 |
| User Control/UI | 1 |
| Service/Handler Endpoint | 2 |
| Application Lifecycle | 1 |
| Core Component | 6 |
| Configuration/Resource | 7 |
| Frontend Asset | 7 |
| Generated UI Partial | 4 |
| Assembly Metadata | 1 |

## Pages And Views

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Page/View | `Admin` | `Login` | `Admin/Login.aspx` | Not Stated | TBD | `TBD` |
| Page/View | `Admin` | `Manage` | `Admin/Manage.aspx` | Not Stated | TBD | `TBD` |
| Page/View | `(root)` | `Default` | `Default.aspx` | Not Stated | TBD | `TBD` |
| Page/View | `offline` | `index` | `offline/index.html` | Not Stated | TBD | `TBD` |

## User Controls And UI Components

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Generated UI Partial | `Admin` | `Login.aspx.designer` | `Admin/Login.aspx.designer.cs` | Not Stated | TBD | `TBD` |
| Generated UI Partial | `Admin` | `Manage.aspx.designer` | `Admin/Manage.aspx.designer.cs` | Not Stated | TBD | `TBD` |
| Generated UI Partial | `(root)` | `Default.aspx.designer` | `Default.aspx.designer.cs` | Not Stated | TBD | `TBD` |
| User Control/UI | `(root)` | `Site` | `Site.Master` | Not Stated | TBD | `TBD` |
| Generated UI Partial | `(root)` | `Site.Master.designer` | `Site.Master.designer.cs` | Not Stated | TBD | `TBD` |

## Services, Handlers, And Controllers

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Application Lifecycle | `(root)` | `Global.asax` | `Global.asax.cs` | Not Stated | TBD | `TBD` |
| Service/Handler Endpoint | `Handlers` | `AjaxHandler` | `Handlers/AjaxHandler.ashx` | Blocked | Replace with REST/JSON API endpoint plus compatibility adapter during transition. | Legacy endpoint surface; redesign to modern API/adapter before migration. |
| Service/Handler Endpoint | `Handlers` | `Playback` | `Handlers/Playback.ashx` | Blocked | Replace with REST/JSON API endpoint plus compatibility adapter during transition. | Legacy endpoint surface; redesign to modern API/adapter before migration. |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Core Component | `Admin` | `Login.aspx` | `Admin/Login.aspx.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Admin` | `Manage.aspx` | `Admin/Manage.aspx.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `Default.aspx` | `Default.aspx.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Handlers` | `AjaxHandler.ashx` | `Handlers/AjaxHandler.ashx.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Handlers` | `Playback.ashx` | `Handlers/Playback.ashx.cs` | Not Stated | TBD | `TBD` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | `Properties/AssemblyInfo.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `Site.Master` | `Site.Master.cs` | Not Stated | TBD | `TBD` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Configuration/Resource | `Admin` | `Web` | `Admin/Web.config` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `App_Data` | `Config` | `App_Data/Config.xml` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `App_Data` | `Services` | `App_Data/Services.xml` | Not Stated | TBD | `TBD` |
| Frontend Asset | `Scripts` | `MCGI` | `Scripts/MCGI.js` | Not Stated | TBD | `TBD` |
| Frontend Asset | `Scripts` | `MakeUpServices` | `Scripts/MakeUpServices.js` | Not Stated | TBD | `TBD` |
| Frontend Asset | `Scripts` | `common` | `Scripts/common.js` | Not Stated | TBD | `TBD` |
| Frontend Asset | `Scripts` | `jquery-1.4.1-vsdoc` | `Scripts/jquery-1.4.1-vsdoc.js` | Not Stated | TBD | `TBD` |
| Frontend Asset | `Scripts` | `jquery-1.4.1` | `Scripts/jquery-1.4.1.js` | Not Stated | TBD | `TBD` |
| Frontend Asset | `Scripts` | `jquery-1.4.1.min` | `Scripts/jquery-1.4.1.min.js` | Not Stated | TBD | `TBD` |
| Frontend Asset | `Styles` | `Site` | `Styles/Site.css` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `(root)` | `Web.Debug` | `Web.Debug.config` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `(root)` | `Web.Release` | `Web.Release.config` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `(root)` | `Web` | `Web.config` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `(root)` | `packages` | `packages.config` | Not Stated | TBD | `TBD` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

