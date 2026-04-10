# P012 - WebSystemDeployer

## Project Tracking Summary

| Field | Value |
|---|---|
| Project Path | `legacy/Portal/Utilities/WebSystemDeployer/WebSystemDeployer/WebSystemDeployer.csproj` |
| Project Kind | Utility Project |
| Assembly Name | `WebSystemDeployer` |
| Target Framework | `v4.7` |
| Output Type | `WinExe` |
| Migration Status | Do Not Migrate As-Is |
| Status Basis | Manual desktop deploy copier should be replaced with automated deployment workflows. |
| Tracking Owner | `TBD` |
| Target Milestone | `TBD` |

## Surface Coverage Snapshot

| Surface | Count |
|---|---:|
| Application Entry Point | 1 |
| Core Component | 1 |
| Configuration/Resource | 3 |
| Generated UI Partial | 3 |
| Assembly Metadata | 1 |

## User Controls And UI Components

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Tracking Notes |
|---|---|---|---|---|---|
| Generated UI Partial | `(root)` | `FormMain.Designer` | `FormMain.Designer.cs` | Do Not Migrate As-Is | Manual deploy utility; replace with CI/CD automation. |
| Generated UI Partial | `Properties` | `Resources.Designer` | `Properties/Resources.Designer.cs` | Do Not Migrate As-Is | Manual deploy utility; replace with CI/CD automation. |
| Generated UI Partial | `Properties` | `Settings.Designer` | `Properties/Settings.Designer.cs` | Do Not Migrate As-Is | Manual deploy utility; replace with CI/CD automation. |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Tracking Notes |
|---|---|---|---|---|---|
| Core Component | `(root)` | `FormMain` | `FormMain.cs` | Do Not Migrate As-Is | Manual deploy utility; replace with CI/CD automation. |
| Application Entry Point | `(root)` | `Program` | `Program.cs` | Do Not Migrate As-Is | Manual deploy utility; replace with CI/CD automation. |
| Assembly Metadata | `Properties` | `AssemblyInfo` | `Properties/AssemblyInfo.cs` | Do Not Migrate As-Is | Manual deploy utility; replace with CI/CD automation. |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Tracking Notes |
|---|---|---|---|---|---|
| Configuration/Resource | `(root)` | `App` | `App.config` | Do Not Migrate As-Is | Manual deploy utility; replace with CI/CD automation. |
| Configuration/Resource | `(root)` | `FormMain` | `FormMain.resx` | Do Not Migrate As-Is | Manual deploy utility; replace with CI/CD automation. |
| Configuration/Resource | `Properties` | `Resources` | `Properties/Resources.resx` | Do Not Migrate As-Is | Manual deploy utility; replace with CI/CD automation. |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

