# 16 - Modern Standalone App BibleReader

## Purpose
Set the modernization direction for BibleReader as a clean standalone app on `.NET 10`.

## Target Shape
- BibleReader host remains standalone with clear boundary to shared platform services.
- Use modern routing, API endpoints, and componentized UI where applicable.
- Preserve domain semantics while removing legacy WebForms-era dependencies.

## Migration Priorities
- Auth/session alignment with platform security standards.
- Provider/data access modernization with test coverage.
- Operational readiness: logs, health checks, deployment profile parity.

## Success Criteria
- Feature parity for core reading/search/user workflows.
- Stable integration points with shared services.
- No blocked runtime dependencies on legacy hosting stack.

## Legacy Reference
- [Legacy card baseline](../legacy/16-standalone-app-biblereader.md)
