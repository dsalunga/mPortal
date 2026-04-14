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

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Page/View | `Admin` | `Login` | Not Stated | TBD | `TBD` | `Admin/Login.aspx` |
| Page/View | `Admin` | `Manage` | Not Stated | TBD | `TBD` | `Admin/Manage.aspx` |
| Page/View | `(root)` | `Default` | Not Stated | TBD | `TBD` | `Default.aspx` |
| Page/View | `offline` | `index` | Not Stated | TBD | `TBD` | `offline/index.html` |

## User Controls And UI Components

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Generated UI Partial | `Admin` | `Login.aspx.designer` | Not Stated | TBD | `TBD` | `Admin/Login.aspx.designer.cs` |
| Generated UI Partial | `Admin` | `Manage.aspx.designer` | Not Stated | TBD | `TBD` | `Admin/Manage.aspx.designer.cs` |
| Generated UI Partial | `(root)` | `Default.aspx.designer` | Not Stated | TBD | `TBD` | `Default.aspx.designer.cs` |
| User Control/UI | `(root)` | `Site` | Not Stated | TBD | `TBD` | `Site.Master` |
| Generated UI Partial | `(root)` | `Site.Master.designer` | Not Stated | TBD | `TBD` | `Site.Master.designer.cs` |

## Services, Handlers, And Controllers

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Application Lifecycle | `(root)` | `Global.asax` | Not Stated | TBD | `TBD` | `Global.asax.cs` |
| Service/Handler Endpoint | `Handlers` | `AjaxHandler` | Blocked | Replace with REST/JSON API endpoint plus compatibility adapter during transition. | Legacy endpoint surface; redesign to modern API/adapter before migration. | `Handlers/AjaxHandler.ashx` |
| Service/Handler Endpoint | `Handlers` | `Playback` | Blocked | Replace with REST/JSON API endpoint plus compatibility adapter during transition. | Legacy endpoint surface; redesign to modern API/adapter before migration. | `Handlers/Playback.ashx` |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Core Component | `Admin` | `Login.aspx` | Not Stated | TBD | `TBD` | `Admin/Login.aspx.cs` |
| Core Component | `Admin` | `Manage.aspx` | Not Stated | TBD | `TBD` | `Admin/Manage.aspx.cs` |
| Core Component | `(root)` | `Default.aspx` | Not Stated | TBD | `TBD` | `Default.aspx.cs` |
| Core Component | `Handlers` | `AjaxHandler.ashx` | Not Stated | TBD | `TBD` | `Handlers/AjaxHandler.ashx.cs` |
| Core Component | `Handlers` | `Playback.ashx` | Not Stated | TBD | `TBD` | `Handlers/Playback.ashx.cs` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | Not Stated | TBD | `TBD` | `Properties/AssemblyInfo.cs` |
| Core Component | `(root)` | `Site.Master` | Not Stated | TBD | `TBD` | `Site.Master.cs` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Configuration/Resource | `Admin` | `Web` | Not Stated | TBD | `TBD` | `Admin/Web.config` |
| Configuration/Resource | `App_Data` | `Config` | Not Stated | TBD | `TBD` | `App_Data/Config.xml` |
| Configuration/Resource | `App_Data` | `Services` | Not Stated | TBD | `TBD` | `App_Data/Services.xml` |
| Frontend Asset | `Scripts` | `MCGI` | Not Stated | TBD | `TBD` | `Scripts/MCGI.js` |
| Frontend Asset | `Scripts` | `MakeUpServices` | Not Stated | TBD | `TBD` | `Scripts/MakeUpServices.js` |
| Frontend Asset | `Scripts` | `common` | Not Stated | TBD | `TBD` | `Scripts/common.js` |
| Frontend Asset | `Scripts` | `jquery-1.4.1-vsdoc` | Not Stated | TBD | `TBD` | `Scripts/jquery-1.4.1-vsdoc.js` |
| Frontend Asset | `Scripts` | `jquery-1.4.1` | Not Stated | TBD | `TBD` | `Scripts/jquery-1.4.1.js` |
| Frontend Asset | `Scripts` | `jquery-1.4.1.min` | Not Stated | TBD | `TBD` | `Scripts/jquery-1.4.1.min.js` |
| Frontend Asset | `Styles` | `Site` | Not Stated | TBD | `TBD` | `Styles/Site.css` |
| Configuration/Resource | `(root)` | `Web.Debug` | Not Stated | TBD | `TBD` | `Web.Debug.config` |
| Configuration/Resource | `(root)` | `Web.Release` | Not Stated | TBD | `TBD` | `Web.Release.config` |
| Configuration/Resource | `(root)` | `Web` | Not Stated | TBD | `TBD` | `Web.config` |
| Configuration/Resource | `(root)` | `packages` | Not Stated | TBD | `TBD` | `packages.config` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

