# P002 - BibleReader.WebApp

## Project Tracking Summary

| Field | Value |
|---|---|
| Project Path | `legacy/BibleReader/BibleReader/BibleReader.WebApp.csproj` |
| Project Kind | Web Application |
| Assembly Name | `BibleReader.WebApp` |
| Target Framework | `v4.7` |
| Output Type | `Library` |
| Migration Status | Not Stated |
| Status Basis | No explicit migration metadata or roadmap marker found in project artifact. |
| Target Alternative | TBD |
| Tracking Owner | `TBD` |
| Target Milestone | `TBD` |

## Surface Coverage Snapshot

| Surface | Count |
|---|---:|
| Page/View | 7 |
| User Control/UI | 1 |
| Service/Handler Endpoint | 1 |
| Application Lifecycle | 1 |
| Core Component | 9 |
| Database Script | 8 |
| Configuration/Resource | 7 |
| Frontend Asset | 5 |
| Generated UI Partial | 8 |
| Assembly Metadata | 1 |

## Pages And Views

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Page/View | `(root)` | `About` | `About.aspx` | Not Stated | TBD | `TBD` |
| Page/View | `Account` | `ChangePassword` | `Account/ChangePassword.aspx` | Not Stated | TBD | `TBD` |
| Page/View | `Account` | `ChangePasswordSuccess` | `Account/ChangePasswordSuccess.aspx` | Not Stated | TBD | `TBD` |
| Page/View | `Account` | `Login` | `Account/Login.aspx` | Not Stated | TBD | `TBD` |
| Page/View | `Account` | `Register` | `Account/Register.aspx` | Not Stated | TBD | `TBD` |
| Page/View | `(root)` | `Default` | `Default.aspx` | Not Stated | TBD | `TBD` |
| Page/View | `(root)` | `Setup` | `Setup.aspx` | Not Stated | TBD | `TBD` |

## User Controls And UI Components

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Generated UI Partial | `(root)` | `About.aspx.designer` | `About.aspx.designer.cs` | Not Stated | TBD | `TBD` |
| Generated UI Partial | `Account` | `ChangePassword.aspx.designer` | `Account/ChangePassword.aspx.designer.cs` | Not Stated | TBD | `TBD` |
| Generated UI Partial | `Account` | `ChangePasswordSuccess.aspx.designer` | `Account/ChangePasswordSuccess.aspx.designer.cs` | Not Stated | TBD | `TBD` |
| Generated UI Partial | `Account` | `Login.aspx.designer` | `Account/Login.aspx.designer.cs` | Not Stated | TBD | `TBD` |
| Generated UI Partial | `Account` | `Register.aspx.designer` | `Account/Register.aspx.designer.cs` | Not Stated | TBD | `TBD` |
| Generated UI Partial | `(root)` | `Default.aspx.designer` | `Default.aspx.designer.cs` | Not Stated | TBD | `TBD` |
| Generated UI Partial | `(root)` | `Setup.aspx.designer` | `Setup.aspx.designer.cs` | Not Stated | TBD | `TBD` |
| User Control/UI | `(root)` | `Site` | `Site.Master` | Not Stated | TBD | `TBD` |
| Generated UI Partial | `(root)` | `Site.Master.designer` | `Site.Master.designer.cs` | Not Stated | TBD | `TBD` |

## Services, Handlers, And Controllers

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Service/Handler Endpoint | `(root)` | `BibleService` | `BibleService.asmx` | Blocked | Replace with REST/JSON API endpoint plus compatibility adapter during transition. | Legacy endpoint surface; redesign to modern API/adapter before migration. |
| Application Lifecycle | `(root)` | `Global.asax` | `Global.asax.cs` | Not Stated | TBD | `TBD` |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Core Component | `(root)` | `About.aspx` | `About.aspx.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Account` | `ChangePassword.aspx` | `Account/ChangePassword.aspx.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Account` | `ChangePasswordSuccess.aspx` | `Account/ChangePasswordSuccess.aspx.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Account` | `Login.aspx` | `Account/Login.aspx.cs` | Not Stated | TBD | `TBD` |
| Core Component | `Account` | `Register.aspx` | `Account/Register.aspx.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `BibleService.asmx` | `BibleService.asmx.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `Default.aspx` | `Default.aspx.cs` | Not Stated | TBD | `TBD` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | `Properties/AssemblyInfo.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `Setup.aspx` | `Setup.aspx.cs` | Not Stated | TBD | `TBD` |
| Core Component | `(root)` | `Site.Master` | `Site.Master.cs` | Not Stated | TBD | `TBD` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Configuration/Resource | `Account` | `Web` | `Account/Web.config` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `Admin/Data/Database` | `Book` | `Admin/Data/Database/Book.xml` | Not Stated | TBD | `TBD` |
| Database Script | `Admin/Data/Database/Procedures` | `Book_Get.create` | `Admin/Data/Database/Procedures/Book_Get.create.sql` | Not Stated | TBD | `TBD` |
| Database Script | `Admin/Data/Database/Procedures` | `Book_Get.drop` | `Admin/Data/Database/Procedures/Book_Get.drop.sql` | Not Stated | TBD | `TBD` |
| Database Script | `Admin/Data/Database/Tables` | `Book.create` | `Admin/Data/Database/Tables/Book.create.sql` | Not Stated | TBD | `TBD` |
| Database Script | `Admin/Data/Database/Tables` | `Book.drop` | `Admin/Data/Database/Tables/Book.drop.sql` | Not Stated | TBD | `TBD` |
| Database Script | `Admin/Data/Database/Tables` | `Translation.create` | `Admin/Data/Database/Tables/Translation.create.sql` | Not Stated | TBD | `TBD` |
| Database Script | `Admin/Data/Database/Tables` | `Translation.drop` | `Admin/Data/Database/Tables/Translation.drop.sql` | Not Stated | TBD | `TBD` |
| Database Script | `Admin/Data/Database/Tables` | `Verse.create` | `Admin/Data/Database/Tables/Verse.create.sql` | Not Stated | TBD | `TBD` |
| Database Script | `Admin/Data/Database/Tables` | `Verse.drop` | `Admin/Data/Database/Tables/Verse.drop.sql` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `Admin/Data/Database` | `Translation` | `Admin/Data/Database/Translation.xml` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `Admin/Data/Database` | `Verse` | `Admin/Data/Database/Verse.xml` | Not Stated | TBD | `TBD` |
| Frontend Asset | `Scripts` | `jquery-1.4.1-vsdoc` | `Scripts/jquery-1.4.1-vsdoc.js` | Not Stated | TBD | `TBD` |
| Frontend Asset | `Scripts` | `jquery-1.4.1` | `Scripts/jquery-1.4.1.js` | Not Stated | TBD | `TBD` |
| Frontend Asset | `Scripts` | `jquery-1.4.1.min` | `Scripts/jquery-1.4.1.min.js` | Not Stated | TBD | `TBD` |
| Frontend Asset | `Styles` | `BibleReader` | `Styles/BibleReader.css` | Not Stated | TBD | `TBD` |
| Frontend Asset | `Styles` | `Site` | `Styles/Site.css` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `(root)` | `Web.Debug` | `Web.Debug.config` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `(root)` | `Web.Release` | `Web.Release.config` | Not Stated | TBD | `TBD` |
| Configuration/Resource | `(root)` | `Web` | `Web.config` | Not Stated | TBD | `TBD` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

