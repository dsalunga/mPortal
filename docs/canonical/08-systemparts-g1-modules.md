# 08 - SystemParts G1 Modules

## Scope
`legacy/Portal/WebParts/SystemParts` (base module pack).

## Main module domains
- Content primitives: content blocks, blog/feed variants, static renderer.
- Article system: article entities, templates, publication and subscription admin.
- Contact system: contact cards and inquiry workflows.
- Event calendar: categories, events, recurrence, reminder sender task.
- File manager: folders/files/version metadata and remote index integration views.
- Menu/navigation: hierarchical menu entities and linked menu models.
- Generic list/survey flows: configurable list/survey UI and data structures.
- Remote indexer: remote library/item models and scheduled indexing task.
- Weekly scheduler: scheduler tasks/presenter.

## UI bundle
`SystemParts/SystemParts/AppBundle` contains a large set of `ascx` admin/render controls for these features.

## Data characteristics
Many modules map to dedicated SQL tables/procedures (article, calendar, file identity/version, generic list, contact, remote indexer, etc.) under `Portal/Binaries/Database`.

## Maturity signals
- Stronger implementations: article, calendar, remote indexer, file manager flows.
- Weaker/partial areas: several GenericList provider paths are still placeholder (`NotImplementedException`).

## Key anchors
- `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.Article/*`
- `legacy/Portal/WebParts/SystemParts/WCMS.Framework.WebParts.LocalCalendar/*`
- `legacy/Portal/WebParts/SystemParts/WCMS.Framework.FileManager/*`
- `legacy/Portal/WebParts/SystemParts/WCMS.WebSystem.WebParts.RemoteIndexer/*`
- `legacy/Portal/WebParts/SystemParts/SystemParts/AppBundle/*`
