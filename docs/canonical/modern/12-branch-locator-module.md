# 12 - Modern Branch Locator Module

## Purpose
Define the modern target for branch discovery/search/map capabilities.

## Target Implementation
- Modern web host + API-backed branch data retrieval.
- Map/search UI rendered with modern components and cached query services.
- Admin edit/update operations gated by RBAC and audit logging.

## Migration Path
- Preserve existing search semantics and location data integrity.
- Replace legacy control rendering with ViewComponents/partials and endpoint handlers.
- Add data quality checks for geospatial/address fields during cutover.

## Quality Metrics
- Search correctness and response-time thresholds.
- Map/load reliability under concurrent access.
- Parity coverage for key branch management workflows.

## Legacy Reference
- [Legacy card baseline](../legacy/12-branch-locator-module.md)
