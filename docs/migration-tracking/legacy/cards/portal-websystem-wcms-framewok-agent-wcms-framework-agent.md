# P042 - WCMS.Framework.Agent

## Project Tracking Summary

| Field | Value |
|---|---|
| Project Path | `legacy/Portal/WebSystem/WCMS.Framewok.Agent/WCMS.Framework.Agent.csproj` |
| Project Kind | Console Agent App |
| Assembly Name | `WCMS.Framework.Agent` |
| Target Framework | `v4.8` |
| Output Type | `Exe` |
| Migration Status | Blocked |
| Status Basis | Thread-abort scheduler model cannot be migrated directly; requires durable job orchestration redesign. |
| Target Alternative | Rebuild as durable scheduler/job worker (Hangfire/Quartz/.NET Worker) with retries/idempotency. |
| Tracking Owner | `TBD` |
| Target Milestone | `TBD` |

## Surface Coverage Snapshot

| Surface | Count |
|---|---:|
| Application Entry Point | 1 |
| Configuration/Resource | 1 |
| Assembly Metadata | 1 |

## Core Components And Utilities

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Application Entry Point | `(root)` | `Program` | `Program.cs` | Blocked | Rebuild as durable scheduler/job worker (Hangfire/Quartz/.NET Worker) with retries/idempotency. | Blocked: replace thread-abort scheduler with durable jobs. |
| Assembly Metadata | `Properties` | `AssemblyInfo` | `Properties/AssemblyInfo.cs` | Blocked | Rebuild as durable scheduler/job worker (Hangfire/Quartz/.NET Worker) with retries/idempotency. | Blocked: replace thread-abort scheduler with durable jobs. |

## Database And Automation Assets

| Component Type | Feature/Area | Functionality | Source File | Migration Status | Target Alternative | Tracking Notes |
|---|---|---|---|---|---|---|
| Configuration/Resource | `(root)` | `App` | `App.config` | Blocked | Rebuild as durable scheduler/job worker (Hangfire/Quartz/.NET Worker) with retries/idempotency. | Blocked: replace thread-abort scheduler with durable jobs. |

## Migration Action Items

| Action | Priority | Status | Notes |
|---|---|---|---|
| Confirm owner and target architecture for this artifact | High | Not Stated | `TBD` |
| Validate feature-level parity against source inventory tables above | High | Not Stated | `TBD` |
| Update row-level statuses as migration work progresses | Medium | Not Stated | `TBD` |

