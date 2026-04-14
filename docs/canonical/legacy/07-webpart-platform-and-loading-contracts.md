# 07 - WebPart Platform And Loading Contracts

## Platform concept
Web parts are runtime-pluggable content/application units bound to page elements and templates.

Core entities:
- `WPart` (logical module)
- `WebPartControl` (renderable control)
- `WebPartControlTemplate` (presentation template)
- `WebPartAdmin` and `WebPartConfig` (admin/config entry points)

## Runtime contract
- Page elements reference part control templates.
- Loader resolves actual control files from module identity + configured filenames.
- Context is propagated through `WContext`, `WQuery`, and value-provider dictionaries.
- Module controls commonly inherit from `WUserControl` / `WPageControl` interfaces.

## Packaging contract
Module UI payloads are mounted into host content tree using junction scripts:
- `create-junction.cmd` in SystemParts/Integration/BranchLocator maps module bundles into `WebSystem-MVC/Content/Parts/...`
- Shared `Content` and `bin` are also junction-linked for local module build/run

## Why it is critical
This is the primary extension model for business functionality. Page composition, inline edit behavior, and module administration all depend on this contract.

## Key anchors
- `legacy/Portal/WebSystem/WCMS.Framework/PartModel/*.cs`
- `legacy/Portal/WebSystem/WCMS.WebSystem.ViewModels/WUserControl.cs`
- `legacy/Portal/WebSystem/WCMS.Framework/WContext.cs`
- `legacy/Portal/WebParts/*/*/create-junction.cmd`

## Evaluation
Strength: mature plugin-like pattern for runtime module composition.
Risk: file-path/junction assumptions and WebForms control dependencies make migration and isolated module testing difficult.
