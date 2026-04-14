# 00 - Modern System Overview

## Purpose
Define the target-state WCMS platform on `.NET 10`, including runtime shape, stack, and migration guardrails.

## Modern Target Architecture
- Platform core runs on `ASP.NET Core 10` with modular app composition under `Portal/WebSystem`.
- Business capabilities remain module-driven (`SystemParts`, `Integration`, `BranchLocator`) but are hosted through modern MVC/Razor + API endpoints.
- Legacy WebForms/ASMX/WCF/ASHX surfaces are replaced via adapter and endpoint cutover, not lift-and-shift.

## Target Tech Stack
- Runtime: `.NET 10`, `ASP.NET Core 10`, `Kestrel`.
- Data: `EF Core 10` + `DbUp` hybrid migration flow, SQL Server/Postgres support lanes as needed.
- Auth: `ASP.NET Core Identity + OpenIddict`.
- Jobs: `Quartz.NET` + Worker Services.
- Editor: `TipTap OSS` + server-side HTML sanitization (`FCKeditorV2` is do-not-migrate).
- Delivery: `GitHub Actions` with deterministic build/test/release lanes.

## Migration Guardrails
- No migration work should port `FredCK.FCKeditorV2` internals; all editor usage converges to TipTap.
- Prefer strangler-by-capability for high-risk areas (routing, auth/session, services, module rendering).
- Use legacy cards as behavior baseline and modern cards as target contract.

## Confirmed Decisions
- Module manifest contract: `Hybrid DB + code manifest` (DB keeps dynamic composition metadata; code manifests provide compile-time validation and startup registration).
- Admin modernization strategy: `Hybrid per domain` (Razor-first core CMS/admin; API + SPA for high-interactivity domains).
- Cutover mode: `Staged strangler` by bounded capability with reversible milestones.

## Legacy Reference
- [Legacy card baseline](../legacy/00-legacy-system-overview.md)
