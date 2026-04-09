# Security Remediation Plan (Phased, Safe-First)

Status: in progress (Phases 1/1B/3 partially completed on 2026-04-09)  
Scope: `/Users/dsalunga/Projects/github.com/dsalunga/mPortal`  
Objective: remove active exposure in current branch first, then perform controlled history cleanup.

## Guiding Rules
- Do not run history rewrite until current-branch remediation and secret rotation are complete.
- Treat all exposed secrets as compromised now.
- Keep remediation auditable: one phase, one PR/commit group, one verification report.

## Phase 0: Containment and Rotation (No Code Changes Yet)
Goal: eliminate live risk before repo surgery.

Checklist:
- [ ] Rotate SMS credential exposed in `Portal/Binaries/Database/WebRegistry.xml`.
- [ ] Rotate DB credentials used by dev/runtime defaults (`SA_PASSWORD`, `PG_PASSWORD`, `Password=postgres` fallback).
- [ ] Rotate crypto material represented by `SECRET_KEY`, `INIT_VECTOR`, `SALT`.
- [ ] Invalidate old auth cookies/sessions tied to previous key material.
- [ ] Temporarily restrict repo access if required by policy.

Exit criteria:
- [ ] New credentials/keys are issued and old ones are disabled.
- [ ] Security owner confirms rotation completion in writing.

## Phase 1: Current-Branch Secret Removal (Safe, Non-Destructive)
Goal: remove plaintext secrets from HEAD without rewriting history.

Checklist:
- [x] Replace concrete values with env/config references in:
  - `Portal/WebSystem/WCMS.Framework/Utilities/LoginSecurity.cs`
  - `Portal/WebSystem/WCMS.Framework/Utilities/SecurityHelper.cs`
  - `Portal/WebSystem/WebSystem/Program.cs`
  - `docker-compose.yml`
  - `docs/DEPLOYMENT_RUNBOOK.md`
- [x] Sanitize credential-bearing XML values:
  - `Portal/Binaries/Database/WebRegistry.xml`
  - `legacy/Portal/Binaries/Database/WebRegistry.xml`
- [x] Ensure all fallback passwords are non-real placeholders only.
- [x] Add/update `.env.example` and secure setup notes (no real secrets).

Exit criteria:
- [x] Tracked-file scan shows no high-risk plaintext secret values.
- [x] App still builds/tests successfully.

## Phase 1B: Current-Branch PII/Contact Anonymization (Completed 2026-04-09)
Goal: remove direct personal identifiers in first-party source/content while preserving application behavior.

Checklist:
- [x] Replace personal contact emails with dummy addresses in first-party templates/content.
- [x] Replace personal phone numbers with dummy values in first-party templates/content.
- [x] Replace salutation/name references (`Bro./Sis./Brother/Sister`) with neutral placeholders (`Mr./Ms.` style).
- [x] Apply updates to both modern (`Portal/`) and legacy (`legacy/Portal/`) sources.
- [x] Re-run first-party pattern scans and confirm zero known-personal-identifier hits from tracked patterns.

Exit criteria:
- [x] No known personal emails from the tracked domain set remain in first-party sources.
- [x] No `Bro./Sis./Brother/Sister` salutation references remain in first-party sources.
- [ ] Optional: third-party/vendor payload review completed for sanitize/remove decisions.

## Phase 2: Legacy Footprint Reduction (Still Non-Destructive)
Goal: reduce accidental redistribution of proprietary/sensitive assets.

Checklist:
- [ ] Classify committed binaries/archives under `legacy/` and `Portal/Binaries/`:
  - keep (required for migration evidence)
  - sanitize
  - remove from active branch
- [ ] Remove/sanitize unnecessary credential-bearing legacy files.
- [ ] Document retained proprietary artifacts and justification.

Exit criteria:
- [ ] Inventory document completed and approved.
- [ ] Non-essential sensitive artifacts removed or redacted from HEAD.

## Phase 3: Preventive Controls (Before History Rewrite)
Goal: prevent reintroduction.

Checklist:
- [x] Add CI secret scanning (`gitleaks` or equivalent).
- [x] Add pre-commit secret hook guidance.
- [x] Add policy doc section: no plaintext secrets in repo or legacy snapshots.
- [x] Add PR checklist item: “No secrets introduced”.

Exit criteria:
- [x] CI fails on new secret patterns.
- [ ] Team confirms local hook adoption path.

## Phase 4: History Rewrite Preparation (Planning + Dry Run)
Goal: prepare safe rewrite execution package, no force-push yet.

Reference: `docs/plans/SECURITY_GIT_HISTORY_REWRITE_RUNBOOK.md`

Checklist:
- [x] Define rewrite scope (all branches/tags vs selected refs).
- [ ] Build exact removal rules for known sensitive values and files.
- [ ] Dry-run rewrite in a disposable mirror clone.
- [ ] Produce impact report:
  - refs changed
  - tags changed
  - estimated clone reset steps

Exit criteria:
- [ ] Dry-run report approved by repo owners.
- [ ] Communication plan drafted for contributors/CI consumers.

## Phase 5: History Rewrite Execution (Controlled Cutover)
Goal: purge secrets from git history and coordinate rollout.

Checklist:
- [ ] Freeze merges during cutover window.
- [ ] Execute approved rewrite procedure.
- [ ] Force-push rewritten refs.
- [ ] Instruct all consumers to re-clone or hard reset to new history.
- [ ] Re-run full-history secret scan and archive report.

Exit criteria:
- [ ] Full-history scan reports zero known high-risk findings.
- [ ] Team sign-off complete.

## Suggested Execution Order (Operational)
1. Phase 0 (rotation)
2. Phase 1 (HEAD cleanup)
3. Phase 3 (controls)
4. Phase 2 (legacy rationalization, parallelizable with Phase 3)
5. Phase 4 (rewrite dry run)
6. Phase 5 (cutover)

## Approval Gates
- Gate A: approve Phase 1 file changes before commit.
- Gate B: approve whether `legacy/` is sanitized vs partially removed.
- Gate C: approve history rewrite scope and freeze window.

## Deliverables Per Phase
- Phase 0: rotation record (internal)
- Phase 1: remediation PR + scan output
- Phase 2: legacy inventory + decision log
- Phase 3: CI policy + hook docs
- Phase 4: dry-run rewrite report
- Phase 5: cutover report + post-rewrite scan evidence
