# 01 - Modern Solution Topology And Project Map

## Purpose
Map legacy solution sprawl into a maintainable `.NET 10` topology with clear migration lanes and ownership.

## Target Topology
- Core libraries centralized around `Core/` and modernized shared framework projects.
- Portal host and module packs aligned to solution-grouped domains under `Portal/`.
- Standalone apps (`BibleReader`, `LessonReviewer`) continue as separate deployable hosts with shared contracts.

## Migration Lanes
- Lane A: Core runtime and framework contracts (`WContext`, providers, registry abstractions).
- Lane B: Host/runtime pipeline (routing, CMS page rendering, auth/session, admin).
- Lane C: Module packs and feature parity closure.
- Lane D: Utilities/ops tooling modernization and release hardening.

## Structure Rules
- Keep one canonical card pair per scope item: `legacy` for baseline, `modern` for target-state.
- Preserve solution-level tracking IDs for cross-linking with migration-tracking docs.
- Shared projects must be explicitly marked and minimized.

## Legacy Reference
- [Legacy card baseline](../legacy/01-solution-topology-and-project-map.md)
