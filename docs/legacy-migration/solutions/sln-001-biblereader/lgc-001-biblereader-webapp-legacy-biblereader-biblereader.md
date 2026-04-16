# LGC-001 - BibleReader.WebApp

## Migration Tracking Card

| Field | Value |
| --- | --- |
| Item ID | LGC-001 |
| Project Type | App |
| Project File | `legacy/BibleReader/BibleReader/BibleReader.WebApp.csproj` |
| Modern Project File / Evidence | `Apps/BibleReader/BibleReader/BibleReader.WebApp.csproj` |
| Project Directory | `legacy/BibleReader/BibleReader` |
| Output Type | Library |
| Target Framework | v4.7 |
| Migration Status | Completed |
| Status Basis | Inventory validation from `legacy-source-tracking-all.csv`: Completed:22, Not Applicable:20, Incomplete:0, Not Started:0. All tracked files for this project are resolved. |
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

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) | Code-Behind / Pair (relative to Project Directory) |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-001 | Completed | Master Page | legacy/BibleReader/BibleReader :: Site.Master | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./Site.Master` | `./Site.Master` | `./Site.Master.cs` |
| LGC-001 | Not Applicable | Page | legacy/BibleReader/BibleReader :: About.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./About.aspx` | N/A (retired/replaced in modern architecture). | `./About.aspx.cs` |
| LGC-001 | Not Applicable | Page | legacy/BibleReader/BibleReader/Account :: ChangePassword.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./Account/ChangePassword.aspx` | N/A (retired/replaced in modern architecture). | `./Account/ChangePassword.aspx.cs` |
| LGC-001 | Not Applicable | Page | legacy/BibleReader/BibleReader/Account :: ChangePasswordSuccess.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./Account/ChangePasswordSuccess.aspx` | N/A (retired/replaced in modern architecture). | `./Account/ChangePasswordSuccess.aspx.cs` |
| LGC-001 | Not Applicable | Page | legacy/BibleReader/BibleReader/Account :: Login.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./Account/Login.aspx` | N/A (retired/replaced in modern architecture). | `./Account/Login.aspx.cs` |
| LGC-001 | Not Applicable | Page | legacy/BibleReader/BibleReader/Account :: Register.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./Account/Register.aspx` | N/A (retired/replaced in modern architecture). | `./Account/Register.aspx.cs` |
| LGC-001 | Not Applicable | Page | legacy/BibleReader/BibleReader :: Default.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./Default.aspx` | N/A (retired/replaced in modern architecture). | `./Default.aspx.cs` |
| LGC-001 | Completed | Page | legacy/BibleReader/BibleReader :: Setup.aspx | WebForms UI surface; plan UI migration target (Razor/Blazor/SPA). | `./Setup.aspx` | Project-level evidence: `./BibleReader.WebApp.csproj` | `./Setup.aspx.cs` |

## User Controls

_No artifacts found._

## Services And Handlers

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) | Code-Behind / Pair (relative to Project Directory) |
| --- | --- | --- | --- | --- | --- | --- | --- |
| LGC-001 | Completed | Service | legacy/BibleReader/BibleReader :: BibleService.asmx | Legacy endpoint surface; map to ASP.NET Core APIs/services. | `./BibleService.asmx` | `./Api/BibleApiController.cs` | `./BibleService.asmx.cs` |

## Components And Classes

| Item ID | Migration Status | Artifact Type | Feature / Functionality (Inferred) | Migration Note | Source File (relative to Project Directory) | Modern File / Evidence (relative when in-project) |
| --- | --- | --- | --- | --- | --- | --- |
| LGC-001 | Not Applicable | Class Component | legacy/BibleReader/BibleReader :: About | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./About.aspx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-001 | Not Applicable | Class Component | legacy/BibleReader/BibleReader/Account :: ChangePassword | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Account/ChangePassword.aspx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-001 | Not Applicable | Class Component | legacy/BibleReader/BibleReader/Account :: ChangePasswordSuccess | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Account/ChangePasswordSuccess.aspx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-001 | Not Applicable | Class Component | legacy/BibleReader/BibleReader/Account :: Login | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Account/Login.aspx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-001 | Not Applicable | Class Component | legacy/BibleReader/BibleReader/Account :: Register | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Account/Register.aspx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-001 | Not Applicable | Class Component | legacy/BibleReader/BibleReader :: BibleService | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./BibleService.asmx.cs` | `./Api/LegacyBibleServiceController.cs` |
| LGC-001 | Not Applicable | Class Component | legacy/BibleReader/BibleReader :: Default | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Default.aspx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-001 | Not Applicable | Class Component | legacy/BibleReader/BibleReader :: Global.asax | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Global.asax.cs` | `./Program.cs` |
| LGC-001 | Not Applicable | Class Component | legacy/BibleReader/BibleReader :: Setup | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Setup.aspx.cs` | N/A (retired/replaced in modern architecture). |
| LGC-001 | Not Applicable | Class Component | legacy/BibleReader/BibleReader :: Site | Migrated to .NET 10. Modern counterpart compiles with 0 errors. | `./Site.Master.cs` | `./Site.Master` |
