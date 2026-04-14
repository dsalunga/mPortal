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

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Assembly Metadata | `(root)` | `AssemblyInfo` | Do Not Migrate As-Is | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. | Legacy DB editor; retire/replace with controlled admin tooling. | `AssemblyInfo.cs` |
| Core Component | `(root)` | `Form1` | Do Not Migrate As-Is | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. | Legacy DB editor; retire/replace with controlled admin tooling. | `Form1.cs` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Configuration/Resource | `(root)` | `Form1` | Do Not Migrate As-Is | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. | Legacy DB editor; retire/replace with controlled admin tooling. | `Form1.resx` |
| Configuration/Resource | `(root)` | `app` | Do Not Migrate As-Is | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. | Legacy DB editor; retire/replace with controlled admin tooling. | `app.config` |
| Configuration/Resource | `(root)` | `nuget` | Do Not Migrate As-Is | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. | Legacy DB editor; retire/replace with controlled admin tooling. | `nuget.config` |
| Configuration/Resource | `(root)` | `packages` | Do Not Migrate As-Is | Retire legacy DB table editor; use governed admin tooling with RBAC/audit. | Legacy DB editor; retire/replace with controlled admin tooling. | `packages.config` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

