# 10 - SystemParts G3 Modules

## Scope
`legacy/Portal/WebParts/SystemPartsG3` (third-generation module pack).

## Main module domains
- Incident management:
  - tickets, categories, types, instances, ticket history
  - admin UI controls for category/type/instance/ticket management
- Jobs board:
  - basic job/job-result entities
  - search/listing UI controls

## Observed maturity
- Incident module is comparatively complete in model/provider/manager shape.
- Jobs module has notable gaps (`Job.Update/Delete` and provider object-id behavior left unimplemented in places).

## Why it matters
G3 contains operational workflow features (incident ticketing) used for service/support style use cases and carries direct business process value.

## Key anchors
- `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Incident/*`
- `legacy/Portal/WebParts/SystemPartsG3/WCMS.WebSystem.WebParts.Jobs/*`
- `legacy/Portal/WebParts/SystemPartsG3/SystemPartsG3/AppBundle3/*`

## Evaluation
Incident capability is a strong domain candidate for preservation/migration first. Jobs submodule should be treated as partially complete legacy code and assessed before production-critical use.
