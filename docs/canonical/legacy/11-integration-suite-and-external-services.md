# 11 - Integration Suite And External Services

## Scope
`legacy/Portal/WebParts/Integration` plus connected handler/service endpoints.

## Functional domains
- Member profile/account linkage (`MemberLink`, membership sync and status updates).
- External attendance/service integrations via generated SOAP clients.
- Music competition models and scoring entities.
- Registration and profile update workflows.
- Auxiliary task jobs (profile sync, migration, reminders, registration processing).
- Embedded BibleReader access tracking entities.

## Service surface
Integration web app exposes mixed endpoints:
- ASMX (`External.asmx`, `Member.asmx`, Bible service wrappers)
- WCF (`Member.svc`, `Music.svc`, registration DataSync)
- Handlers (`MakeUp.ashx`, print/stream utilities)

## Runtime coupling
- Depends on core account/session/security helpers from WCMS framework.
- Uses external SOAP services for member/attendance/common data.
- Writes outbound messages via `WebMessageQueue` and can trigger agent processing.

## Key anchors
- `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/*`
- `legacy/Portal/WebParts/Integration/WCMS.WebSystem.Apps.Integration/Task/*`
- `legacy/Portal/WebParts/Integration/IntegrationParts/Apps/Integration/*`

## Evaluation
High business value but high integration risk: many external contracts, mixed protocols, and strong dependency on legacy auth/account flows.
