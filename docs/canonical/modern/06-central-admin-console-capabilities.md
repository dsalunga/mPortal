# 06 - Modern Central Admin Console Capabilities

## Purpose
Describe how admin capabilities migrate from WebForms controls to modern UI and APIs.

## Admin Surface Direction
- Adopt a `Hybrid per domain` admin strategy:
  Razor-first for core CMS/admin management surfaces, with API + SPA for high-interactivity workflows.
- Prioritize high-value domains: site/page/template management, security, content operations, diagnostics.
- Replace WebForms stateful control behavior with explicit request/response and component state models.

## Editor And Content Operations
- Standardize rich-text editing on TipTap OSS with shared sanitization and content policy services.
- Replace legacy file-browser/editor connector paths with secure upload/media APIs.
- Track content conversion quality and sanitize legacy HTML on read/write boundaries.

## Governance
- Require RBAC + audit logs on all admin mutations.
- Add feature flags for staged release of migrated admin features.
- Maintain side-by-side parity checklists before retiring legacy admin endpoints.
- Execute admin migration in staged strangler slices by domain (not a single all-admin cutover).

## Legacy Reference
- [Legacy card baseline](../legacy/06-central-admin-console-capabilities.md)
