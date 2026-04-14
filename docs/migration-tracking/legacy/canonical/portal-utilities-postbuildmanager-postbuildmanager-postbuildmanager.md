# P010 - PostBuildManager

## Project Tracking Summary

| Field | Value |
|---|---|
| Project Path | `legacy/Portal/Utilities/PostBuildManager/PostBuildManager/PostBuildManager.csproj` |
| Project Kind | Utility Project |
| Assembly Name | `PostBuildManager` |
| Target Framework | `v4.7` |
| Output Type | `Exe` |
| Migration Status | Blocked |
| Status Basis | Depends on legacy post-build/release mechanics; migrate only after CI/CD redesign. |
| Target Alternative | Replace with CI/CD packaging/publish pipeline and artifact promotion. |
| Tracking Owner | `TBD` |
| Target Milestone | `TBD` |

## Surface Coverage Snapshot

| Surface | Count |
|---|---:|
| Application Entry Point | 1 |
| Configuration/Resource | 1 |
| Assembly Metadata | 1 |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Application Entry Point | `(root)` | `Program` | Blocked | Replace with CI/CD packaging/publish pipeline and artifact promotion. | Blocked pending modern build/release pipeline. | `Program.cs` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | Blocked | Replace with CI/CD packaging/publish pipeline and artifact promotion. | Blocked pending modern build/release pipeline. | `Properties/AssemblyInfo.cs` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Configuration/Resource | `(root)` | `app` | Blocked | Replace with CI/CD packaging/publish pipeline and artifact promotion. | Blocked pending modern build/release pipeline. | `app.config` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

