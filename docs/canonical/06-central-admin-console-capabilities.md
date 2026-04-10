# 06 - Central Admin Console Capabilities

## What it is
The Central console is the operational control plane for CMS administration, exposed under `Content/Parts/Central` and loaded dynamically by the `CentralLoader` page.

## Functional surface
Major areas observed in `Content/Parts/Central`:
- Site/page/template management (`WebSites`, `Template`, `WebPart` folders).
- Security management (users, groups, roles, object permissions, policies).
- Runtime tools (registry editor, query analyzer, data explorer, logs, queue manager, sessions).
- Agent management (job dashboard/editor/view).
- Misc admin entities (offices, addresses, parameter sets/resources).

## Loading mechanics
- `Default.aspx.cs` (`CentralLoader`) validates admin/session rights.
- Target control is selected from query (`PageId`, `PageElementId`, `PartAdminId`, `Load`).
- Control path resolves to `~/Content/Parts/{PartIdentity}/{ControlFile}`.
- If control implements `IUpdatable`, update/cancel workflow is wired by shell page.

## Why it is critical
Most live data governance happens here: content structure, access control, job scheduling, registry configuration, and troubleshooting tools are centralized in this surface.

## Key anchors
- `legacy/Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/Default.aspx.cs`
- `legacy/Portal/WebSystem/WebSystem-MVC/Content/Parts/Central/*`
- `legacy/Portal/WebSystem/WCMS.Framework/Utilities/WHelper.cs`

## Evaluation
Strength: broad operational coverage in one integrated console.
Risk: very large WebForms surface area with high coupling to query-string context and dynamic control loading, which increases maintenance and regression cost.
