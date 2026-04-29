# Security Git History Rewrite Runbook

Status: dry-run completed on 2026-04-09; cutover execution pending owner approval

## Pending Classification (2026-04-29)

| Item | Category | Current State | Remaining Action |
|---|---|---|---|
| Phase 0 rotation precondition | External/manual owner action pending | Rotation/session invalidation is not yet confirmed complete. | Complete rotation and record sign-off before cutover. |
| Freeze window and communication | Approval-gated pending | Dry-run exists, but freeze and comms plan are not approved. | Approve cutover window and contributor reset instructions. |
| Final rewrite cutover | Approval-gated execution pending | Operational steps are documented but intentionally not executed yet. | Run canonical rewrite cutover, force-push refs, and archive post-cutover history-scan evidence. |

## Objective
Purge historical secret-bearing content from git history after current-branch remediation and credential rotation are complete.

## Preconditions
- [ ] Phase 0 rotation is complete (SMS, DB creds, crypto keys, session invalidation).
- [x] Current HEAD scan is clean (`gitleaks detect --no-git --source .` => 0 findings).
- [ ] Freeze window approved by repo owners.
- [ ] Contributor communication prepared.

## Scope
- Refs: all branches and tags.
- Cleanup strategy:
  - Remove known secret-bearing binary/sample artifacts from history where not required.
  - Replace known secret literals with redacted placeholders.

## Candidate Historical Indicators
Evidence that sensitive values still exist in prior commits:
- `git log --all -S'SECRET_KEY'`
- `git log --all -S'<legacy-google-key-pattern>'`
- `git log --all -S'<legacy-recaptcha-private-key-pattern>'`

## Dry-Run Procedure (Mirror Clone)
1. Create disposable mirror:
   - `git clone --mirror . /tmp/mportal-history-rewrite.git`
2. Enter mirror:
   - `cd /tmp/mportal-history-rewrite.git`
3. Prepare replacement rules file (`/tmp/mportal-replacements.txt`):
   - Format: `literal==>replacement`
   - Use secure local source for literals; do not commit the raw values.
4. Run rewrite:
   - `git filter-repo --replace-text /tmp/mportal-replacements.txt`
   - Add `--invert-paths --path <path>` for artifacts that should be removed fully.
5. Validate:
   - `gitleaks git --report-format json --report-path /tmp/mportal-history-gitleaks.json`
   - `git for-each-ref --format='%(refname)' > /tmp/new-refs.txt`
6. Record impact:
   - refs changed
   - tags changed
   - communication/reset steps

Dry-run evidence:
- `docs/plans/completed/SECURITY_GIT_HISTORY_DRY_RUN_REPORT_2026-04-09.md`

## Cutover Procedure (After Approval)
1. Freeze merges.
2. Repeat validated rewrite procedure in designated canonical clone.
3. Force-push rewritten refs.
4. Require all consumers to re-clone or hard-reset.
5. Re-run full-history scan and archive results.

## Rollback Notes
- Keep a pre-rewrite mirror backup.
- If cutover fails validation, stop and restore original refs from backup.
