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
| Target Alternative | Replace manual desktop deploy flow with automated release/deploy pipeline. |
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

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Generated UI Partial | `(root)` | `FormMain.Designer` | Do Not Migrate As-Is | Replace manual desktop deploy flow with automated release/deploy pipeline. | Manual deploy utility; replace with CI/CD automation. | `FormMain.Designer.cs` |
| Generated UI Partial | `Properties` | `Resources.Designer` | Do Not Migrate As-Is | Replace manual desktop deploy flow with automated release/deploy pipeline. | Manual deploy utility; replace with CI/CD automation. | `Properties/Resources.Designer.cs` |
| Generated UI Partial | `Properties` | `Settings.Designer` | Do Not Migrate As-Is | Replace manual desktop deploy flow with automated release/deploy pipeline. | Manual deploy utility; replace with CI/CD automation. | `Properties/Settings.Designer.cs` |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Core Component | `(root)` | `FormMain` | Do Not Migrate As-Is | Replace manual desktop deploy flow with automated release/deploy pipeline. | Manual deploy utility; replace with CI/CD automation. | `FormMain.cs` |
| Application Entry Point | `(root)` | `Program` | Do Not Migrate As-Is | Replace manual desktop deploy flow with automated release/deploy pipeline. | Manual deploy utility; replace with CI/CD automation. | `Program.cs` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | Do Not Migrate As-Is | Replace manual desktop deploy flow with automated release/deploy pipeline. | Manual deploy utility; replace with CI/CD automation. | `Properties/AssemblyInfo.cs` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Configuration/Resource | `(root)` | `App` | Do Not Migrate As-Is | Replace manual desktop deploy flow with automated release/deploy pipeline. | Manual deploy utility; replace with CI/CD automation. | `App.config` |
| Configuration/Resource | `(root)` | `FormMain` | Do Not Migrate As-Is | Replace manual desktop deploy flow with automated release/deploy pipeline. | Manual deploy utility; replace with CI/CD automation. | `FormMain.resx` |
| Configuration/Resource | `Properties` | `Resources` | Do Not Migrate As-Is | Replace manual desktop deploy flow with automated release/deploy pipeline. | Manual deploy utility; replace with CI/CD automation. | `Properties/Resources.resx` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

