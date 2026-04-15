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
| Migration Status | Completed |
| Status Basis | Modern counterpart on .NET 10 verified; compiles with 0 errors. |
| Project References | 1 |
| Surface Artifacts | 9 |
| Component/Class Artifacts | 10 |

## Migration Execution Details

| Track | Current | Next Step |
| --- | --- | --- |
| Phase | Completed | Migration to .NET 10 complete. All source files compile with 0 errors. |
| WebForms Surface Present | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |
| Endpoint Surface Present | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |
| Class/Component Porting | Yes (Completed) | All items migrated to ASP.NET Core on .NET 10. |

## Project References

| --- | --- | --- |
| ../BibleReader.Core/BibleReader.Core.csproj |

## Pages And Views

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-001 | Completed | Master Page | legacy/BibleReader/BibleReader :: Site.Master | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Site.Master` | `legacy/BibleReader/BibleReader/Site.Master.cs` |
| LGC-001 | Completed | Page | legacy/BibleReader/BibleReader :: About.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/About.aspx` | `legacy/BibleReader/BibleReader/About.aspx.cs` |
| LGC-001 | Completed | Page | legacy/BibleReader/BibleReader/Account :: ChangePassword.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Account/ChangePassword.aspx` | `legacy/BibleReader/BibleReader/Account/ChangePassword.aspx.cs` |
| LGC-001 | Completed | Page | legacy/BibleReader/BibleReader/Account :: ChangePasswordSuccess.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Account/ChangePasswordSuccess.aspx` | `legacy/BibleReader/BibleReader/Account/ChangePasswordSuccess.aspx.cs` |
| LGC-001 | Completed | Page | legacy/BibleReader/BibleReader/Account :: Login.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Account/Login.aspx` | `legacy/BibleReader/BibleReader/Account/Login.aspx.cs` |
| LGC-001 | Completed | Page | legacy/BibleReader/BibleReader/Account :: Register.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Account/Register.aspx` | `legacy/BibleReader/BibleReader/Account/Register.aspx.cs` |
| LGC-001 | Completed | Page | legacy/BibleReader/BibleReader :: Default.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Default.aspx` | `legacy/BibleReader/BibleReader/Default.aspx.cs` |
| LGC-001 | Completed | Page | legacy/BibleReader/BibleReader :: Setup.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `legacy/BibleReader/BibleReader/Setup.aspx` | `legacy/BibleReader/BibleReader/Setup.aspx.cs` |

## User Controls

_No artifacts found._

## Services And Handlers

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File | Code-Behind / Pair |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-001 | Completed | Service | legacy/BibleReader/BibleReader :: BibleService.asmx | Legacy endpoint surface; map to ASP.NET Core APIs/services. | `legacy/BibleReader/BibleReader/BibleService.asmx` | `legacy/BibleReader/BibleReader/BibleService.asmx.cs` |

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File |
| --- | --- | --- | --- | --- | --- |
| LGC-001 | Completed | Class Component | legacy/BibleReader/BibleReader :: About | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader/About.aspx.cs` |
| LGC-001 | Completed | Class Component | legacy/BibleReader/BibleReader/Account :: ChangePassword | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader/Account/ChangePassword.aspx.cs` |
| LGC-001 | Completed | Class Component | legacy/BibleReader/BibleReader/Account :: ChangePasswordSuccess | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader/Account/ChangePasswordSuccess.aspx.cs` |
| LGC-001 | Completed | Class Component | legacy/BibleReader/BibleReader/Account :: Login | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader/Account/Login.aspx.cs` |
| LGC-001 | Completed | Class Component | legacy/BibleReader/BibleReader/Account :: Register | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader/Account/Register.aspx.cs` |
| LGC-001 | Completed | Class Component | legacy/BibleReader/BibleReader :: BibleService | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader/BibleService.asmx.cs` |
| LGC-001 | Completed | Class Component | legacy/BibleReader/BibleReader :: Default | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader/Default.aspx.cs` |
| LGC-001 | Completed | Class Component | legacy/BibleReader/BibleReader :: Global.asax | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader/Global.asax.cs` |
| LGC-001 | Completed | Class Component | legacy/BibleReader/BibleReader :: Setup | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader/Setup.aspx.cs` |
| LGC-001 | Completed | Class Component | legacy/BibleReader/BibleReader :: Site | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `legacy/BibleReader/BibleReader/Site.Master.cs` |
