# 02 - Modern Runtime Request Pipeline And Rendering

## Purpose
Define how incoming requests resolve to CMS pages/modules in the `.NET 10` host.

## Modern Pipeline
- Use `Program.cs` endpoint routing + middleware chain for auth, tenant/site context, and page resolution.
- Implement CMS fallback endpoint (`MapCmsPages()` style) after explicit API/feature endpoints.
- Render pages using Razor layouts + ViewComponents, replacing `.aspx`/`.ascx` runtime composition.

## CMS Module Loading Mechanism
- Use a `Hybrid DB + code manifest` model:
  DB metadata (`WebObject`/page tables) remains the runtime composition source, while code manifests validate and register allowed module/component mappings at startup.
- Resolve page metadata from DB into panel zones, then bind zone elements only to manifest-approved component keys.
- Map approved keys to registered handlers (ViewComponents/partial renderers) via DI-backed registry.
- Reject/quarantine unresolved or non-manifest module keys with structured logging and migration diagnostics.

## Compatibility Strategy
- Legacy URL and service paths use adapter endpoints during transition.
- Legacy editor connectors are replaced with modern upload APIs integrated with TipTap workflows.
- Performance gates: cache resolved page metadata and component binding plans.
- Apply capability-by-capability strangler cutover for routing/rendering paths (no big-bang host switch).

## Legacy Reference
- [Legacy card baseline](../legacy/02-runtime-request-pipeline-and-rendering.md)
