# Legacy Migration Tracking

This folder is the single source of truth for legacy migration tracking.

Program status (`2026-04-16`): **In Progress**. Artifact mapping is largely complete, while remaining implementation closure work is tracked in `docs/plans/LEGACY_MIGRATION_PENDING_TASKS_CHECKLIST_PLAN.md`.

## Master Views

- LGC master inventory (project-grouped tracking): [master-inventory-projects.md](./master-inventory-projects.md)
- Canonical master inventory (P-ID card index): [master-inventory-summary.md](./master-inventory-summary.md)
- Solution master inventory (SLN-grouped tracking): [master-inventory-solutions.md](./master-inventory-solutions.md)
- Canonical migration decision matrix: [master-inventory-summary.md#canonical-migration-decision-matrix](./master-inventory-summary.md#canonical-migration-decision-matrix)
- Modern target decisions (.NET 10, free-first): [modern-target-decisions.md](./modern-target-decisions.md)

## Detail Cards

- LGC component cards (grouped by solution): [SOLUTION_GROUPS_INDEX.md](./solutions/SOLUTION_GROUPS_INDEX.md)
- Solution cards: [solutions/README.md](./solutions/README.md)
- Canonical `P###` index with direct LGC links: [master-inventory-summary.md](./master-inventory-summary.md)

## ID Conventions

- `LGC-###`: legacy inventory components and per-component implementation tracking.
- `P###`: canonical card set used for architecture-level migration planning.
