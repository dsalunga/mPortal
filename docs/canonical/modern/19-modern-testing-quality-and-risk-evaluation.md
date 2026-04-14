# 19 - Modern Testing Quality And Risk Evaluation

## Purpose
Set verification and risk controls to support confident migration and cutover.

## Quality Strategy
- Adopt layered tests: unit, integration, module parity, and end-to-end cutover checks.
- Track migration parity using explicit acceptance checklists per canonical card/module.
- Embed performance/security regression checks into CI pipelines.

## Top Migration Risks
- Dynamic CMS rendering and module binding drift from legacy behavior.
- Auth/session/permission parity gaps during staged cutover.
- Data/schema migration issues for legacy procedural paths.
- Operational instability if job and deployment modernization lags.

## Release Readiness Gates
- All blocker decisions closed for the release phase.
- Critical user journeys validated in staging with representative data.
- Observability, rollback runbooks, and ownership escalation paths confirmed.
- Staged strangler cutover checkpoints signed off per capability (routing/rendering/admin/module domains) before expanding rollout scope.

## Recommended SLO Baselines By Environment

| Environment | Availability | Error Rate | Latency | Job Success | Gate Type |
| --- | --- | --- | --- | --- | --- |
| Dev | Track-only | Track-only | Track-only | Track-only | No hard gate |
| QA | N/A (functional lane) | `5xx < 2%` | `P95 < 1000ms` (critical paths) | `>= 97%` | Advisory gate |
| UAT/Staging | `>= 99.5%` (soak) | `5xx < 1%` | `P95 < 500ms` core, `P95 < 1200ms` heavy admin | `>= 99%` | Cutover gate |
| Production (stabilized target) | `>= 99.9%` | `5xx < 0.5%` | Same as staging or stricter per domain | `>= 99%` | Operational SLO |

## Legacy Reference
- [Legacy card baseline](../legacy/19-testing-quality-and-risk-evaluation.md)
