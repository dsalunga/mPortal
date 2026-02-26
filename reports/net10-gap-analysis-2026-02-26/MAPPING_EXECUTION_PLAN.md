# Legacy-to-Modern Mapping Execution Plan

Date: 2026-02-26

## Outputs

- Mapping CSV: `legacy-to-modern-mapping.csv`
- Input source: `legacy-unmatched-web-artifacts.txt`
- Prioritized execution subset: `top30-p0-p1-prioritized.csv`
- Ranked backlog view: `TOP30_EXECUTION_BACKLOG.md`

## Mapping status summary

- Total legacy artifacts mapped: **187**
- `Mapped-Direct`: **5**
- `Mapped-Probable`: **10**
- `Needs-Manual`: **164**
- `Retired-By-Design`: **8**

## Priority summary

- P0: **15**
- P1: **141**
- P2: **23**
- P3: **8**

## High-focus modules (by unresolved items)

- `Portal/WebParts/SystemParts/SystemParts`: **73** unresolved (`Needs-Manual`)
- `Portal/WebSystem/WebSystem-MVC`: **54** unresolved (`Needs-Manual`)
- `Portal/WebParts/SystemPartsG2/SystemPartsG2`: **30** unresolved (`Needs-Manual`)
- `BibleReader/BibleReader`: **2** unresolved (`Needs-Manual`)
- `Portal/WebParts/BranchLocator/WCMS.WebSystem.Apps.BranchLocator.WebApp`: **2** unresolved (`Needs-Manual`)
- `Portal/WebParts/Integration/IntegrationParts`: **2** unresolved (`Needs-Manual`)
- `LessonReviewer/LessonReviewer`: **1** unresolved (`Needs-Manual`)

## Execution checklist

1. [ ] Resolve all `P0` rows first (legacy `.asmx/.ashx/.svc` contract parity).
2. [ ] Resolve `P1` rows under `SystemParts/AppBundle` and `SystemPartsG2/AppBundle2`.
3. [ ] For each `Mapped-Probable`, confirm route + request/response + auth parity, then upgrade status to `Migrated` or `Missing` in the CSV.
4. [ ] For each `Needs-Manual`, decide one: `Implement`, `Retire-Approved`, `Defer` (with owner/date).
5. [ ] Add integration tests for every resolved `P0` item.
6. [ ] Re-run parity smoke tests against populated PostgreSQL data.

## CSV status semantics

- `Mapped-Direct`: deterministic replacement found.
- `Mapped-Probable`: heuristic candidate(s) found; requires manual validation.
- `Needs-Manual`: no reliable replacement candidate found.
- `Retired-By-Design`: intentionally removed and replaced at platform level.

## Notes

- This mapping is deliberately conservative: uncertain mappings remain `Mapped-Probable`/`Needs-Manual` to avoid false parity claims.
- Update this CSV as the system-of-record for migration closure tracking.
