# 09 - Modern SystemParts G2 Modules

## Purpose
Align G2 modules to modern contracts with consistent rendering and service patterns.

## Modernization Direction
- Standardize module controller/service boundaries and event flows.
- Adopt shared UI component patterns for social/forum/newsletter surfaces.
- Apply consistent observability, validation, and authorization policies.

## Execution Strategy
- Migrate module-by-module with compatibility adapters where cross-module coupling exists.
- Use feature toggles to gate high-risk transitions.
- Track unresolved dependencies in migration-tracking cards with explicit owners.

## Quality Gates
- Module-level integration tests.
- Data migration verification for user-generated content paths.
- No legacy editor/runtime dependency leakage.

## Legacy Reference
- [Legacy card baseline](../legacy/09-systemparts-g2-modules.md)
