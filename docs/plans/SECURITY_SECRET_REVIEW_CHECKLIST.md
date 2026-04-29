# Security/Data Exposure Review Checklist

Status: in progress (current-branch remediation completed 2026-04-09; secret rotation/history rewrite/sign-off still pending)

## Pending Classification (2026-04-29)

| Category | Items | Notes |
|---|---|---|
| External/manual owner action pending | Sections 1, 2, and 8 unchecked items | Credential/key rotation, session invalidation, temporary access restriction, and formal sign-offs require security/ops owner execution. |
| Approval-gated execution pending | Section 5 unchecked items | History rewrite cutover depends on owner-approved freeze window and coordinated consumer communication. |
| Review/decision pending | Section 4 + Section 9 vendor-review items | Third-party/vendor bundle review needs explicit keep/sanitize/remove decisions. |
| Verification run pending | Section 6 full-history scan | Full-history secret scan must run after approved history rewrite execution. |

## 1) Immediate Containment
- [x] Treat the exposed values in this repo as compromised.
- [ ] Restrict repo access (temporary) while cleanup is in progress.
- [x] Stop using any credential currently hardcoded in repository files.

## 2) Credential and Secret Rotation
- [ ] Rotate SMS gateway credential found in `Portal/Binaries/Database/WebRegistry.xml`.
- [ ] Rotate DB credentials used by local/container defaults (`SA_PASSWORD`, `PG_PASSWORD`, any `Password=postgres` fallback).
- [ ] Rotate all app cryptographic constants currently hardcoded in source:
  - `SECRET_KEY`
  - `INIT_VECTOR`
  - `SALT`
- [ ] Invalidate old cookies/tokens/sessions if tied to previous key material.

## 3) Code/Config Remediation (Current Branch)
- [x] Remove plaintext credentials from committed config/data files.
- [x] Replace hardcoded crypto material with environment-backed configuration.
- [x] Remove insecure fallback credentials from runtime code and compose files.
- [x] Ensure dev-only sample values are clearly non-real and non-default.
- [x] Add secure config loading (`env`, secrets manager, or mounted secret files).

## 4) Legacy Snapshot Remediation
- [x] Remove or sanitize duplicate secret-bearing files under `legacy/`.
- [ ] Review large binary/vendor bundles under `legacy/` for proprietary/internal content.
- [ ] Keep only what is required for migration traceability.

## 5) Git History Cleanup
- [ ] Rewrite history to remove exposed secrets from all commits/tags.
- [ ] Force-push rewritten branches and coordinate consumer reset.
- [ ] Revoke old clones/archives as trusted sources.

## 6) Verification
- [x] Run secret scan on HEAD (tracked files) and ensure zero high-risk findings.
- [ ] Run secret scan across full git history and ensure zero high-risk findings.
- [x] Manually verify known paths no longer contain sensitive values.

## 7) Preventive Controls
- [x] Add CI secret scanning (gitleaks/trufflehog).
- [x] Add pre-commit secret scanning hooks.
- [x] Add policy: no plaintext secrets in repo, including legacy snapshots.
- [x] Add documented secure local setup (`.env.example` without real secrets).

## 8) Sign-off
- [ ] Security review complete.
- [ ] Engineering lead sign-off complete.
- [ ] Repo access policy reviewed post-cleanup.

## 9) PII/Contact Data Anonymization (Modern + Legacy)
- [x] Replace direct personal contact emails with dummy addresses in first-party templates/content.
- [x] Replace direct personal phone/contact numbers with dummy values in first-party templates/content.
- [x] Apply replacements in both `Portal/` and `legacy/Portal/` trees.
- [x] Re-scan first-party sources and confirm zero known-personal-identifier hits from the tracked pattern set.
- [ ] Complete secondary review of third-party/vendor bundles to decide keep-as-is vs sanitize/remove.

## Verification Notes
- 2026-04-09: `gitleaks detect --no-git --source .` => `0` findings in current HEAD.
- 2026-04-09: targeted validation build passed:
  - `dotnet build Portal/WebSystem/WCMS.Framework/WCMS.Framework.csproj -c Release`
  - `dotnet build Portal/WebSystem/WebSystem/WCMS.WebSystem.WebApp.csproj -c Release`
- 2026-04-09: history rewrite dry-run completed in mirror clone with baseline `32` findings reduced to `0` post-rewrite (`docs/plans/completed/SECURITY_GIT_HISTORY_DRY_RUN_REPORT_2026-04-09.md`).
