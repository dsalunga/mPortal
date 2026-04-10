# 02 - Runtime Request Pipeline And Rendering

## Core runtime entry
Primary host: `legacy/Portal/WebSystem/WebSystem-MVC`.

Key start path:
- `Global.asax.cs` initializes cache and optional agent autostart.
- `Application_BeginRequest` rewrites CMS URLs to runtime loaders.
- Dynamic requests are rewritten toward `~/Default.aspx` with internal page id (`___pid`).

## Page resolution flow
1. Incoming URL is normalized.
2. `WebRewriter.ResolvePageLowered(...)` maps host/path to site/page.
3. If page exists, query is rewritten with page id and target base path (`Default.aspx` or `Default.cshtml` depending on template engine).
4. `Default.aspx.cs` (`DefaultViewController`) composes template hierarchy and page/master elements.

## Rendering model
- Template controls are loaded from `~/Content/Themes/{Theme}/{File}`.
- Page elements map to web part controls and are injected into template panels.
- Runtime can toggle design mode overlays for authorized users.
- Resource headers (CSS/JS/meta fragments) are aggregated from theme/template/site/page scopes.

## Notable behavior
- Explicit reflection hack in `Application_Start` disables root file-monitor subdir behavior to reduce AppDomain restarts.
- Rewriter supports short URLs and host-based site identity lookup.
- Non-CMS paths (`/content/`, `/api/`, `/u/`, known constants) bypass rewrite.

## Key anchors
- `legacy/Portal/WebSystem/WebSystem-MVC/Global.asax.cs`
- `legacy/Portal/WebSystem/WCMS.Framework/WebRewriter.cs`
- `legacy/Portal/WebSystem/WebSystem-MVC/Default.aspx.cs`
- `legacy/Portal/WebSystem/WCMS.Framework/SiteModel/WebPage.cs`

## Evaluation
This pipeline is feature-rich and flexible, but hard to reason about due to global/static state, request-time reflection, and deep cross-object lookups at runtime.
