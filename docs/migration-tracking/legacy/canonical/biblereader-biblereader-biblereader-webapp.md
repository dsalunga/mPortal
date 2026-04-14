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

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Page/View | `(root)` | `About` | Not Stated | TBD | `TBD` | `About.aspx` |
| Page/View | `Account` | `ChangePassword` | Not Stated | TBD | `TBD` | `Account/ChangePassword.aspx` |
| Page/View | `Account` | `ChangePasswordSuccess` | Not Stated | TBD | `TBD` | `Account/ChangePasswordSuccess.aspx` |
| Page/View | `Account` | `Login` | Not Stated | TBD | `TBD` | `Account/Login.aspx` |
| Page/View | `Account` | `Register` | Not Stated | TBD | `TBD` | `Account/Register.aspx` |
| Page/View | `(root)` | `Default` | Not Stated | TBD | `TBD` | `Default.aspx` |
| Page/View | `(root)` | `Setup` | Not Stated | TBD | `TBD` | `Setup.aspx` |

## User Controls And UI Components

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Generated UI Partial | `(root)` | `About.aspx.designer` | Not Stated | TBD | `TBD` | `About.aspx.designer.cs` |
| Generated UI Partial | `Account` | `ChangePassword.aspx.designer` | Not Stated | TBD | `TBD` | `Account/ChangePassword.aspx.designer.cs` |
| Generated UI Partial | `Account` | `ChangePasswordSuccess.aspx.designer` | Not Stated | TBD | `TBD` | `Account/ChangePasswordSuccess.aspx.designer.cs` |
| Generated UI Partial | `Account` | `Login.aspx.designer` | Not Stated | TBD | `TBD` | `Account/Login.aspx.designer.cs` |
| Generated UI Partial | `Account` | `Register.aspx.designer` | Not Stated | TBD | `TBD` | `Account/Register.aspx.designer.cs` |
| Generated UI Partial | `(root)` | `Default.aspx.designer` | Not Stated | TBD | `TBD` | `Default.aspx.designer.cs` |
| Generated UI Partial | `(root)` | `Setup.aspx.designer` | Not Stated | TBD | `TBD` | `Setup.aspx.designer.cs` |
| User Control/UI | `(root)` | `Site` | Not Stated | TBD | `TBD` | `Site.Master` |
| Generated UI Partial | `(root)` | `Site.Master.designer` | Not Stated | TBD | `TBD` | `Site.Master.designer.cs` |

## Services, Handlers, And Controllers

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Service/Handler Endpoint | `(root)` | `BibleService` | Blocked | Replace with REST/JSON API endpoint plus compatibility adapter during transition. | Legacy endpoint surface; redesign to modern API/adapter before migration. | `BibleService.asmx` |
| Application Lifecycle | `(root)` | `Global.asax` | Not Stated | TBD | `TBD` | `Global.asax.cs` |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Core Component | `(root)` | `About.aspx` | Not Stated | TBD | `TBD` | `About.aspx.cs` |
| Core Component | `Account` | `ChangePassword.aspx` | Not Stated | TBD | `TBD` | `Account/ChangePassword.aspx.cs` |
| Core Component | `Account` | `ChangePasswordSuccess.aspx` | Not Stated | TBD | `TBD` | `Account/ChangePasswordSuccess.aspx.cs` |
| Core Component | `Account` | `Login.aspx` | Not Stated | TBD | `TBD` | `Account/Login.aspx.cs` |
| Core Component | `Account` | `Register.aspx` | Not Stated | TBD | `TBD` | `Account/Register.aspx.cs` |
| Core Component | `(root)` | `BibleService.asmx` | Not Stated | TBD | `TBD` | `BibleService.asmx.cs` |
| Core Component | `(root)` | `Default.aspx` | Not Stated | TBD | `TBD` | `Default.aspx.cs` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | Not Stated | TBD | `TBD` | `Properties/AssemblyInfo.cs` |
| Core Component | `(root)` | `Setup.aspx` | Not Stated | TBD | `TBD` | `Setup.aspx.cs` |
| Core Component | `(root)` | `Site.Master` | Not Stated | TBD | `TBD` | `Site.Master.cs` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Configuration/Resource | `Account` | `Web` | Not Stated | TBD | `TBD` | `Account/Web.config` |
| Configuration/Resource | `Admin/Data/Database` | `Book` | Not Stated | TBD | `TBD` | `Admin/Data/Database/Book.xml` |
| Database Script | `Admin/Data/Database/Procedures` | `Book_Get.create` | Not Stated | TBD | `TBD` | `Admin/Data/Database/Procedures/Book_Get.create.sql` |
| Database Script | `Admin/Data/Database/Procedures` | `Book_Get.drop` | Not Stated | TBD | `TBD` | `Admin/Data/Database/Procedures/Book_Get.drop.sql` |
| Database Script | `Admin/Data/Database/Tables` | `Book.create` | Not Stated | TBD | `TBD` | `Admin/Data/Database/Tables/Book.create.sql` |
| Database Script | `Admin/Data/Database/Tables` | `Book.drop` | Not Stated | TBD | `TBD` | `Admin/Data/Database/Tables/Book.drop.sql` |
| Database Script | `Admin/Data/Database/Tables` | `Translation.create` | Not Stated | TBD | `TBD` | `Admin/Data/Database/Tables/Translation.create.sql` |
| Database Script | `Admin/Data/Database/Tables` | `Translation.drop` | Not Stated | TBD | `TBD` | `Admin/Data/Database/Tables/Translation.drop.sql` |
| Database Script | `Admin/Data/Database/Tables` | `Verse.create` | Not Stated | TBD | `TBD` | `Admin/Data/Database/Tables/Verse.create.sql` |
| Database Script | `Admin/Data/Database/Tables` | `Verse.drop` | Not Stated | TBD | `TBD` | `Admin/Data/Database/Tables/Verse.drop.sql` |
| Configuration/Resource | `Admin/Data/Database` | `Translation` | Not Stated | TBD | `TBD` | `Admin/Data/Database/Translation.xml` |
| Configuration/Resource | `Admin/Data/Database` | `Verse` | Not Stated | TBD | `TBD` | `Admin/Data/Database/Verse.xml` |
| Frontend Asset | `Scripts` | `jquery-1.4.1-vsdoc` | Not Stated | TBD | `TBD` | `Scripts/jquery-1.4.1-vsdoc.js` |
| Frontend Asset | `Scripts` | `jquery-1.4.1` | Not Stated | TBD | `TBD` | `Scripts/jquery-1.4.1.js` |
| Frontend Asset | `Scripts` | `jquery-1.4.1.min` | Not Stated | TBD | `TBD` | `Scripts/jquery-1.4.1.min.js` |
| Frontend Asset | `Styles` | `BibleReader` | Not Stated | TBD | `TBD` | `Styles/BibleReader.css` |
| Frontend Asset | `Styles` | `Site` | Not Stated | TBD | `TBD` | `Styles/Site.css` |
| Configuration/Resource | `(root)` | `Web.Debug` | Not Stated | TBD | `TBD` | `Web.Debug.config` |
| Configuration/Resource | `(root)` | `Web.Release` | Not Stated | TBD | `TBD` | `Web.Release.config` |
| Configuration/Resource | `(root)` | `Web` | Not Stated | TBD | `TBD` | `Web.config` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

