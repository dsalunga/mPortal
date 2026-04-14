# 06 - Modern Central Admin Console Capabilities

## Purpose
Describe how admin capabilities migrate from WebForms controls to modern UI and APIs.

## Admin Surface Direction
- Adopt a `Hybrid per domain` admin strategy:
  Razor-first for core CMS/admin management surfaces, with API + SPA for high-interactivity workflows.
- Prioritize high-value domains: site/page/template management, security, content operations, diagnostics.
- Replace WebForms stateful control behavior with explicit request/response and component state models.

## Recommended Domain Split

| Domain | UI Mode | Why |
| --- | --- | --- |
| Site/Page/Template admin | Razor-first | CRUD-heavy workflows, fastest migration path. |
| User/Role/Permission admin | Razor-first | Security-sensitive and governance-oriented flows. |
| Content moderation/review queues | Razor-first | Lower UI complexity with strong auditability. |
| Media manager (bulk upload/preview) | API + SPA-first | High interaction UX and client-side state needs. |
| Dashboards/analytics | API + SPA-first | Rich visualization and dynamic filtering needs. |
| Bulk import/export tooling | API + SPA-first | Better async/progress UX for long-running operations. |
| Audit/log browsing | Razor-first (API-backed) | Quick delivery with optional API reuse. |

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
