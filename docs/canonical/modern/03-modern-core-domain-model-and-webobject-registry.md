# 03 - Modern Core Domain Model And WebObject Registry

## Purpose
Preserve the CMS domain contract while modernizing persistence, validation, and service boundaries.

## Domain Preservation
- Keep core entities (`WSite`, `WPage`, `WPart`, templates, permissions, registry) as first-class model contracts.
- Define anti-corruption layer where legacy semantics are ambiguous or side-effect heavy.
- Introduce explicit DTO/application models for external APIs and admin workflows.

## Registry Modernization
- Expose registry access through typed services/options (`IOptionsMonitor` + provider abstraction).
- Support runtime refresh with controlled invalidation instead of broad static/global state.
- Audit and classify security-sensitive keys for secrets vault migration.

## Implementation Direction
- Use EF Core mappings for stable read/write aggregates.
- Retain SQL-based operations via well-scoped data service interfaces where needed.
- Add validation rules and invariants at service layer boundaries.

## Legacy Reference
- [Legacy card baseline](../legacy/03-core-domain-model-and-webobject-registry.md)
