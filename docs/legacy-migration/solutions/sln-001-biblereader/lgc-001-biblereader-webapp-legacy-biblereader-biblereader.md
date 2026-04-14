# LGC-001 - BibleReader.WebApp

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-001 |
| Project Type | App |
| Project File | `legacy/BibleReader/BibleReader/BibleReader.WebApp.csproj` |
| Project Directory | `legacy/BibleReader/BibleReader` |
| Output Type | Library |
| Target Framework | v4.7 |
| Migration Status | Partial |
| Status Basis | Legacy target (v4.7) with modern build artifacts in obj/bin. |
| Project References | 1 |
| Surface Artifacts | 9 |
| Component/Class Artifacts | 10 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Execution (In Progress) | Close remaining legacy runtime/UI/endpoint gaps and define cutover tests. |
| WebForms Surface Present | Yes | If `Yes`, define replacement pages/components and parity checklist. |
| Endpoint Surface Present | Yes | If `Yes`, map ASMX/SVC/ASHX to target API pattern. |
| Class/Component Porting | Yes | Review `System.Web` and framework-specific dependencies. |

## Project References

| --- | --- | --- |
| ../BibleReader.Core/BibleReader.Core.csproj |

## Pages And Views

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-001 | Partial | Master Page | legacy/BibleReader/BibleReader :: Site.Master | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Site.Master` | `legacy/BibleReader/BibleReader/Site.Master.cs` |
| LGC-001 | Partial | Page | legacy/BibleReader/BibleReader :: About.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/About.aspx` | `legacy/BibleReader/BibleReader/About.aspx.cs` |
| LGC-001 | Partial | Page | legacy/BibleReader/BibleReader/Account :: ChangePassword.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Account/ChangePassword.aspx` | `legacy/BibleReader/BibleReader/Account/ChangePassword.aspx.cs` |
| LGC-001 | Partial | Page | legacy/BibleReader/BibleReader/Account :: ChangePasswordSuccess.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Account/ChangePasswordSuccess.aspx` | `legacy/BibleReader/BibleReader/Account/ChangePasswordSuccess.aspx.cs` |
| LGC-001 | Partial | Page | legacy/BibleReader/BibleReader/Account :: Login.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Account/Login.aspx` | `legacy/BibleReader/BibleReader/Account/Login.aspx.cs` |
| LGC-001 | Partial | Page | legacy/BibleReader/BibleReader/Account :: Register.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Account/Register.aspx` | `legacy/BibleReader/BibleReader/Account/Register.aspx.cs` |
| LGC-001 | Partial | Page | legacy/BibleReader/BibleReader :: Default.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Default.aspx` | `legacy/BibleReader/BibleReader/Default.aspx.cs` |
| LGC-001 | Partial | Page | legacy/BibleReader/BibleReader :: Setup.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Setup.aspx` | `legacy/BibleReader/BibleReader/Setup.aspx.cs` |

## User Controls

_No artifacts found._

## Services And Handlers

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-001 | Partial | Service | legacy/BibleReader/BibleReader :: BibleService.asmx | Legacy endpoint surface; map to ASP.NET Core APIs/services. | `legacy/BibleReader/BibleReader/BibleService.asmx` | `legacy/BibleReader/BibleReader/BibleService.asmx.cs` |

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-001 | Partial | Class Component | legacy/BibleReader/BibleReader :: About | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader/About.aspx.cs` |
| LGC-001 | Partial | Class Component | legacy/BibleReader/BibleReader/Account :: ChangePassword | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader/Account/ChangePassword.aspx.cs` |
| LGC-001 | Partial | Class Component | legacy/BibleReader/BibleReader/Account :: ChangePasswordSuccess | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader/Account/ChangePasswordSuccess.aspx.cs` |
| LGC-001 | Partial | Class Component | legacy/BibleReader/BibleReader/Account :: Login | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader/Account/Login.aspx.cs` |
| LGC-001 | Partial | Class Component | legacy/BibleReader/BibleReader/Account :: Register | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader/Account/Register.aspx.cs` |
| LGC-001 | Partial | Class Component | legacy/BibleReader/BibleReader :: BibleService | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader/BibleService.asmx.cs` |
| LGC-001 | Partial | Class Component | legacy/BibleReader/BibleReader :: Default | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader/Default.aspx.cs` |
| LGC-001 | Partial | Class Component | legacy/BibleReader/BibleReader :: Global.asax | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader/Global.asax.cs` |
| LGC-001 | Partial | Class Component | legacy/BibleReader/BibleReader :: Setup | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader/Setup.aspx.cs` |
| LGC-001 | Partial | Class Component | legacy/BibleReader/BibleReader :: Site | Library/business component; assess API compatibility and dependencies. | `legacy/BibleReader/BibleReader/Site.Master.cs` |
