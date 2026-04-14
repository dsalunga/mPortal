# 05 - Modern Security Authentication And Session Control

## Purpose
Define the target security model and cutover strategy for identity/session behavior.

## Target Security Model
- Identity: `ASP.NET Core Identity + OpenIddict` as program baseline.
- Session/auth context exposed through scoped services (`IWSession`/`IWContext` style), not static globals.
- Use modern cookie/token flows with explicit lifetime, rotation, and audit events.

## Migration Controls
- Password migration path: staged rehash/backfill with login-time upgrade support.
- Map legacy role/group/object permission semantics to modern authorization handlers/policies.
- Deprecate insecure legacy crypto/session patterns with documented compatibility windows.

## Legacy Account Migration Pattern
- Baseline: rehash-on-login using legacy-hash verification + automatic upgrade to ASP.NET Core Identity hash.
- Default policy: do **not** force reset inactive users by default.
- Optional policy: forced reset campaigns are config-driven and should only be enabled for security incident/compliance needs.
- Cutover guardrail: set a final decommission date for legacy hash verification based on accepted risk posture and migration coverage.

## Hardening Baseline
- Require CSP, CSRF protections, secure headers, and secret vault-backed configuration.
- Enforce server-side HTML sanitization for all rich-text persistence.
- Record auth/session anomalies using structured telemetry and alerting thresholds.

## Legacy Reference
- [Legacy card baseline](../legacy/05-security-authentication-and-session-control.md)
