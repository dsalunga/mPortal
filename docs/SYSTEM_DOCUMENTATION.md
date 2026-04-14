# mPortal — System Documentation

> **mPortal** is a custom-built, database-driven **Web Content Management System (WCMS)** originally
> built on ASP.NET WebForms / MVC 5 and .NET Framework 4.8.  This document describes the system
> architecture, every project in the repository, and how each legacy feature maps to a
> .NET 10 / ASP.NET Core cross-platform equivalent.

Date: 2026-02-23

---

## Table of Contents

1. [System Overview](#1-system-overview)
2. [Repository Layout](#2-repository-layout)
3. [Core Framework (WCMS.Framework)](#3-core-framework)
4. [Shared Library (WCMS.Common)](#4-shared-library)
5. [View-Model Layer (WCMS.WebSystem.ViewModels)](#5-view-model-layer)
6. [Data Provider Layer](#6-data-provider-layer)
7. [Web Part / Module System](#7-web-part--module-system)
8. [Dynamic Page Rendering Pipeline](#8-dynamic-page-rendering-pipeline)
9. [Site, Page & Template Model](#9-site-page--template-model)
10. [Security & Authentication](#10-security--authentication)
11. [Registry & Configuration](#11-registry--configuration)
12. [Background Agent System](#12-background-agent-system)
13. [Satellite Applications](#13-satellite-applications)
14. [Utility Projects](#14-utility-projects)
15. [Legacy Web Assets Inventory](#15-legacy-web-assets-inventory)
16. [Feature-by-Feature Migration Guide (.NET 10)](#16-feature-by-feature-migration-guide)
17. [Migration Plan Validation Notes](#17-migration-plan-validation-notes)

---

## 1) System Overview

mPortal is a **fully dynamic CMS** where everything — sites, pages, master pages, themes,
elements (web parts), security policies, and configuration — is stored in a SQL Server
database and assembled at runtime.

### Key capabilities

| Capability | Description |
|---|---|
| **Multi-site hosting** | Multiple websites on different domains, with parent/child site hierarchies |
| **Dynamic pages** | Pages resolved from database by URL; no static route files |
| **Master pages & themes** | Reusable layouts with template panels (content zones) |
| **Web parts (controls)** | Pluggable UI components loaded per-element on each page |
| **Template engines** | Supports both ASPX (WebForms) and Razor rendering |
| **Security model** | Per-object permissions, user/group/role-based access control |
| **Registry system** | Hierarchical key/value configuration (like Windows Registry) |
| **Domain management** | Host-name-to-site mapping with WebAddress/WebBinding |
| **Background agents** | Scheduled job system for email, indexing, maintenance |
| **File management** | Versioned file/folder manager with upload handling |
| **Content modules** | Article, Contact, Calendar, Forum, Social wall, Incident tracker, Jobs board, etc. |

### Technology stack (legacy)

- ASP.NET WebForms + MVC 5 on .NET Framework 4.8
- Entity Framework 6 (Database First / EDMX)
- WCF services (`.svc` endpoints)
- SQL Server with stored procedures
- IIS / IIS Express hosting
- OWIN middleware for auth
- FCKeditor for rich-text editing

---

## 2) Repository Layout

```
mPortal-private/
├── Core/
│   └── WCMS.Common/                  # Shared utility library (standalone copy, net10.0)
├── Portal/
│   ├── WebSystem/                     # ──── Core CMS engine ────
│   │   ├── WCMS.Common/              # Shared utilities (main copy)
│   │   ├── WCMS.Framework/           # CMS domain model & data access
│   │   ├── WCMS.Framework.Core.SqlProvider/     # SQL data providers
│   │   ├── WCMS.Framework.Core.XmlProvider/     # XML data providers
│   │   ├── WCMS.Framework.Core.SqlProvider.Smo/ # SQL SMO script gen
│   │   ├── WCMS.WebSystem.Utilities/ # DB manager, email updater
│   │   ├── WCMS.WebSystem.ViewModels/# ViewModel bridge (framework ↔ UI)
│   │   ├── WCMS.Framewok.Agent/      # Background agent console
│   │   ├── WCMS.Framework.AgentService/ # Background agent Windows service
│   │   ├── FCKeditor.Net_2.6.3/      # Rich-text editor integration
│   │   ├── WebSystem/            # ★ Main web application host
│   │   ├── mPortal.sln               # Full solution
│   │   └── mPortal.slnx              # Consolidated modern solution file
│   ├── WebParts/                      # ──── Pluggable modules ────
│   │   ├── SystemParts/              # G1: FileManager, Calendar, Article, Contact, etc.
│   │   ├── SystemPartsG2/            # G2: Forum, Social, Ads, Newsletter
│   │   ├── SystemPartsG3/            # G3: Incident tracker, Jobs board
│   │   ├── Integration/              # External-system integration + BibleReader adapter
│   │   ├── BranchLocator/            # Branch/location finder with maps
│   │   └── SDKTest/                  # SDK smoke-test harness
│   └── Utilities/                     # ──── CLI/desktop tools ────
│       ├── DbManager/                # CLI database backup/restore
│       ├── DbManagerWPF/             # WPF database manager (net10.0-windows)
│       ├── PostBuildManager/          # Post-build automation
│       ├── WebExtractor/              # Web content extraction
│       ├── WebSystemDeployer/         # Deployment tool
│       └── MySQL TableEditor/         # MySQL table editor utility
├── Apps/
│   ├── BibleReader/                  # Standalone Bible reader web app
│   │   ├── BibleReader.Core/         # Bible domain model
│   │   └── BibleReader/              # ASP.NET Core web host (net10.0)
│   └── LessonReviewer/               # Lesson playback/review web app
│       ├── LessonReviewer.Core/      # Lesson domain model
│       └── LessonReviewer/           # ASP.NET Core web host (net10.0)
├── Libraries/
│   └── Media-Player-ASP.NET-Control/ # Media player abstraction (net10.0)
├── Directory.Build.props              # Shared MSBuild properties
├── docs/plans/NET10_MIGRATION_PLAN.md # Migration plan & task tracker
└── README.md                          # Getting-started guide
```

### Project count by target framework (current state)

| Target | Count | Status |
|---|---|---|
| `net10.0` | 33 | Migrated (includes scaffolded web hosts) |
| `net10.0-windows` | 1 | DbManagerWPF |
| `net48` | 12 | Remaining to migrate |
| Unknown | 1 | MySQL TableEditor (parsing issue) |
| **Total** | 47 | |

---

## 3) Core Framework (WCMS.Framework)

**Project:** `Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj`
**Target:** `net10.0` (migrated)

This is the heart of the CMS. It defines the entire domain model, data access, and
rendering pipeline.

### 3.1) Object hierarchy

```
IWebObject
  └── WebObjectBase  (Id, OBJECT_ID)
        └── WebObject  (Name, Description, Active, ...)
              └── NamedWebObject  (+ named identity)
                    ├── ParameterizedWebObject  (+ WebParameter storage)
                    │     ├── WebTemplate
                    │     ├── WebPartAdmin
                    │     └── WebPartConfig (obsolete)
                    ├── SecurableObject  (+ management-level security)
                    │     ├── WPart  (WebPart — logical part group)
                    │     ├── WebPartControl  (renderable control definition)
                    │     └── WebPartControlTemplate  (display template for a control)
                    └── PublicSecurableObject  (+ public access control)
                          ├── WSite  (website container)
                          ├── WebMasterPage  (layout)
                          ├── WebTheme / WebSkin  (styling)
                          └── PageElementBase
                                ├── WPage  (page within a site)
                                └── WebPageElement  (part instance on a page)
```

### 3.2) Key domain classes

| Class | File | Purpose |
|---|---|---|
| `WSite` | `SiteModel/WebSite.cs` | Website: hostname, login page, title format, child sites |
| `WPage` | `SiteModel/WebPage.cs` | Page: URL identity, master page link, theme, rank |
| `WebMasterPage` | `SiteModel/WebMasterPage.cs` | Layout: links to template, owns page elements |
| `WebTemplate` | `SiteModel/WebTemplate.cs` | Content template: markup, version, engine type |
| `WebTemplatePanel` | `SiteModel/WebTemplatePanel.cs` | Named content zone within a template |
| `WebPageElement` | `SiteModel/WebPageElement.cs` | Instance of a part on a page/master page |
| `WebPagePanel` | `SiteModel/WebPagePanel.cs` | Page-level panel override/inherit behavior |
| `WPart` | `PartModel/WebPart.cs` | Logical part group (e.g., "Article") |
| `WebPartControl` | `PartModel/WebPartControl.cs` | Control definition (e.g., "ArticleList") |
| `WebPartControlTemplate` | `PartModel/WebPartControlTemplate.cs` | Render template for a control |
| `WebPartAdmin` | `PartModel/WebPartAdmin.cs` | Admin/config UI for a part |

### 3.3) Manager classes (31 managers)

Each domain object has a corresponding manager providing CRUD and query operations:

`WebSiteManager`, `WebPageManager`, `WebMasterPageManager`, `WebTemplateManager`,
`WebTemplatePanelManager`, `WebPageElementManager`, `WebPagePanelManager`,
`WebPartManager`, `WebPartControlManager`, `WebPartControlTemplateManager`,
`WebPartAdminManager`, `WebThemeManager`, `WebSkinManager`,
`WebObjectSecurityManager`, `WebPermissionManager`, `WebUserManager`,
`WebGroupManager`, `WebRoleManager`, `WebRegistryManager`, `WebParameterManager`,
`WebParameterSetManager`, `WebAddressManager`, `WebBindingManager`,
`WebJobManager`, `WebLogManager`, `WebShareManager`, `WebCommentManager`,
`WebEventManager`, `WebShortUrlManager`, `WebCategoryManager`, `WebTagManager`

### 3.4) Data context

- **Legacy:** `WFrameworkModel.edmx` → Entity Framework 6 Database First
- **Migrated:** `WFrameworkModel.Context.cs` → EF Core `DbContext` with `OnModelCreating`
- Database: SQL Server with stored procedures for data providers

---

## 4) Shared Library (WCMS.Common)

**Project:** `Portal/WebSystem/WCMS.Common/WCMS.Common.csproj`
**Target:** `net10.0` (migrated)

General-purpose utilities shared by all projects.

| Area | Key classes | Purpose |
|---|---|---|
| **Data access** | `IDataProvider<T>`, `SqlDataProviderBase`, `GenericSqlDataProvider` | Abstract CRUD via stored procedures |
| **Configuration** | `ConfigUtil` | Wrapper around `ConfigurationManager` |
| **Logging** | `ILogger`, `LogManager`, `FileLogger`, `ConsoleLogger` | Multi-sink logging |
| **Query parsing** | `QueryParser` | URL query-string builder & parser |
| **Web helpers** | `WebHelper` | HTTP redirects, security, URL utilities |
| **Data helpers** | `SqlHelper`, `DataHelper`, `CsvHelper`, `XmlUtil` | Data transformation |
| **IO** | `FileHelper`, `FtpHelper`, `Compression`, `ImageUtil` | File operations |
| **Network** | `SmtpHelper`, `SmsHelper`, `NetworkConnection` | Email, SMS, network |
| **Serialization** | `SerializationUtil`, `JsonUtil` | Object serialization |

### Migration note
`System.Web` references replaced with `Microsoft.AspNetCore.SystemWebAdapters 2.0.0`.
WebForms-only classes (`ImageSecurity`, `Dates`, `OracleHelper`) excluded from compilation.

---

## 5) View-Model Layer (WCMS.WebSystem.ViewModels)

**Project:** `Portal/WebSystem/WCMS.WebSystem.ViewModels/WCMS.WebSystem.csproj`
**Target:** `net10.0` (migrated)

Bridges the framework domain model to the web presentation layer.

| Class | Purpose |
|---|---|
| `WebSystemViewModel` | System-level admin tabs & navigation |
| `WebSiteViewModel` | Site list rendering (ListItem generation) |
| `WebPageViewModel` | Page tree & tab generation |
| `WebPartViewModel` | Part tree structure for admin UI |
| `WebMasterPageViewModel` | Master page listing |
| `WebOfficeViewModel` | Office/workspace views |
| `WebGroupViewModel` | User group management views |
| `WebPageElementViewModel` | Element configuration views |
| `FrameworkData` | Request context: UserId, PageId, SiteId, ElementId |

### Key interfaces

| Interface | Purpose |
|---|---|
| `IWebPartControl` | Part control with page title capability |
| `IConfigurablePart` | Part lifecycle: Delete, Manage, ViewHome |
| `IObjectValueProvider` | Dictionary-based value resolution for data binding |
| `ITabControl` | Dynamic tab management |
| `ITextEditor` | Rich-text editor abstraction |

### Migration note
11 WebForms-specific files excluded from compilation (`WPage.cs`, `WUserControl.cs`,
`RazorHelper.cs`, `WebPartBase.cs`, various ViewModel files that depend on `System.Web.UI`).
These need Razor Pages / ViewComponent equivalents.

---

## 6) Data Provider Layer

### Provider pattern

```
IWebXxxProvider  (interface in WCMS.Framework)
       │
       ├── SqlXxxProvider  (in WCMS.Framework.Core.SqlProvider)
       │     Uses: stored procedures via SqlHelper
       │
       └── XmlXxxProvider  (in WCMS.Framework.Core.XmlProvider)
              Uses: XML file storage
```

### Projects

| Project | Target | Purpose |
|---|---|---|
| `WCMS.Framework.Core.SqlProvider` | `net10.0` | SQL Server data providers (30+ providers) |
| `WCMS.Framework.Core.XmlProvider` | `net10.0` | XML file-based data providers |
| `WCMS.Framework.Core.SqlProvider.Smo` | `net10.0` | SQL SMO script generation |

### How providers are loaded

```csharp
// In DataAccess.cs:
public static T CreateProvider<T>() {
    // Reads provider assembly & type name from WConfig (registry)
    // Uses ReflectionUtil to load and instantiate
    return ReflectionUtil.LoadAndCreateInstance<T>(typeName);
}
```

This **factory pattern with config-driven type loading** is central to the CMS's
extensibility. Providers can be swapped at runtime via registry settings.

---

## 7) Web Part / Module System

Web parts are the CMS's pluggable component architecture. Each module provides one or
more web parts that can be placed on any page.

### Module inventory

#### SystemParts (Group 1) — Content & productivity

| Module | Project | Description |
|---|---|---|
| **Article** | `WCMS.WebSystem.Apps.Article` | Content publishing: articles, columns, templates, RSS |
| **FileManager** | `WCMS.WebSystem.Apps.FileManager` | File/folder management with versioning |
| **EventCalendar** | `WCMS.WebSystem.Apps.EventCalendar` | Calendar events, reminders, recurrence |
| **Contact** | `WCMS.WebSystem.Apps.Contact` | Contact forms & inquiry management |
| **GenericList** | `WCMS.WebSystem.Apps.GenericList` | Dynamic data lists with configurable columns |
| **RemoteIndexer** | `WCMS.WebSystem.Apps.RemoteIndexer` | Remote library search & indexing |
| **WeeklyScheduler** | `WCMS.WebSystem.Apps.WeeklyScheduler` | Weekly schedule management |
| **WebParts (core)** | `WCMS.WebSystem.Apps` | Shared part infrastructure |

#### SystemPartsG2 (Group 2) — Community & social

| Module | Project | Description |
|---|---|---|
| **Forum** | `WCMS.WebSystem.Apps.Forum` | Discussion forums: categories, threads, posts |
| **Social** | `WCMS.Framework.Social` | Social wall: updates, events, profiles |
| **Social.ViewModel** | `WCMS.WebSystem.Apps.Social.ViewModel` | Social UI bindings |
| **WebParts G2** | `WCMS.WebSystem.Apps` (G2) | Ads, newsletter, flash banner modules |

#### SystemPartsG3 (Group 3) — Support & HR

| Module | Project | Description |
|---|---|---|
| **Incident** | `WCMS.WebSystem.Apps.Incident` | Ticket/issue tracking: categories, types, history |
| **Jobs** | `WCMS.WebSystem.Apps.Jobs` | Job posting & application system |

#### Integration — External systems

| Module | Project | Description |
|---|---|---|
| **Integration Core** | `WCMS.WebSystem.Apps.Integration` | External DB sync, attendance, music competition |
| **BibleReader adapter** | `WCMS.WebSystem.Apps.BibleReader` | Bible content integration |

#### BranchLocator

| Module | Project | Description |
|---|---|---|
| **BranchLocator** | `WCMS.WebSystem.Apps.BranchLocator` | Branch/location finder with map integration |

### How web parts are loaded

1. **Registration:** `WPart` (WebPart) record in database with identity (e.g., "Article")
2. **Controls:** `WebPartControl` records define renderable controls (e.g., "ArticleList", "ArticleDetail")
3. **Templates:** `WebPartControlTemplate` records specify render files (`.ascx` or `.cshtml`)
4. **Placement:** `WebPageElement` records bind a part template to a page/panel
5. **Runtime:** The rendering pipeline resolves the template file path and instantiates it

### Template resolution

```
~/Content/Parts/{PartIdentity}/{TemplatePath}
```

Example: `~/Content/Parts/Article/Default/ArticleList.ascx`

Two template engine types are supported:
- **ASPX** (WebForms) — `.ascx` user controls
- **Razor** — `.cshtml` views

---

## 8) Dynamic Page Rendering Pipeline

### Request flow

```
HTTP Request
    │
    ▼
┌───────────────────┐
│  WebRewriter       │  URL resolution: matches path segments to
│  .ResolvePage()    │  WSite → WPage hierarchy in database
└───────────────────┘
    │
    ▼
┌───────────────────┐
│  WContext          │  Creates request context:
│  (FrontEnd/Edit/   │  - ObjectId, RecordId, Query
│   Admin mode)      │  - ValueProvider dictionary
└───────────────────┘
    │
    ▼
┌───────────────────┐
│  WPage             │  Resolves:
│  (page object)     │  - MasterPageId → WebMasterPage
│                    │  - ThemeId → WebTheme → WebSkin
└───────────────────┘
    │
    ▼
┌───────────────────┐
│  WebTemplate       │  Master page's template contains
│  + Panels          │  WebTemplatePanel items (named zones)
└───────────────────┘
    │
    ▼
┌───────────────────┐
│  WebPageElement    │  For each panel: load elements
│  (per panel)       │  Each element → WebPartControlTemplate
└───────────────────┘
    │
    ▼
┌───────────────────┐
│  Template Engine   │  ASPX or Razor rendering
│  (.ascx / .cshtml) │  with security check per element
└───────────────────┘
    │
    ▼
  HTML Response
```

### URL resolution algorithm (WebRewriter)

1. Request path `/` → return site home page
2. Split path by `/` → match first segment to `WSite.Identity`
3. Traverse sub-site hierarchy via `ParentId`
4. Remaining segments match `WPage.Identity` within the resolved site
5. Fallback: check `WebShortUrl` table for single-segment aliases
6. Result: `WPage` object or redirect URL

### Context modes

| Mode | WContextTypes | Use case |
|---|---|---|
| FrontEnd | `0` | Normal page rendering |
| EditMode | `1` | Page/element editing in-place |
| AdminMode | `2` | Part administration panel |

---

## 9) Site, Page & Template Model

### Multi-site architecture

```
WSite (root)
  ├── WSite (child site 1)  ← different domain/hostname
  │     ├── WPage (home)
  │     ├── WPage (about)
  │     └── WPage (contact)
  └── WSite (child site 2)
        └── WPage (home)
```

Each site has:
- `HostName` — domain binding
- `LoginPage` — authentication page reference
- `PageTitleFormat` — title template
- `Identity` — URL slug
- `ThemeId` — default theme

### Page structure

```
WPage
  ├── MasterPageId → WebMasterPage
  │                    └── TemplateId → WebTemplate
  │                                      └── WebTemplatePanel[] (content zones)
  ├── ThemeId → WebTheme → WebSkin
  ├── WebPagePanel[] (panel overrides)
  └── WebPageElement[] (part instances)
         └── PartControlTemplateId → WebPartControlTemplate
                                       └── GetRenderPath() → physical file
```

### Theming

- **WebTheme**: A collection of themed templates
- **WebSkin**: Visual skin applied to a theme (CSS/assets)
- Themes are set at site level, overridable per-page
- Theme templates are stored as `.ascx`/`.cshtml` in `Content/Themes/{ThemeName}/`

---

## 10) Security & Authentication

### Object-level security

Every `SecurableObject` and `PublicSecurableObject` has:
- **Public access level**: `WebPublicAccess` (All, Authenticated, None, etc.)
- **Management permissions**: per-user/group/role permission matrix
- **Inherited permissions**: cascading from parent objects

```csharp
// Check if current user can view
object.GetPublicAccess(session);  // → WebPublicAccess enum

// Check if user can manage
object.IsUserMgmtPermitted(permissionId);
```

### Key security classes

| Class | Purpose |
|---|---|
| `WebObjectSecurity` | Permission matrix: IsUserPermitted, IsUserAdded |
| `WebUser` | User account with login, email, profile |
| `WebGroup` | User group for access control |
| `WebRole` | Predefined roles (Admin, SiteManager, User) |
| `WebPermission` / `WebPermissionSet` | Permission definitions |
| `WebGlobalPolicy` | System-wide security policies |
| `LoginCookieManager` | Cookie-based authentication (60-day expiry) |
| `OtpCodeGenerator` | One-time password generation |
| `UserSessionManager` | Browser session tracking |

### Authentication flow (legacy)

1. Login form → `WSession.Login(userId)` → sets session state
2. `LoginCookieManager.RememberLogin()` → encrypted auth cookie
3. `WebRewriter` checks page access on each request
4. OWIN middleware (`Startup.Auth.cs`) for OAuth/external providers

---

## 11) Registry & Configuration

### Registry system

The CMS uses a hierarchical registry (similar to Windows Registry) stored in database:

```
/System
  ├── /Debugging
  ├── /Resources
  ├── /SMTP
  │     ├── Host
  │     ├── Port
  │     └── Credentials
  ├── /SMSConfig
  ├── /Agent
  │     ├── JobTimerInterval
  │     └── JobCacheRefreshInterval
  └── /SiteIdentity
```

### WConfig class

Central configuration with live-reloading via registry events:

| Property | Purpose |
|---|---|
| `DefaultSite` | System's default WSite object |
| `BaseAddress` | Site's base URL |
| `PageExt` | URL extension (`.html`) |
| `SystemNode` | Root registry node |
| `AllowCache` | Full-page cache flag |
| `Environment` | DEV, UAT, PROD, DEV_ISOLATED |

```csharp
// Configuration updates propagate via events
WebRegistry.Updated += (sender, args) => {
    // Refresh cached properties
};
```

### WebParameter system

Any domain object can have associated key/value parameters:
```csharp
// Stored via WebParameterManager
WebParameter.GetList(objectId, objectType);
```

---

## 12) Background Agent System

### Architecture

```
WCMS.Framework.Agent (console app, net10.0)
    │
    ├── FrameworkAgent       Scheduler: manages job queue
    │     └── Worker threads  Execute individual jobs
    │
    └── AgentTaskBase        Base class for all jobs
          └── StartManagedExecution()

WCMS.Framework.AgentService (worker service, net10.0)
    └── BackgroundService    Hosts agent as a managed service
```

### Job execution

1. Registry path `/System/Agent` → configuration
2. `WebJob` records define scheduled tasks with `TypeName` and schedule
3. `ReflectionUtil.LoadAndCreateInstance<AgentTaskBase>(typeName)` → creates task
4. Jobs are loaded from configured assemblies via reflection
5. Supports single-job (`/task:name`) and all-jobs modes

### Built-in agent tasks

| Task | Module | Purpose |
|---|---|---|
| `EventReminderSender` | EventCalendar | Send calendar event reminders |
| `EmailUpdater` | Utilities | Process queued emails |
| `RemoteLibraryIndexer` | RemoteIndexer | Index remote content |

---

## 13) Satellite Applications

### BibleReader

**Purpose:** Bible reading and search application.

| Component | Target | Description |
|---|---|---|
| `BibleReader.Core` | `net48` | Domain model: BibleVersion, BibleBook, BibleChapter, BibleVerse |
| `BibleReader.WebApp` | `net10.0` | ASP.NET Core host (scaffold) |

**Features:** Version selector, book/chapter/verse navigation, search, user sessions.
**Legacy:** WebForms pages (`.aspx`), SOAP web service (`BibleService.asmx`).

### LessonReviewer

**Purpose:** Lesson recording playback and review system.

| Component | Target | Description |
|---|---|---|
| `LessonReviewer.Core` | `net48` | Domain: ConfigManager, PlaybackHelper, ServiceDefinition |
| `LessonReviewer` | `net10.0` | ASP.NET Core host (scaffold) |

**Features:** Lesson playback, configuration management, admin interface.
**Legacy:** WebForms pages, XML-based config (`App_Data/Config.xml`).

### BranchLocator

**Purpose:** Branch/location finder with map integration.

| Component | Target | Description |
|---|---|---|
| `WCMS.WebSystem.Apps.BranchLocator` | `net48` | Core: MChapter, FALHelper, location data |
| `BranchLocator.WebApp` | `net10.0` | ASP.NET Core host with Razor views |

**Features:** Search, map display, chapter browser, announcements.

---

## 14) Utility Projects

| Project | Target | Purpose |
|---|---|---|
| `DbManager` (CLI) | `net48` | Database backup, restore, migration via command-line |
| `DbManagerWPF` | `net10.0-windows` | WPF-based database management tool |
| `PostBuildManager` | `net48` | Post-build file copy and automation |
| `WebExtractor` | `net48` | Web content extraction tool |
| `WebSystemDeployer` | `net48` | IIS deployment automation |
| `MySQL TableEditor` | `net48` | MySQL table editing utility |

---

## 15) Legacy Web Assets Inventory

### Files requiring migration

| Asset type | Count | Location | Migration target |
|---|---|---|---|
| `.aspx` (WebForms pages) | 57 | Portal, BibleReader, LessonReviewer | Razor Pages / MVC views |
| `.ascx` (user controls) | 518 | Portal (themes, parts, controls, admin) | Razor view components / partial views |
| `.cshtml` (Razor views) | 57 | Portal, BranchLocator, Integration | Keep (already Razor) |
| `.svc` (WCF services) | 6 | Integration, SystemParts, Portal | Web API controllers |
| `.master` (master pages) | 0 | (none in repo) | N/A |
| `.asmx` (SOAP services) | 1 | BibleReader | Web API endpoint |
| `web.config` | 5+ | Various web projects | `appsettings.json` |
| `packages.config` | ~10 | Remaining net48 projects | `<PackageReference>` |

### ASCX breakdown by area

| Area | Count | Content |
|---|---|---|
| Parts/Central/ | ~100 | Admin: security, templates, web parts, tools, manager |
| Parts/Common/ | ~10 | Login, comments, user photo, message board |
| Themes/ | ~50 | Bootstrap, Central, Basic, test theme templates |
| Controls/ | ~12 | Tab control, text editor, breadcrumb, date picker |
| AppBundle/ (SystemParts) | ~200 | Module-specific controls: article, calendar, file manager |
| AppBundle2/ (SystemPartsG2) | ~80 | Forum, social, ads, newsletter |
| AppBundle3/ (SystemPartsG3) | ~40 | Incident, jobs |
| Admin/ | ~15 | Site admin, permissions, user profiles |
| Windows/ | ~7 | Popup dialogs: file browser, link browser, upload |

---

## 16) Feature-by-Feature Migration Guide

### 16.1) Dynamic page rendering → ASP.NET Core middleware + Razor

**Legacy:** `WebRewriter` + WebForms page lifecycle + `System.Web.UI.Control` tree

**Migration strategy:**

```
ASP.NET Core Pipeline:
  Middleware:  URL resolution (replaces WebRewriter)
  └── Custom IRouter or endpoint routing with database lookup
  └── PageRenderingMiddleware:
        1. Resolve WPage from URL
        2. Load WebTemplate (layout)
        3. For each panel: load WebPageElements
        4. Render via Razor Components or partial views
        5. Apply theme/skin CSS
```

**Key changes:**
- Replace `System.Web.HttpContext` → `Microsoft.AspNetCore.Http.HttpContext` (via SystemWebAdapters or direct)
- Replace WebForms control tree → Razor view components (`ViewComponent`)
- Replace `.ascx` user controls → `.cshtml` partial views or Tag Helpers
- Replace `Page` lifecycle → middleware pipeline
- `WContext` → scoped service registered in DI container
- `WSession` → `IHttpContextAccessor` + ASP.NET Core session/claims

### 16.2) Web parts system → View Components + DI

**Legacy:** Reflection-loaded `.ascx` controls via `WebPartControlTemplate.GetRenderPath()`

**Migration strategy:**

```csharp
// ASP.NET Core View Component pattern:
public class ArticleListViewComponent : ViewComponent
{
    private readonly IArticleProvider _provider;
    
    public ArticleListViewComponent(IArticleProvider provider)
    {
        _provider = provider;
    }
    
    public async Task<IViewComponentResult> InvokeAsync(int elementId)
    {
        var articles = await _provider.GetListAsync(elementId);
        return View(articles);  // Views/Shared/Components/ArticleList/Default.cshtml
    }
}
```

**Key changes:**
- Each `.ascx` web part → `ViewComponent` class + `.cshtml` view
- `ReflectionUtil.LoadAndCreateInstance` → DI service registration
- `IWebPartControl` interface → `ViewComponent` base class
- Part template resolution → convention-based or `IViewComponentSelector`

### 16.3) Master pages & themes → Razor Layouts

**Legacy:** `WebTemplate` with `WebTemplatePanel` content zones, rendered as WebForms master pages

**Migration strategy:**

```
WebTemplate → _Layout.cshtml (Razor layout)
WebTemplatePanel → @RenderSection("PanelName", required: false)
WebPageElement rendering → @await Component.InvokeAsync("PartName", ...)
WebTheme/WebSkin → CSS theme files + _Layout selection
```

**Key changes:**
- Template content stored in DB → rendered via Razor layout engine
- Template panels → `@RenderSection` or `@RenderBody`
- Theme selection → layout file selection per request
- Dynamic layout: middleware selects `_Layout.cshtml` based on `WPage.ThemeId`

### 16.4) Authentication → ASP.NET Core Identity

**Legacy:** `FormsAuthentication` + OWIN middleware + `LoginCookieManager`

**Migration strategy:**

```csharp
// In Program.cs:
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.LoginPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromDays(60);
    });

builder.Services.AddAuthorization(options => {
    options.AddPolicy("SiteAdmin", policy => 
        policy.RequireClaim("UserType", "Admin", "SiteManager"));
});
```

**Key changes:**
- `FormsAuthentication` → Cookie authentication middleware
- `WSession.Login()` → `HttpContext.SignInAsync()` with claims
- `LoginCookieManager` → built-in cookie auth with sliding expiration
- `WebObjectSecurity.IsUserPermitted()` → authorization policies + middleware
- OWIN → native ASP.NET Core middleware

### 16.5) Entity Framework 6 → EF Core

**Legacy:** EDMX Database First model (`WFrameworkModel.edmx`)

**Migration status:** Already migrated to `Microsoft.EntityFrameworkCore 9.0.0`

**Remaining work:**
- Integration module still on EF6 (`ExternalDBModel.edmx`)
- BranchLocator still on EF6
- WeeklyScheduler uses EDMX model

**Strategy:** Code-first `DbContext` with `OnModelCreating` configuration.

### 16.6) WCF services → Web API / Minimal API

**Legacy:** 6 `.svc` endpoints (Member, Music, DataSync, Content Service, User)

**Migration strategy:**

```csharp
// Each WCF operation → API endpoint:
app.MapPost("/api/integration/member/sync", async (SyncRequest req, IMemberService svc) => {
    var result = await svc.SyncAsync(req);
    return Results.Ok(result);
});
```

**Key changes:**
- `[ServiceContract]` / `[OperationContract]` → `[ApiController]` or Minimal API
- `System.ServiceModel` → removed entirely
- XML/SOAP → JSON/REST
- Client proxies → `HttpClient` with typed clients

### 16.7) Registry system → Configuration + Options pattern

**Legacy:** `WebRegistry` tree in database, `WConfig` with event-driven reload

**Migration strategy:**

The registry system should be **preserved** as-is since it's a core CMS feature.
The database-stored registry provides runtime configuration that users can change
without restarting the application.

```csharp
// Register as a scoped/singleton service:
builder.Services.AddSingleton<IWebRegistryService, WebRegistryService>();

// WConfig properties become an IOptions<WConfigOptions>:
builder.Services.Configure<WConfigOptions>(options => {
    // Load from registry at startup, subscribe to updates
});
```

### 16.8) Background agents → .NET Worker Services

**Migration status:** Already converted.

- `WCMS.Framework.AgentService` → `BackgroundService` with `Microsoft.Extensions.Hosting`
- Job scheduling preserved via existing `WebJob` / `FrameworkAgent` infrastructure
- Cross-platform compatible (no longer requires Windows Service)

### 16.9) File management → Cross-platform paths

**Legacy:** `Server.MapPath()`, Windows-style paths, IIS file handling

**Migration strategy:**

```csharp
// Replace Server.MapPath:
var contentPath = Path.Combine(env.ContentRootPath, "Content", "Parts");

// Replace HttpPostedFile:
// IFormFile in ASP.NET Core

// File serving:
app.UseStaticFiles();
app.MapGet("/files/{**path}", async (string path, IFileManagerService svc) => {
    return Results.File(await svc.GetFileAsync(path));
});
```

### 16.10) WebForms controls → Razor components

| Legacy (WebForms) | Modern (ASP.NET Core) |
|---|---|
| `.aspx` page | Razor Page (`.cshtml` with `@page`) |
| `.ascx` user control | Partial view or View Component |
| `.master` master page | `_Layout.cshtml` |
| `<asp:Repeater>` | `@foreach` loop |
| `<asp:GridView>` | HTML `<table>` or JS data grid |
| `<asp:TextBox>` | `<input asp-for="...">` Tag Helper |
| `<asp:Button OnClick>` | `<button type="submit">` + form handler |
| `Page.IsPostBack` | Separate GET/POST handlers |
| `ViewState` | Hidden fields / session / TempData |
| `UpdatePanel` (AJAX) | htmx, fetch API, or Blazor |
| Code-behind (`.aspx.cs`) | Page model (`.cshtml.cs`) |

### 16.11) FCKeditor → Modern rich-text editor

**Legacy:** FCKeditor 2.6.3 ASP.NET control

**Migration:** Replace with CKEditor 5 or TinyMCE:

```html
<!-- In Razor view: -->
<textarea id="editor" asp-for="Content"></textarea>
<script src="https://cdn.ckeditor.com/ckeditor5/latest/classic/ckeditor.js"></script>
<script>
    ClassicEditor.create(document.querySelector('#editor'));
</script>
```

### 16.12) IIS-specific features → Cross-platform

| IIS feature | ASP.NET Core equivalent |
|---|---|
| `web.config` transforms | `appsettings.{env}.json` |
| IIS URL Rewrite | `UseRewriter()` middleware |
| Application pools | Kestrel process model |
| Windows Auth (NTLM) | Negotiate/Kerberos middleware |
| Handler mappings | Middleware pipeline |
| Static file handling | `UseStaticFiles()` |
| Compression | `UseResponseCompression()` |

---

## 17) Migration Plan Validation Notes

Based on the system documentation, the following updates/additions are recommended
for `docs/plans/NET10_MIGRATION_PLAN.md`:

### Items confirmed correct

- ✅ The bottom-up dependency order (Common → Framework → Providers → ViewModels → Modules) is correct
- ✅ EF6→EF Core migration approach is sound
- ✅ SystemWebAdapters is the right interim solution for `HttpContext` compatibility
- ✅ `#if NETFRAMEWORK` conditional compilation for WebForms-only code is appropriate
- ✅ Agent → BackgroundService conversion is correct

### Recommended additions to the migration plan

1. **Dynamic page rendering middleware** — The plan should include a task for creating
   a custom ASP.NET Core middleware that replaces `WebRewriter` URL resolution. This is
   the most critical piece: it must resolve URLs to `WPage` objects from the database and
   select the appropriate Razor layout + view components.

2. **View Component conversion plan** — Each `.ascx` web part control needs a
   corresponding `ViewComponent`. With 518 `.ascx` files, this should be prioritized
   by usage (Central admin parts first, then content parts, then theme templates).

3. **WContext as a DI service** — `WContext` currently relies on `HttpContext.Current`
   (static). It needs to be refactored into a scoped service injected via DI. This
   affects virtually every request in the system.

4. **WSession modernization** — Session management needs to move from ASP.NET session
   state to ASP.NET Core distributed session or claims-based identity. The
   `LoginCookieManager` should be replaced with ASP.NET Core cookie authentication.

5. **Registry service** — The `WebRegistry` / `WConfig` system should be wrapped in an
   `IOptions<>` pattern or a custom `IConfiguration` provider to integrate with the
   ASP.NET Core configuration system.

6. **Theme system** — Dynamic layout selection (themes) should map to a custom
   `IViewLocationExpander` or middleware that selects `_Layout.cshtml` per-request.

7. **SOAP service** — `BibleService.asmx` in BibleReader needs migration to a Web API
   endpoint (not currently in the plan).

8. **Cross-platform path handling** — All `Server.MapPath()` calls, Windows-style
   path separators, and IIS-specific file handling must be audited. The
   `SystemWebAdapters` package provides some shims, but content path resolution needs
   a cross-platform abstraction.

9. **Database provider abstraction** — The stored-procedure-based data providers in
   `SqlDataProviderBase` use `System.Data.SqlClient`. Consider migrating to
   `Microsoft.Data.SqlClient` for better .NET 10 support and potential cross-platform
   database options.

10. **Build/deployment scripts** — Current build scripts (`.cmd` files) are
    Windows-only. Create cross-platform equivalents using `dotnet` CLI commands,
    PowerShell Core scripts, or Makefile.

### Updated §7 status snapshot

Since the prior §7 snapshot was written, 21 additional projects have been migrated.
The current state is:

- 34 projects on `net10.0` / `net10.0-windows`
- 12 projects still on `net48`
- Many `packages.config` files already deleted
- Many System.Web references already removed or shimmed
- EF Core migration done for WCMS.Framework (1 of 3 EF6 projects)
