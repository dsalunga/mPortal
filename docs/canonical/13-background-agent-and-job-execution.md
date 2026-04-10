# 13 - Background Agent And Job Execution

## Scope
`legacy/Portal/WebSystem/WCMS.Framewok.Agent`, `WCMS.Framework.AgentService`, and `WCMS.Framework/Agent`.

## Runtime model
- `WCMS.Framewok.Agent` is a console host that boots `FrameworkAgent`.
- `WCMS.Framework.AgentService` is a Windows Service wrapper that also boots `FrameworkAgent`.
- Job definitions are loaded from `/System/Agent` in `WebRegistry`.
- Scheduler uses `JobTimerInterval` and `JobCacheRefreshInterval` values from registry config.

## Execution contract
- Jobs resolve from `WebJob` metadata (`TypeName`, parameters, schedule, enabled state).
- Each task is instantiated via reflection and must inherit `AgentTaskBase`.
- `AgentTaskBase.StartManagedExecution()` records status/start/end timestamps and execution message.
- Task parameters are copied into `Attributes` key-value pairs before execution.

## Key task examples
- `MessageProcessorTask`: drains `WebMessageQueue` and sends email/SMS.
- `EmailUpdaterTask`: batch-updates user email values from XML source data.
- `SenderTask`: templated bulk email sender with configurable pacing.

## Operational characteristics
- Logging is per task (`<LogPath>/<TaskName>.txt`) plus scheduler log (`Agent.txt`).
- Supports single-task forced execution flags (`/task:<name>`, `/force`, `/interactive`).
- Threads are explicit `Thread` instances with STA apartment state.

## Evaluation
Strengths:
- Flexible metadata-driven scheduler and reusable task base contract.
- Clear operational hooks for message queue and maintenance jobs.

Risks:
- Uses `Thread.Abort()` for shutdown paths (unsafe cancellation behavior).
- No built-in retry policy, backoff, or dead-letter workflow for failed jobs.
- Reflection- and config-heavy startup increases runtime failure surface.

## Key anchors
- `legacy/Portal/WebSystem/WCMS.Framewok.Agent/Program.cs`
- `legacy/Portal/WebSystem/WCMS.Framework/Agent/FrameworkAgent.cs`
- `legacy/Portal/WebSystem/WCMS.Framework/Agent/AgentTaskBase.cs`
- `legacy/Portal/WebSystem/WCMS.Framework/Net/MessageProcessorTask.cs`
- `legacy/Portal/WebSystem/WCMS.Framework.AgentService/FrameworkAgentService.cs`

