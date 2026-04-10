# P009 - TableEditor

## Project Tracking Summary

| Field | Value |
|---|---|
| Project Path | `legacy/Portal/Utilities/MySQL TableEditor/TableEditor.csproj` |
| Project Kind | Utility Project |
| Assembly Name | `TableEditor` |
| Target Framework | `v4.7` |
| Output Type | `WinExe` |
| Migration Status | Do Not Migrate As-Is |
| Status Basis | Legacy MySQL table editor is outside core product scope and has high operational risk. |
| Target Alternative | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. |
| Tracking Owner | `TBD` |
| Target Milestone | `TBD` |

## Surface Coverage Snapshot

| Surface | Count |
|---|---:|
| Core Component | 1 |
| Configuration/Resource | 4 |
| Assembly Metadata | 1 |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Assembly Metadata | `(root)` | `AssemblyInfo` | `AssemblyInfo.cs` | Do Not Migrate As-Is | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. | Legacy DB editor; retire/replace with controlled admin tooling. |
| Core Component | `(root)` | `Form1` | `Form1.cs` | Do Not Migrate As-Is | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. | Legacy DB editor; retire/replace with controlled admin tooling. |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Configuration/Resource | `(root)` | `Form1` | `Form1.resx` | Do Not Migrate As-Is | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. | Legacy DB editor; retire/replace with controlled admin tooling. |
| Configuration/Resource | `(root)` | `app` | `app.config` | Do Not Migrate As-Is | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. | Legacy DB editor; retire/replace with controlled admin tooling. |
| Configuration/Resource | `(root)` | `nuget` | `nuget.config` | Do Not Migrate As-Is | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. | Legacy DB editor; retire/replace with controlled admin tooling. |
| Configuration/Resource | `(root)` | `packages` | `packages.config` | Do Not Migrate As-Is | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. | Legacy DB editor; retire/replace with controlled admin tooling. |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

