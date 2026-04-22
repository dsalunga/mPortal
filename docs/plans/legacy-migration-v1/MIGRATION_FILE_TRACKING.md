# Legacy -> Modern File Migration Tracking

This file is a concise snapshot for `.ascx` migration coverage.
The authoritative per-file source of truth is:
- `docs/plans/legacy-migration/inventory/legacy-source-tracking-portal.csv`
- `docs/plans/legacy-migration/inventory/legacy-source-tracking-all.csv`

Snapshot date: `2026-04-16` (tracker reconciliation pass 1)

## Totals (`.ascx` only)

- Total `.ascx` files: **519**
- Completed (mapped to modern counterpart): **507**
- Not Applicable (pending explicit closure/replacement rationale): **12**

## Summary by Portal Submodule

| Submodule | Completed | Not Applicable | Total |
|---|---:|---:|---:|
| `Portal/Binaries/Externals` | 0 | 1 | 1 |
| `Portal/WebParts/BranchLocator` | 4 | 0 | 4 |
| `Portal/WebParts/Integration` | 127 | 0 | 127 |
| `Portal/WebParts/SDKTest` | 0 | 1 | 1 |
| `Portal/WebParts/SystemParts` | 136 | 0 | 136 |
| `Portal/WebParts/SystemPartsG2` | 52 | 0 | 52 |
| `Portal/WebParts/SystemPartsG3` | 10 | 0 | 10 |
| `Portal/WebSystem/WebSystem-MVC` | 178 | 10 | 188 |
| **TOTAL** | **507** | **12** | **519** |

## Remaining Not Applicable `.ascx`

Use the explicit unresolved list:
- `docs/plans/legacy-migration/inventory/unresolved-ascx-not-applicable.txt`

## Implementation Closure Plan

For remaining implementation tasks and closure criteria, use:
- `docs/plans/LEGACY_MIGRATION_PENDING_TASKS_CHECKLIST_PLAN.md`

## Quick Audit Commands

```bash
# Count .ascx statuses from portal tracker
python3 - <<'PY'
import csv
from collections import Counter
rows=list(csv.DictReader(open('docs/plans/legacy-migration/inventory/legacy-source-tracking-portal.csv', newline='')))
ascx=[r for r in rows if r['file_type']=='.ascx']
print(Counter(r['status'] for r in ascx))
PY
```

```bash
# Show unresolved .ascx paths
cat docs/plans/legacy-migration/inventory/unresolved-ascx-not-applicable.txt
```
