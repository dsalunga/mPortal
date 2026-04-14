# 08 - Modern SystemParts G1 Modules

## Purpose
Modernize G1 foundational modules with standardized component contracts and APIs.

## Target State
- Each G1 module mapped to modern ViewComponent/API surface with explicit inputs/outputs.
- Shared concerns (security, caching, localization, rendering) centralized in framework services.
- Legacy per-module utility duplication reduced via reusable abstractions.

## Migration Priorities
- Prioritize modules that affect core page composition and admin productivity.
- Convert data access to EF Core/data services with test coverage at module boundaries.
- Retire legacy UI assets once parity sign-off is complete.

## Acceptance Gates
- Behavior parity checklist per module.
- Performance and authorization checks for public/admin actions.
- No direct dependency on WebForms runtime artifacts.

## Legacy Reference
- [Legacy card baseline](../legacy/08-systemparts-g1-modules.md)
