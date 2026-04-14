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
| Target Alternative | Replace Windows service host with modern background worker orchestration and observability. |
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

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Generated UI Partial | `(root)` | `FrameworkAgentService.Designer` | Blocked | Replace Windows service host with modern background worker orchestration and observability. | Blocked pending agent runtime redesign. | `FrameworkAgentService.Designer.cs` |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Core Component | `(root)` | `FrameworkAgentService` | Blocked | Replace with durable job orchestration and safe cancellation semantics. | Legacy thread/service execution model; requires durable job orchestration redesign. | `FrameworkAgentService.cs` |
| Application Entry Point | `(root)` | `Program` | Blocked | Replace Windows service host with modern background worker orchestration and observability. | Blocked pending agent runtime redesign. | `Program.cs` |
| Assembly Metadata | `Properties` | `AssemblyInfo` | Blocked | Replace Windows service host with modern background worker orchestration and observability. | Blocked pending agent runtime redesign. | `Properties/AssemblyInfo.cs` |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Migration Status | Target Alternative | Tracking Notes | Source File |
| --- | --- | --- | --- | --- | --- | --- |
| Configuration/Resource | `(root)` | `App` | Blocked | Replace Windows service host with modern background worker orchestration and observability. | Blocked pending agent runtime redesign. | `App.config` |
| Configuration/Resource | `(root)` | `FrameworkAgentService` | Blocked | Replace Windows service host with modern background worker orchestration and observability. | Blocked pending agent runtime redesign. | `FrameworkAgentService.resx` |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

