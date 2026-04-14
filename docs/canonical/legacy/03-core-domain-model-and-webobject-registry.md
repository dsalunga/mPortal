# 03 - Core Domain Model And WebObject Registry

## Critical concept
`WebObject` is the metadata spine of the platform. It links logical object ids/names to:
- concrete CLR model type (`TypeName`)
- concrete data provider type (`DataProviderName`)
- concrete manager type (`ManagerName`)
- cache strategy and record-tracking metadata

## Metadata source
- Runtime seed data: `legacy/Portal/Binaries/WebObject.xml`
- Observed registrations: 137 object entries.

## Domain families represented
- Site model: `WSite`, `WPage`, `WebMasterPage`, `WebTemplate`, `WebTemplatePanel`, etc.
- Security model: users, groups, permissions, object security, policies.
- Part model: web parts, part controls, part templates, admin/config models.
- Operational model: jobs, logs, queue, registry, resource headers, parameters.
- Module model: article, content/menu/social/incident/integration-linked entities.

## Runtime mechanics
- `WebObject` static bootstrap loads object list and resolves managers/providers via reflection.
- Domain classes typically call `WebObject.ResolveManager<T, TProvider>(...)` in static constructors.
- Cache behavior and manager wiring are driven by `WebObject` metadata and registry flags.

## Key anchors
- `legacy/Portal/WebSystem/WCMS.Framework/Core/WebObject.cs`
- `legacy/Portal/Binaries/WebObject.xml`
- `legacy/Portal/WebSystem/WCMS.Framework/Core/ObjectManager.cs`

## Evaluation
Strength: strongly extensible metadata-first design.
Risk: reflection failures and bad metadata can break runtime binding globally; static initialization order is a systemic fragility point.
