# 07 - Modern WebPart Platform And Loading Contracts

## Purpose
Define the modern module composition contract replacing legacy control-path loading.

## Target Module Contract
- Confirmed pattern: `Hybrid DB + code manifest`.
- Modules expose render units as registered component keys (ViewComponents/renderer services) via code manifests.
- Page element metadata in DB references component keys and typed configuration payloads, validated against manifest contracts.
- Module lifecycle includes registration, validation, health checks, and deprecation metadata.

## CMS Module Loading Mechanism (Modern)
- At startup: load code manifests, register component resolvers through DI, and build an allow-list of valid module/component keys.
- At request time: resolve page zones from DB -> module elements -> manifest-approved component handlers.
- On failure: fallback renderer + structured error telemetry + migration tracking hook.

## Packaging And Deployment
- Eliminate junction-based module composition in favor of build artifacts and package references.
- Use deterministic CI outputs for host + module deployment units.
- Keep backward-compat adapters only where blocking dependencies remain.
- Roll out module binding changes in staged strangler increments by capability/domain.

## Legacy Reference
- [Legacy card baseline](../legacy/07-webpart-platform-and-loading-contracts.md)
