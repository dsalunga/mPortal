# P043 - WCMS.Framework.AgentService

## Project Tracking Summary

| Field | Value |
|---|---|
| Project Path | `legacy/Portal/WebSystem/WCMS.Framework.AgentService/WCMS.Framework.AgentService.csproj` |
| Project Kind | Service/Agent App |
| Assembly Name | `WCMS.Framework.AgentService` |
| Target Framework | `v4.8` |
| Output Type | `WinExe` |
| Migration Status | Blocked |
| Status Basis | Service host is coupled to legacy agent runtime and needs architectural redesign. |
| Tracking Owner | `TBD` |
| Target Milestone | `TBD` |

## Surface Coverage Snapshot

| Surface | Count |
|---|---:|
| Application Entry Point | 1 |
| Core Component | 1 |
| Configuration/Resource | 2 |
| Generated UI Partial | 1 |
| Assembly Metadata | 1 |

## User Controls And UI Components

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Tracking Notes |
|---|---|---|---|---|---|
| Generated UI Partial | `(root)` | `FrameworkAgentService.Designer` | `FrameworkAgentService.Designer.cs` | Blocked | Blocked pending agent runtime redesign. |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Tracking Notes |
|---|---|---|---|---|---|
| Core Component | `(root)` | `FrameworkAgentService` | `FrameworkAgentService.cs` | Blocked | Legacy thread/service execution model; requires durable job orchestration redesign. |
| Application Entry Point | `(root)` | `Program` | `Program.cs` | Blocked | Blocked pending agent runtime redesign. |
| Assembly Metadata | `Properties` | `AssemblyInfo` | `Properties/AssemblyInfo.cs` | Blocked | Blocked pending agent runtime redesign. |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Tracking Notes |
|---|---|---|---|---|---|
| Configuration/Resource | `(root)` | `App` | `App.config` | Blocked | Blocked pending agent runtime redesign. |
| Configuration/Resource | `(root)` | `FrameworkAgentService` | `FrameworkAgentService.resx` | Blocked | Blocked pending agent runtime redesign. |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

